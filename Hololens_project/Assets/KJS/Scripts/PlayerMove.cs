using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    //CharacterController cc; // ĳ���� ��Ʈ�ѷ� ����

    [SerializeField] float moveSpeed; // �̵� �ӵ� ����
    [SerializeField] float rotateSpeed;

    //float gravity = -20f; // �߷� ����
    //float yVelocity = 0; // ���� �ӷ� ����
    //
    //float jumpPower = 10f; // ������ ����
    //
    //bool isJumping = false; // ���� ���� ����

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
        // ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
        //cc = GetComponent<CharacterController>();

        anim = GetComponentInChildren<Animator>();
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        // ������� �Է��� �ޱ�
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");


        if (isJoyStickDown) { 

        // �̵� ���� ����
        //Vector3 dir = new Vector3(h, 0, v);
        Vector3 dir = new Vector3(joyStickX, 0, joyStickY);
        //dir = dir.normalized;

        // �̵� �� ȸ��
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);

        // �̵� ���� Ʈ�� ȣ��
        anim.SetFloat("MoveMotion", dir.magnitude);
        }


        //// ���� ī�޶� �������� ������ ��ȯ
        //dir = Camera.main.transform.TransformDirection(dir);
        //
        //// ����, ���� ���̾���, �ٽ� �ٴڿ� �����ߴٸ�
        //if (isJumping && cc.collisionFlags == CollisionFlags.Below)
        //{
        //    // ���� �� ���·� �ʱ�ȭ�ϰ�
        //    isJumping = false;
        //    // ĳ���� ���� �ӵ��� 0���� �����.
        //    yVelocity = 0;
        //}
        //
        //// ����, spacebar Ű�� ������, �������� ���� ���¶��
        //if (Input.GetButton("Jump") && !isJumping)
        //{
        //    //ĳ���� ���� �ӵ��� �������� �����ϰ� ���� ���·� ����
        //    yVelocity = jumpPower;
        //    isJumping = true;
        //}
        //
        //// ĳ���� ���� �ӵ��� �߷� ���� ����
        //yVelocity += gravity * Time.deltaTime;
        //dir.y = yVelocity;

        // �̵� �ӵ��� ���� �̵�
        // p = p0 + vt
        //cc.Move(dir * moveSpeed * Time.deltaTime);
    }
}
