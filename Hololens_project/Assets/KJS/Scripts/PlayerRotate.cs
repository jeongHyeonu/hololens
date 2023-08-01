using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{

    public float rotSpeed = 200f; // 회전 속도 변수

    // 회전 값 변수
    float mx = 0;

    // Update is called once per frame
    void Update()
    {

        // 마우스 입력을 받음
        float mouse_X = Input.GetAxis("Mouse X");

        // 회전 값 변수에 마우스 입력 값만큼 미리 누적
        mx += mouse_X * rotSpeed * Time.deltaTime;

        // 회전 방향으로 물체를 회전
        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
