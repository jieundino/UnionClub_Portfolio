using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingCloudController : MonoBehaviour
{
    Vector3 pos;            // ���� ��ġ
    float delta = 2.5f;     // �¿�� �̵� ������ x �ִ밪.
    float speed = 0.5f;     // ���� �̵��ӵ�

    public GameObject player;

    void Start()
    {
        this.pos = this.transform.position;
    }

    void Update()
    {
        Vector3 v = pos;
        v.x += delta * Mathf.Sin(Time.time * speed);    // �¿� �̵��� �ִ�ġ �� ���� ó��.
        // Mathf.Sin(Time.time*speed)�� -1 ~ 1 ������ ���� ����. �̰����� ���� ó���� ��.

        this.transform.position = v;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // �����̴� ���� ���� �ö�°� �÷��̾��
        if (other.transform.CompareTag("Player"))
            other.transform.SetParent(transform);
        // �÷��̾ �� ������ �ڽ����� �־��༭ ���� �����̰� ��.
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
            other.transform.SetParent(null);
        // ���� ������ ������ �÷��̾�� �ڽ����� �־��ذ� ������. �÷��̾� �����Ӱ� ������ �� �ְ� ��.
    }
}
