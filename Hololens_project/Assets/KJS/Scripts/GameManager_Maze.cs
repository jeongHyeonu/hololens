using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_Maze : MonoBehaviour
{


    // ���� ������Ʈ ����
    public GameObject gameLabel;
    public GameObject gamePlay_ui;
    public GameObject gameRestart_ui;

    public Transform mainCam;

    Transform finish;
    Transform player;
    

    // ���� ���� UI �ؽ�Ʈ ������Ʈ ����
    Text gameText;

    public float finishLine = 3.5f;

    // ���� ���� ���
    public enum GameState
    {
        Ready,
        Run,
        GameOver,
        Finish
    }


    // ���� ���� ���� ����
    public GameState gState;

    // EnemyFSM Ŭ���� ����
    EnemyFSM enemy;
    // PlayerMove Ŭ���� ����
    PlayerMove playerMove;

    IEnumerator ReadyToStart()
    {

        // 1�ʰ� ����Ѵ�.
        yield return new WaitForSeconds(1.0f);
        mainCam.Rotate(90, 90, 0);

        // 2�ʰ� ����Ѵ�.
        yield return new WaitForSeconds(2.0f);
        gamePlay_ui.transform.Rotate(12, -10, -190);

        // ���� �ؽ�Ʈ�� ���� 'Go!' ����
        gameText.text = "Go!";

        // 0.5�ʰ� ���
        yield return new WaitForSeconds(0.5f);

        // ���� �ؽ�Ʈ�� ��Ȱ��ȭ
        gameLabel.SetActive(false);

        // ���¸� '���� ��' ���·� ����
        gState = GameState.Run;
    }



    // Start is called before the first frame update
    void Start()
    {
        // �ʱ� ���� ���´� �غ� ���·� ����
        gState = GameState.Ready;
        

        // ���� ���� UI ������Ʈ���� Text ������Ʈ ��������
        gameText = gameLabel.GetComponent<Text>();

        // ���� �ؽ�Ʈ�� ������ 'Ready...'�� ����
        gameText.text = "Ready...";

        // ���� �ؽ�Ʈ�� ������ ��Ȳ������ ����
        gameText.color = new Color32(255, 185, 0, 255);

        // ���̽�ƽ Ȱ��ȭ
        gamePlay_ui.SetActive(true);

        // ���� �غ�-> ���� �� ���·� ��ȯ
        StartCoroutine(ReadyToStart());

        // �÷��̾� ������Ʈ�� ã�� �� �÷��̾��� PlayerMove ������Ʈ �޾ƿ���
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        enemy = GameObject.Find("Enemy").GetComponent<EnemyFSM>();
        player = GameObject.Find("Player").transform;
        finish = GameObject.Find("Finish").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // ���ʹ̿� �÷��̾ �ε�����
        if (enemy.collisionState == true)
        {

            // �÷��̾� �ִϸ��̼� ����
            playerMove.stop();

            gamePlay_ui.SetActive(false);

            // ���� �ؽ�Ʈ�� Ȱ��ȭ
            gameLabel.SetActive(true);

            // ���� �ؽ�Ʈ�� ������ 'Game Over'�� ����
            gameText.text = "Game Over";

            // ���� �ؽ�Ʈ�� ������ ���������� ����
            gameText.color = new Color32(255, 0, 0, 255);

            // ��ư ������Ʈ�� Ȱ��ȭ
            gameRestart_ui.gameObject.SetActive(true);

            // ���¸� '���� ����' ���·� ����
            gState = GameState.GameOver;
        }

        if (Vector3.Distance(finish.position, player.position) < finishLine)
        {
            // �÷��̾� �ִϸ��̼� ����
            playerMove.stop();

            gamePlay_ui.SetActive(false);

            // ���� �ؽ�Ʈ�� Ȱ��ȭ
            gameLabel.SetActive(true);
            // ���� �ؽ�Ʈ�� ������ 'Game Over'�� ����
            gameText.text = "You Win";

            // ���� �ؽ�Ʈ�� ������ �Ķ������� ����
            gameText.color = new Color32(0, 0, 255, 255);

            // ��ư ������Ʈ�� Ȱ��ȭ
            gameRestart_ui.gameObject.SetActive(true);

            // ���¸� '���� ����' ���·� ����
            gState = GameState.Finish;
        }
    }


    public void RestartButton()
    {
        SceneManager.LoadScene("KJS_project");
    }
}
