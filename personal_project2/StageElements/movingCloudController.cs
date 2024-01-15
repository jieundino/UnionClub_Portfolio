using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingCloudController : MonoBehaviour
{
    Vector3 pos;            // 현재 위치
    float delta = 2.5f;     // 좌우로 이동 가능한 x 최대값.
    float speed = 0.5f;     // 구름 이동속도

    public GameObject player;

    void Start()
    {
        this.pos = this.transform.position;
    }

    void Update()
    {
        Vector3 v = pos;
        v.x += delta * Mathf.Sin(Time.time * speed);    // 좌우 이동의 최대치 및 반전 처리.
        // Mathf.Sin(Time.time*speed)는 -1 ~ 1 사이의 값이 나옴. 이것으로 반전 처리가 됨.

        this.transform.position = v;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 움직이는 구름 위에 올라온게 플레이어면
        if (other.transform.CompareTag("Player"))
            other.transform.SetParent(transform);
        // 플레이어를 이 구름의 자식으로 넣어줘서 같이 움직이게 함.
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
            other.transform.SetParent(null);
        // 구름 위에서 나간게 플레이어면 자식으로 넣어준거 끊어줌. 플레이어 자유롭게 움직일 수 있게 함.
    }
}
