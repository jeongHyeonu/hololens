using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_Maze : MonoBehaviour
{

    // 싱글턴 변수
    public static GameManager_Maze gm;

    // 게임 상태 UI 오브젝트 변수
    public GameObject gameLabel;
    public GameObject gamePlay_ui;
    public GameObject gameRestart_ui;

    public Transform mainCam;

    Transform finish;
    Transform player;
    

    // 게임 상태 UI 텍스트 컴포넌트 변수
    Text gameText;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    public float finishLine = 1.5f;

    // 게임 상태 변수
    public enum GameState
    {
        Ready,
        Run,
        GameOver,
        Finish
    }


    // 현재 게임 상태 변수
    public GameState gState;

    // PlayerMove 클래스 변수
    EnemyFSM enemy;
    PlayerMove playerMove;

    IEnumerator ReadyToStart()
    {
        
        // 2초간 대기한다.
        yield return new WaitForSeconds(1.0f);
        mainCam.Rotate(90, 90, 0);
        
        yield return new WaitForSeconds(2.0f);
        
        gamePlay_ui.transform.Rotate(12, -10, -190);
        // 상태 텍스트의 내용을 'Go!'로 한다.
        gameText.text = "Go!";

        // 0.5초간 대기한다.
        yield return new WaitForSeconds(0.5f);

        // 상태 텍스트를 비활성화 한다.
        gameLabel.SetActive(false);

        // 상태를 '게임 중' 상태로 변경한다.
        gState = GameState.Run;
    }



    // Start is called before the first frame update
    void Start()
    {
        // 초기 게임 상태는 준비 상태로 설정한다.
        gState = GameState.Ready;
        gamePlay_ui.SetActive(true);
        gamePlay_ui.transform.Translate(10, -3, -215);
        gamePlay_ui.transform.Rotate(12, -10, -190);
        
        

        

        // 게임 상태 UI 오브젝트에서 Text 컴포넌트를 가져온다.
        gameText = gameLabel.GetComponent<Text>();

        // 상태 텍스트의 내용을 'Ready...'로 한다.
        gameText.text = "Ready...";

        // 상태 텍스트의 색상을 주황색으로 한다.
        gameText.color = new Color32(255, 185, 0, 255);

        // 게임 준비-> 게임 중 상태로 전환하기
        StartCoroutine(ReadyToStart());

        



        // 플레이어 오브젝트를 찾은 후 플레이어의 PlayerMove 컴포넌트 받아오기
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        enemy = GameObject.Find("Enemy").GetComponent<EnemyFSM>();
        player = GameObject.Find("Player").transform;
        finish = GameObject.Find("Finish").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // 에너미와 플레이어가 부딪히면
        if (enemy.collisionState == true)
        {

            // 플레이어 애니메이션 정지
            playerMove.stop();

            gamePlay_ui.SetActive(false);

            // 상태 텍스트를 활성화
            gameLabel.SetActive(true);

            // 상태 텍스트의 내용을 'Game Over'로 한다.
            gameText.text = "Game Over";

            // 상태 텍스트의 색상을 붉은색으로 한다.
            gameText.color = new Color32(255, 0, 0, 255);

            // 버튼 오브젝트를 활성화 한다.
            gameRestart_ui.gameObject.SetActive(true);

            // 상태를 '게임 오버' 상태로 변경한다.
            gState = GameState.GameOver;
        }

        if (Vector3.Distance(finish.position, player.position) < finishLine)
        {
            // 플레이어 애니메이션 정지
            playerMove.stop();

            gamePlay_ui.SetActive(false);

            // 상태 텍스트를 활성화
            gameLabel.SetActive(true);
            // 상태 텍스트의 내용을 'Game Over'로 한다.
            gameText.text = "You Win";

            // 상태 텍스트의 색상을 파란색으로 한다.
            gameText.color = new Color32(0, 0, 255, 255);

            // 버튼 오브젝트를 활성화 한다.
            gameRestart_ui.gameObject.SetActive(true);

            // 상태를 '게임 오버' 상태로 변경한다.
            gState = GameState.Finish;
        }
    }


    public void RestartButton()
    {
        SceneManager.LoadScene("KJS_project");
    }
}
