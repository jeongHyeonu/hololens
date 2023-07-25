using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
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
        //Debug.Log(Vector3.Normalize(Joystick_Button.transform.position - Joystick_UI.transform.position));

        Vector2 normalizedVector = Joystick_Button.transform.position - Joystick_UI.transform.position;
        normalizedVector.Normalize();

        Vector2 vec = Joystick_Button.transform.localPosition - Joystick_UI.transform.localPosition;
        Debug.Log(vec);
        vec = Vector2.ClampMagnitude(vec, .04f);
        Joystick_Button.transform.localPosition = vec;

        lineRenderer.SetPosition(0, Joystick_UI.transform.position);
        lineRenderer.SetPosition(1,Joystick_Button.transform.position);

        //Player.Instance.MovePlayer(normalizedVector.x, normalizedVector.y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        x += eventData.delta.x * Time.deltaTime * 100;  
        y += eventData.delta.y * Time.deltaTime * 100;

        x = Mathf.Clamp(x, -50f, 50f);
        y = Mathf.Clamp(y, -50f, 50f);


        Vector2 vec = new Vector2(x, y);
        float distance = 50f;// Vector2.Distance(originPos, vec);

        vec = Vector2.ClampMagnitude(vec, distance);
        this.transform.localPosition = vec;

        //Player.Instance.MovePlayerJoystick(x, y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("down");

        x = y = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Player.Instance.isJoyStickDown = false;
        this.transform.position = originPos;
        x = y = 0;
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
