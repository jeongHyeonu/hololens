using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    float coolTime = 3f;

    // ΩÃ±€≈Ê
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

    public void ShootBulletStart()
    {
        Instantiate(bullet);
    }

    IEnumerator ShootBullet()
    {
        Instantiate(bullet);

        yield return new WaitForSeconds(coolTime);
        StartCoroutine(ShootBullet());
    }
}
