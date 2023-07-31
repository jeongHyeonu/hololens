using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletSpawn;
    [SerializeField] GameObject BulletCollider;
    [SerializeField] GameObject BulletPool;

    [SerializeField] List<GameObject> bulletSpawnList;

    float coolTime = 1f;

    // 싱글톤
    private static BulletManager instance = null;
    public static BulletManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Start()
    {
        for(int i = 0; i < bulletSpawn.transform.childCount; i++)
        {
            bulletSpawnList.Add(bulletSpawn.transform.GetChild(i).gameObject);
        }
    }

    public void ShootBulletStart() { StartCoroutine(ShootBullet()); }
    public void ShootBulletEnd() { StopCoroutine(ShootBullet()); }

    IEnumerator ShootBullet()
    {
        // 게임오버시 실행 X
        if (!GameManager.Instance.isGameOver)
        {
            GameManager.Instance.ScoreIncrease();       // 총알 발사때마다 점수 증가
            int randomPos = Random.Range(0, bulletSpawnList.Count); // 총알 랜덤좌표 설정

            // 오브젝트 풀링
            // GameObject b = Instantiate(bullet, bulletSpawnList[randomPos].transform);

            // 총알 발사 가능한 오브젝트 선택
            int availeBulletNumber = 0;
            while (BulletPool.transform.GetChild(availeBulletNumber).gameObject.activeSelf)
                availeBulletNumber++;

            GameObject b = BulletPool.transform.GetChild(availeBulletNumber).gameObject;
            b.transform.position = bulletSpawnList[randomPos].transform.position;
            b.SetActive(true);

            yield return new WaitForSeconds(coolTime);
            StartCoroutine(ShootBullet());
        }
    }
}
