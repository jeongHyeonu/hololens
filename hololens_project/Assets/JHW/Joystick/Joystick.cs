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
        if (isDragging) // ���̽�ƽ �巡�� �����϶� ����
        {
            // ���� ����ȭ
            Vector2 normalizedVector = Joystick_Button.transform.position - Joystick_UI.transform.position;
            normalizedVector.Normalize();

            // ���̽�ƽ �Ӹ��κ� clamp
            Vector2 vec = Joystick_Button.transform.localPosition - Joystick_UI.transform.localPosition;
            vec = Vector2.ClampMagnitude(vec, .04f);
            Joystick_Button.transform.localPosition = vec;

            // �÷��̾� �̵�, ���̽�ƽ���� �� ���� ���¸� ������ ����
            //if (!isDragging) { Player.Instance.MovePlayerJoystick(0, 0); return; }
            Player.Instance.MovePlayerJoystick(normalizedVector.x, normalizedVector.y);
        }
        
        // ���̽�ƽ ��ƽ �κ� lineRenderer
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
