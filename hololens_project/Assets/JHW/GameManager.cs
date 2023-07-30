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

    // 싱글톤
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
        // 이거 쓰면 게임 멈추게 하는건데, 모바일이나 pc게임에선 되는데, 홀로렌즈는 쓰면 안 되는 것 같다.. input이 달라서 그런듯

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
