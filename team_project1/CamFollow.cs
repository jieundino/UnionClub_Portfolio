using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� �����ӿ� ���� ī�޶� ������
public class CamFollow : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        transform.position = target.position;
    }
}
