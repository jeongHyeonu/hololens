using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        // ī�޶��� ��ġ�� ��ǥ Ʈ�������� ��ġ�� ��ġ
        transform.position = target.position;
    }
}
