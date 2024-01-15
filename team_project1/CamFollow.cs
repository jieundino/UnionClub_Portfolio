using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 움직임에 따라 카메라도 움직임
public class CamFollow : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        transform.position = target.position;
    }
}
