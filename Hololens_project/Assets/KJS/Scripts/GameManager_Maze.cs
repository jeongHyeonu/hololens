using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_Maze : MonoBehaviour
{

    // �̱��� ����
    public static GameManager_Maze gm;

    // ���� ���� UI ������Ʈ ����
    public GameObject gameLabel;
    public GameObject gamePlay_ui;
    public GameObject gameRestart_ui;

    public Transform mainCam;

    Transform finish;
    Transform player;
    

    // ���� ���� UI �ؽ�Ʈ ������Ʈ ����
    Text gameText;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    public float finishLine = 1.5f;

    // ���� ���� ����
    public enum GameState
    {
        Ready,
        Run,
        GameOver,
        Finish
    }


    // ���� ���� ���� ����
    public GameState gState;

    // PlayerMove Ŭ���� ����
    EnemyFSM enemy;
    PlayerMove playerMove;

    IEnumerator ReadyToStart()
    {
        
        // 2�ʰ� ����Ѵ�.
        yield return new WaitForSeconds(1.0f);
        mainCam.Rotate(90, 90, 0);
        
        yield return new WaitForSeconds(2.0f);
        
        gamePlay_ui.transform.Rotate(12, -10, -190);
        // ���� �ؽ�Ʈ�� ������ 'Go!'�� �Ѵ�.
        gameText.text = "Go!";

        // 0.5�ʰ� ����Ѵ�.
        yield return new WaitForSeconds(0.5f);

        // ���� �ؽ�Ʈ�� ��Ȱ��ȭ �Ѵ�.
        gameLabel.SetActive(false);

        // ���¸� '���� ��' ���·� �����Ѵ�.
        gState = GameState.Run;
    }



    // Start is called before the first frame update
    void Start()
    {
        // �ʱ� ���� ���´� �غ� ���·� �����Ѵ�.
        gState = GameState.Ready;
        gamePlay_ui.SetActive(true);
        gamePlay_ui.transform.Translate(10, -3, -215);
        gamePlay_ui.transform.Rotate(12, -10, -190);
        
        

        

        // ���� ���� UI ������Ʈ���� Text ������Ʈ�� �����´�.
        gameText = gameLabel.GetComponent<Text>();

        // ���� �ؽ�Ʈ�� ������ 'Ready...'�� �Ѵ�.
        gameText.text = "Ready...";

        // ���� �ؽ�Ʈ�� ������ ��Ȳ������ �Ѵ�.
        gameText.color = new Color32(255, 185, 0, 255);

        // ���� �غ�-> ���� �� ���·� ��ȯ�ϱ�
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

            // ���� �ؽ�Ʈ�� ������ 'Game Over'�� �Ѵ�.
            gameText.text = "Game Over";

            // ���� �ؽ�Ʈ�� ������ ���������� �Ѵ�.
            gameText.color = new Color32(255, 0, 0, 255);

            // ��ư ������Ʈ�� Ȱ��ȭ �Ѵ�.
            gameRestart_ui.gameObject.SetActive(true);

            // ���¸� '���� ����' ���·� �����Ѵ�.
            gState = GameState.GameOver;
        }

        if (Vector3.Distance(finish.position, player.position) < finishLine)
        {
            // �÷��̾� �ִϸ��̼� ����
            playerMove.stop();

            gamePlay_ui.SetActive(false);

            // ���� �ؽ�Ʈ�� Ȱ��ȭ
            gameLabel.SetActive(true);
            // ���� �ؽ�Ʈ�� ������ 'Game Over'�� �Ѵ�.
            gameText.text = "You Win";

            // ���� �ؽ�Ʈ�� ������ �Ķ������� �Ѵ�.
            gameText.color = new Color32(0, 0, 255, 255);

            // ��ư ������Ʈ�� Ȱ��ȭ �Ѵ�.
            gameRestart_ui.gameObject.SetActive(true);

            // ���¸� '���� ����' ���·� �����Ѵ�.
            gState = GameState.Finish;
        }
    }


    public void RestartButton()
    {
        SceneManager.LoadScene("KJS_project");
    }
}
