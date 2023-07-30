using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] BulletManager bulletManager;
    [SerializeField] GameObject gameStart_ui;
    [SerializeField] GameObject play_ui;
    [SerializeField] GameObject gameOver_ui;

    // �̱���
    private static GameManager instance = null;
    public static GameManager Instance
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
        // Time.timeScale = 0f;
        // �̰� ���� ���� ���߰� �ϴ°ǵ�, ������̳� pc���ӿ��� �Ǵµ�, Ȧ�η���� ���� �� �Ǵ� �� ����.. input�� �޶� �׷���

        gameStart_ui.SetActive(true);
    }

    public void GameStart()
    {
        //Time.timeScale = 1f;
        play_ui.SetActive(true);
        bulletManager.ShootBulletStart();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        play_ui.SetActive(false);
        gameOver_ui.SetActive(true);
        //bulletManager.StopBullet();
    }
}
