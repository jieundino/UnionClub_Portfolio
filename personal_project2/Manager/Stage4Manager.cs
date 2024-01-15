using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage4Manager : MonoBehaviour
{
    // �Ÿ� ��Ÿ���ִ� UI
    GameObject player;
    GameObject flag;
    public Slider distance;

    void Start()
    {
        player = GameObject.Find("Dust");
        flag = GameObject.Find("Goal");
    }

    void Update()
    {
        // �÷��̾�� �÷��ױ����� �Ÿ� ���
        float length = flag.transform.position.y - player.transform.position.y;
        // length�� �����̴��� �ݿ���.
        distance.value = 55 - length;
    }
}
