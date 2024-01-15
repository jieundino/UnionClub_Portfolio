using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2Manager : MonoBehaviour
{
    // ��ѱ�
    public Sprite front_img;
    public Sprite back_img;

    public GameObject pigeon1;
    public GameObject pigeon2;
    public GameObject pigeon3;

    public bool isChange = false;   // �� ������ �̿��Ͽ� pigeonMove�޼ҵ� �ϳ������� �̹��� �ٲٰ� ��.

    public float startTime; // �ʹ� ������ ���� ���� �ð�
    public float delayTime;     // ��ѱⰡ �ڵ��ƺ��� ��

    // �Ÿ� ��Ÿ���ִ� UI
    GameObject player;
    GameObject flag;
    public Slider distance;

    void Start()
    {
        player = GameObject.Find("Dust");
        flag = GameObject.Find("flag");

        // ó������ startTime���� ��ѱⰡ ���� �ôٰ� startTime�ð� ������ ���� ��.
        StartCoroutine("pigeonRepeat", startTime);
        // �� ������ 4�ʸ��� �� ���ƺ�.
    }

    void Update()
    {
        // �÷��̾�� �÷��ױ����� �Ÿ� ���
        float length = flag.transform.position.x - player.transform.position.x;
        // length�� �����̴��� �ݿ���.
        distance.value = 60 - length;
    }

    public void pigeonMove()
    {
        if(!isChange)
        {
            // A������ ����
            // B������ ����
            pigeon1.GetComponent<SpriteRenderer>().sprite = front_img;
            pigeon2.GetComponent<SpriteRenderer>().sprite = back_img;
            pigeon3.GetComponent<SpriteRenderer>().sprite = front_img;

            isChange=true;
        }
        else 
        {
            // A������ ����
            // B������ ����
            pigeon1.GetComponent<SpriteRenderer>().sprite = back_img;
            pigeon2.GetComponent<SpriteRenderer>().sprite = front_img;
            pigeon3.GetComponent<SpriteRenderer>().sprite = back_img;

            isChange = false;
        }
    }

    IEnumerator pigeonRepeat(float start)
    {
        pigeonMove();
        yield return new WaitForSeconds(start);
        StartCoroutine("pigeonRepeat", delayTime);      // delayTime���� ��ѱⰡ �� ���ƺ�.
    }
}
