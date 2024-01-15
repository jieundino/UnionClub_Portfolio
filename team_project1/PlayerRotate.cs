using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어 회전 마우스 입력값에 따라 x축으로만 회전함.
public class PlayerRotate : MonoBehaviour
{
    public float rotSpeed = 200f;

    float mx = 0;

    void Update()
    {
        float mouse_X = Input.GetAxis("Mouse X");

        mx += mouse_X * rotSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
