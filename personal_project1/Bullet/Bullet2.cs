using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    // 필요속성 : 이동속도
    public float speed = 5;

    private void Start()
    {
        // 계속 날아가게 하면 무거워지니까
        // 보통 2초 후에 화면에서 벗어나서 2초 후에 게임오브젝트인 도토리 삭제
        Invoke("DestroyBullet", 2f);
    }

    void Update()
    {
        // 왼쪽으로 날아감
        transform.Translate(transform.right * -speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
