using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    // �ʿ�Ӽ� : �̵��ӵ�
    public float speed = 5;

    private void Start()
    {
        // ��� ���ư��� �ϸ� ���ſ����ϱ�
        // ���� 2�� �Ŀ� ȭ�鿡�� ����� 2�� �Ŀ� ���ӿ�����Ʈ�� ���丮 ����
        Invoke("DestroyBullet", 2f);
    }

    void Update()
    {
        // �������� ���ư�
        transform.Translate(transform.right * -speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
