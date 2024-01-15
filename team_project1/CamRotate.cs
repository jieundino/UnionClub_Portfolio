using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라도 마우스 입력값을 받아서 같이 회전함.
// 카메라 회전 범위 90도로 제한함
public class CamRotate : MonoBehaviour
{
    public float rotSpeed = 200f;

    float mx = 0;
    float my = 0;

    void Update()
    {
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_Y * rotSpeed * Time.deltaTime;

        my = Mathf.Clamp(my, -90f, 90f);

        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
