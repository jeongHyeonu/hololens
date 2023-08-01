using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] float moveSpeed; // 이동 속도 변수
    [SerializeField] float rotateSpeed;


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

        anim = GetComponentInChildren<Animator>();
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {

   
        if (isJoyStickDown)
        {

            // 이동 방향 설정
            Vector3 dir = new Vector3(joyStickX, 0, joyStickY);

            // 이동 및 회전
            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);

            // 이동 블랜드 트리 호출
            anim.SetFloat("MoveMotion", dir.magnitude);
        }
        else
        {
            // 이동 블랜드 트리 호출
            anim.SetFloat("MoveMotion", 0);
        }


    }
}
