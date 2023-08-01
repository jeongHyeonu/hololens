using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] float moveSpeed; // �̵� �ӵ� ����
    [SerializeField] float rotateSpeed;


    Animator anim; // �ִϸ����� ����
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

            // �̵� ���� ����
            Vector3 dir = new Vector3(joyStickX, 0, joyStickY);

            // �̵� �� ȸ��
            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);

            // �̵� ���� Ʈ�� ȣ��
            anim.SetFloat("MoveMotion", dir.magnitude);
        }
        else
        {
            // �̵� ���� Ʈ�� ȣ��
            anim.SetFloat("MoveMotion", 0);
        }


    }
}
