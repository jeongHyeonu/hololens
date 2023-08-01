using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{

    // ���ʹ� ���� ���
    enum EnemyState
    {
        Idle, // ���
        Chase, // �߰�
        Return // ����
    }

    // ���ʹ� ���� ����
    EnemyState e_State;

    // �÷��̾� Ʈ���� ��
    Transform player;

    // �÷��̾� �߰� ����
    public float findDistance = 8f;

    // �ִϸ����� ����
    Animator anim;

    // �÷��̾�� �浹 ����
    public float colliderDistance = 1f;

    // ���� �浹 ����
    public bool collisionState = false;

    // �̵� ���� ����
    public float moveDistance = 20f;

    // �ʱ� ��ġ ����� ����
    Vector3 originPos; // ��ǥ
    Quaternion originRot; // ȸ������

    // ������̼� ������Ʈ ����
    NavMeshAgent nvAgent;

    void Idle()
    {
        // �÷��̾���� �Ÿ��� �÷��̾� �߰� ���� �̳���� Chase ���·� ��ȯ
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            e_State = EnemyState.Chase;
            print("���� ��ȯ: Idle -> Chase");

            // �̵� �ִϸ��̼����� ��ȯ
            anim.SetTrigger("IdleToMove");
        }
    }

    void Chase()
    {
        // ���� ��ġ�� �ʱ� ��ġ���� �̵� ���� ������ �Ѿ�����
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            // ���� ���¸� ����(Return)�� ��ȯ�Ѵ�.
            e_State = EnemyState.Return;
            print("���� ��ȯ: Chase -> Return");
        }

        // �÷��̾���� �Ÿ��� �浹 ���� ���� �ָ� �÷��̾ ���� �̵�
        if (Vector3.Distance(transform.position, player.position) > colliderDistance)
        {

            // ������̼� ������Ʈ�� �̵��� ���߰� ��θ� �ʱ�ȭ
            nvAgent.isStopped = true;
            nvAgent.ResetPath();

            // ������̼����� �����ϴ� �ּ� �Ÿ��� �浹 ���� �Ÿ��� ����
            nvAgent.stoppingDistance = colliderDistance;

            // ������̼��� �������� �÷��̾��� ��ġ�� ����
            nvAgent.destination = player.position;
        }
        // �÷��̾���� �Ÿ��� �浹 ���� �̳� �̶�� �浹 ������ true ����
        else
        {
            print("�÷��̾�� ���� �浹");
            collisionState = true;
        }

    }

    void Return()
    {
        // �ʱ� ��ġ������ �Ÿ��� 1f �̻��̶�� �ʱ� ��ġ ������ �̵�
        if (Vector3.Distance(transform.position, originPos) > 1f)
        {
            // ������̼��� �������� �ʱ� ����� ��ġ�� ����
            nvAgent.destination = originPos;

            // ������̼����� �����ϴ� �ּ� �Ÿ��� '0'���� ����
            nvAgent.stoppingDistance = 0;
        }
        // �ʱ� ��ġ������ �Ÿ��� 1f ���϶�� �ڽ��� ��ġ�� �ʱ� ��ġ�� �����ϰ� ���� ���¸� ���� ��ȯ
        else
        {

            // ������̼� ������Ʈ�� �̵��� ���߰� ��θ� �ʱ�ȭ
            nvAgent.isStopped = true;
            nvAgent.ResetPath();

            // ��ġ ���� ȸ�� ���� �ʱ� ���·� ��ȯ
            transform.position = originPos;
            originRot = transform.rotation;

            e_State = EnemyState.Idle;
            print("���� ��ȯ: Return -> Idle");

            // ��� �ִϸ��̼����� ��ȯ�ϴ� Ʈ�������� ȣ��
            anim.SetTrigger("MoveToIdle");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // ������ ���ʹ� ����
        e_State = EnemyState.Idle;
        // �÷��̾��� Ʈ������ ������Ʈ ����
        player = GameObject.Find("Player").transform;

        // �ڽ� ������Ʈ�� ���� �ִϸ����� ���� ����
        anim = transform.GetComponentInChildren<Animator>();

        // �ڽ��� �ʱ� ��ġ ����
        originPos = transform.position;
        originRot = transform.rotation;

        // ������̼� ������Ʈ ������Ʈ ����
        nvAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        // ���º� ��� ����
        switch (e_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Chase:
                Chase();
                break;
            case EnemyState.Return:
                Return();
                break;
        }
    }
}
