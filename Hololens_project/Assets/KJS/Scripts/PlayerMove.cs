using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    //CharacterController cc; // 캐릭터 컨트롤러 변수

    [SerializeField] float moveSpeed; // 이동 속도 변수
    [SerializeField] float rotateSpeed;

    //float gravity = -20f; // 중력 변수
    //float yVelocity = 0; // 수직 속력 변수
    //
    //float jumpPower = 10f; // 점프력 변수
    //
    //bool isJumping = false; // 점프 상태 변수

    Animator anim; // 애니메이터 변수
    public CapsuleCollider col;

    float joyStickX;
    float joyStickY;

    public bool isJoyStickDown = false;

    private static PlayerMove instance = null;

    public static PlayerMove Instance
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

    public void MovePlayerJoystick(float x, float y)
    {
        isJoyStickDown = true;
        joyStickX = x;
        joyStickY = y;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 캐릭터 콘트롤러 컴포넌트 받아오기
        //cc = GetComponent<CharacterController>();

        anim = GetComponentInChildren<Animator>();
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        // 사용자의 입력을 받기
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");


        if (isJoyStickDown) { 

        // 이동 방향 설정
        //Vector3 dir = new Vector3(h, 0, v);
        Vector3 dir = new Vector3(joyStickX, 0, joyStickY);
        //dir = dir.normalized;

        // 이동 및 회전
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);

        // 이동 블랜드 트리 호출
        anim.SetFloat("MoveMotion", dir.magnitude);
        }


        //// 메인 카메라를 기준으로 방향을 전환
        //dir = Camera.main.transform.TransformDirection(dir);
        //
        //// 만일, 점프 중이었고, 다시 바닥에 착지했다면
        //if (isJumping && cc.collisionFlags == CollisionFlags.Below)
        //{
        //    // 점프 전 상태로 초기화하고
        //    isJumping = false;
        //    // 캐릭터 수직 속도를 0으로 만든다.
        //    yVelocity = 0;
        //}
        //
        //// 만일, spacebar 키를 눌렀고, 점프하지 않은 상태라면
        //if (Input.GetButton("Jump") && !isJumping)
        //{
        //    //캐릭터 수직 속도에 점프력을 적용하고 점프 상태로 변경
        //    yVelocity = jumpPower;
        //    isJumping = true;
        //}
        //
        //// 캐릭터 수직 속도에 중력 값을 적용
        //yVelocity += gravity * Time.deltaTime;
        //dir.y = yVelocity;

        // 이동 속도에 맞춰 이동
        // p = p0 + vt
        //cc.Move(dir * moveSpeed * Time.deltaTime);
    }
}
