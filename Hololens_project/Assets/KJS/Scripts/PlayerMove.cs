using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController cc; // ĳ���� ��Ʈ�ѷ� ����

    float moveSpeed = 7f; // �̵� �ӵ� ����

    float gravity = -20f; // �߷� ����
    float yVelocity = 0; // ���� �ӷ� ����

    float jumpPower = 10f; // ������ ����

    bool isJumping = false; // ���� ���� ����

    Animator anim; // �ִϸ����� ����

    // Start is called before the first frame update
    void Start()
    {
        // ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
        cc = GetComponent<CharacterController>();

        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // ������� �Է��� �ޱ�
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // �̵� ���� ����
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        // �̵� ���� Ʈ�� ȣ��
        anim.SetFloat("MoveMotion", dir.magnitude);

        // ���� ī�޶� �������� ������ ��ȯ
        dir = Camera.main.transform.TransformDirection(dir);

        // ����, ���� ���̾���, �ٽ� �ٴڿ� �����ߴٸ�
        if (isJumping && cc.collisionFlags == CollisionFlags.Below)
        {
            // ���� �� ���·� �ʱ�ȭ�ϰ�
            isJumping = false;
            // ĳ���� ���� �ӵ��� 0���� �����.
            yVelocity = 0;
        }

        // ����, spacebar Ű�� ������, �������� ���� ���¶��
        if (Input.GetButton("Jump") && !isJumping)
        {
            //ĳ���� ���� �ӵ��� �������� �����ϰ� ���� ���·� ����
            yVelocity = jumpPower;
            isJumping = true;
        }

        // ĳ���� ���� �ӵ��� �߷� ���� ����
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        // �̵� �ӵ��� ���� �̵�
        // p = p0 + vt
        cc.Move(dir * moveSpeed * Time.deltaTime);
    }
}
