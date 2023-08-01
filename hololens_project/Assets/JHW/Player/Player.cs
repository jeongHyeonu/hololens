using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] float playerSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] GameObject playerModel;
    [SerializeField] GameObject particle;

    private static Player instance = null;
    float joyStickX;
    float joyStickY;
    
    public bool isJoyStickDown = false;
    public bool isJumping = false;

    public Animator ani;
    public CapsuleCollider col;

    public static Player Instance
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
        ani = playerModel.GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        // 게임오버 됬으면 실행 X
        if (GameManager.Instance.isGameOver) return;

        if (isJoyStickDown)
        {
            Vector3 dir = new Vector3(joyStickX, 0, joyStickY);

            // 이동 및 회전
            transform.position += dir * playerSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);

            // 플레이어 애니메이션
            if (!isJumping) ani.SetBool("walk", true);
        }
        else
        {
            if (!isJumping) ani.SetBool("walk", false);
        }


    }

    public void MovePlayerJoystick(float x, float y)
    {
        isJoyStickDown = true;
        joyStickX = x;
        joyStickY = y;
    }

    public void PlayerJump()
    {
        if (isJumping) return;

        ani.SetBool("jump", true);
        isJumping = true;
    }

    public void Player_JumpStart()
    {
        playerModel.GetComponent<Rigidbody>().drag = 2f;
        playerModel.GetComponent<Rigidbody>().AddForce(new Vector3(0, 3f, 0));
    }

    public void Player_JumpEnd()
    {
        playerModel.GetComponent<Rigidbody>().drag = 40f;
        ani.SetBool("jump", false);
        isJumping = false;
    }
}
