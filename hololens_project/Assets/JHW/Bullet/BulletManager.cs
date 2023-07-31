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

    // �̱���
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
        // ���ӿ����� ���� X
        if (!GameManager.Instance.isGameOver)
        {
            GameManager.Instance.ScoreIncrease();       // �Ѿ� �߻綧���� ���� ����
            int randomPos = Random.Range(0, bulletSpawnList.Count); // �Ѿ� ������ǥ ����

            // ������Ʈ Ǯ��
            // GameObject b = Instantiate(bullet, bulletSpawnList[randomPos].transform);

            // �Ѿ� �߻� ������ ������Ʈ ����
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
