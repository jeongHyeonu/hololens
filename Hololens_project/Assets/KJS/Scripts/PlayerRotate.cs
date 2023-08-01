using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{

    public float rotSpeed = 200f; // ȸ�� �ӵ� ����

    // ȸ�� �� ����
    float mx = 0;

    // Update is called once per frame
    void Update()
    {

        // ���콺 �Է��� ����
        float mouse_X = Input.GetAxis("Mouse X");

        // ȸ�� �� ������ ���콺 �Է� ����ŭ �̸� ����
        mx += mouse_X * rotSpeed * Time.deltaTime;

        // ȸ�� �������� ��ü�� ȸ��
        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
