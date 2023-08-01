using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{

    // 에너미 상태 상수
    enum EnemyState
    {
        Idle, // 대기
        Chase, // 추격
        Return // 복귀
    }

    // 에너미 상태 변수
    EnemyState e_State;

    // 플레이어 트랜스 폼
    Transform player;

    // 플레이어 발견 범위
    public float findDistance = 8f;

    // 애니메이터 변수
    Animator anim;

    // 플레이어와 충돌 범위
    public float colliderDistance = 1f;

    // 좀비 충돌 변수
    public bool collisionState = false;

    // 이동 가능 범위
    public float moveDistance = 20f;

    // 초기 위치 저장용 변수
    Vector3 originPos; // 좌표
    Quaternion originRot; // 회전방향

    // 내비게이션 에이전트 변수
    NavMeshAgent nvAgent;

    void Idle()
    {
        // 플레이어와의 거리가 플레이어 발견 범위 이내라면 Chase 상태로 전환
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            e_State = EnemyState.Chase;
            print("상태 전환: Idle -> Chase");

            // 이동 애니메이션으로 전환
            anim.SetTrigger("IdleToMove");
        }
    }

    void Chase()
    {
        // 현재 위치가 초기 위치에서 이동 가능 범위를 넘었을때
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            // 현재 상태를 복귀(Return)로 전환한다.
            e_State = EnemyState.Return;
            print("상태 전환: Chase -> Return");
        }

        // 플레이어와의 거리가 충돌 범위 보다 멀면 플레이어를 향해 이동
        if (Vector3.Distance(transform.position, player.position) > colliderDistance)
        {

            // 내비게이션 에이전트의 이동을 멈추고 경로를 초기화
            nvAgent.isStopped = true;
            nvAgent.ResetPath();

            // 내비게이션으로 접근하는 최소 거리를 충돌 가능 거리로 설정
            nvAgent.stoppingDistance = colliderDistance;

            // 내비게이션의 목적지를 플레이어의 위치로 설정
            nvAgent.destination = player.position;
        }
        // 플레이어와의 거리가 충돌 범위 이내 이라면 충돌 변수에 true 저장
        else
        {
            print("플레이어와 좀비 충돌");
            collisionState = true;
        }

    }

    void Return()
    {
        // 초기 위치에서의 거리가 1f 이상이라면 초기 위치 쪽으로 이동
        if (Vector3.Distance(transform.position, originPos) > 1f)
        {
            // 내비게이션의 목적지를 초기 저장된 위치로 설정
            nvAgent.destination = originPos;

            // 내비게이션으로 접근하는 최소 거리를 '0'으로 설정
            nvAgent.stoppingDistance = 0;
        }
        // 초기 위치에서의 거리가 1f 이하라면 자신의 위치를 초기 위치로 조정하고 현재 상태를 대기로 전환
        else
        {

            // 내비게이션 에이전트의 이동을 멈추고 경로를 초기화
            nvAgent.isStopped = true;
            nvAgent.ResetPath();

            // 위치 값과 회전 값을 초기 상태로 변환
            transform.position = originPos;
            originRot = transform.rotation;

            e_State = EnemyState.Idle;
            print("상태 전환: Return -> Idle");

            // 대기 애니메이션으로 전환하는 트랜지션을 호출
            anim.SetTrigger("MoveToIdle");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 최초의 에너미 상태
        e_State = EnemyState.Idle;
        // 플레이어의 트랜스폼 컴포넌트 저장
        player = GameObject.Find("Player").transform;

        // 자식 오브젝트로 부터 애니메이터 변수 저장
        anim = transform.GetComponentInChildren<Animator>();

        // 자신의 초기 위치 저장
        originPos = transform.position;
        originRot = transform.rotation;

        // 내비게이션 에이전트 컴포넌트 저장
        nvAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        // 상태별 기능 수행
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
