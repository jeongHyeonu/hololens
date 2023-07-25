using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour { 
    float x, y;
    Vector3 originPos;

    [SerializeField] GameObject Joystick_UI;
    [SerializeField] GameObject Joystick_Button;

    LineRenderer lineRenderer;

    bool isDragging = false;

    // Start is called before the first frame update
    void Start()
    {
        originPos = this.transform.position;
        lineRenderer = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging) // 조이스틱 드래깅 상태일때 실행
        {
            // 벡터 정규화
            Vector2 normalizedVector = Joystick_Button.transform.position - Joystick_UI.transform.position;
            normalizedVector.Normalize();

            // 조이스틱 머리부분 clamp
            Vector2 vec = Joystick_Button.transform.localPosition - Joystick_UI.transform.localPosition;
            vec = Vector2.ClampMagnitude(vec, .04f);
            Joystick_Button.transform.localPosition = vec;

            // 플레이어 이동, 조이스틱에서 손 놓은 상태면 움직임 멈춤
            //if (!isDragging) { Player.Instance.MovePlayerJoystick(0, 0); return; }
            Player.Instance.MovePlayerJoystick(normalizedVector.x, normalizedVector.y);
        }
        
        // 조이스틱 스틱 부분 lineRenderer
        lineRenderer.SetPosition(0, Joystick_UI.transform.position);
        lineRenderer.SetPosition(1, Joystick_Button.transform.position);
    }

    public void OnDrag(GameObject _obj)
    {
        isDragging = true;
    }

    public void ExitDrag(GameObject _obj)
    {
        Joystick_Button.transform.localPosition = Vector3.zero;
        isDragging = false;
    }
}
