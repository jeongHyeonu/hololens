using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] BulletManager bulletManager;
    [SerializeField] GameObject gameStart_ui;
    [SerializeField] GameObject play_ui;
    [SerializeField] GameObject gameOver_ui;

    [SerializeField] TextMeshProUGUI scoreText;

    public int score = 0;
    public bool isGameOver = false;

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
    private void Awake()
    {
        instance = this;
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
        gameStart_ui.SetActive(false);
        play_ui.SetActive(true);
        bulletManager.ShootBulletStart();
    }

    public void GameOver()
    {
        //Time.timeScale = 0f;
        play_ui.SetActive(false);
        gameOver_ui.SetActive(true);
        isGameOver = true;
        bulletManager.ShootBulletEnd();
        Player.Instance.ani.SetBool("Died", true);
        Player.Instance.col.height = 0.3f;
    }

    public void ScoreIncrease()
    {
        score++;
        scoreText.text = "score : " + score;
    }

    public void GameOverButton()
    {
        SceneManager.LoadScene("JHW_project");
    }
}
