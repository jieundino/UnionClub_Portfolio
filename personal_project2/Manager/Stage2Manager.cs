using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2Manager : MonoBehaviour
{
    // 비둘기
    public Sprite front_img;
    public Sprite back_img;

    public GameObject pigeon1;
    public GameObject pigeon2;
    public GameObject pigeon3;

    public bool isChange = false;   // 이 변수를 이용하여 pigeonMove메소드 하나만으로 이미지 바꾸게 함.

    public float startTime; // 초반 시작할 때만 쓰는 시간
    public float delayTime;     // 비둘기가 뒤돌아보는 텀

    // 거리 나타내주는 UI
    GameObject player;
    GameObject flag;
    public Slider distance;

    void Start()
    {
        player = GameObject.Find("Dust");
        flag = GameObject.Find("flag");

        // 처음에만 startTime동안 비둘기가 앞을 봤다가 startTime시간 끝나면 뒤쪽 봄.
        StartCoroutine("pigeonRepeat", startTime);
        // 그 다음엔 4초마다 뒤 돌아봄.
    }

    void Update()
    {
        // 플레이어와 플래그까지의 거리 계산
        float length = flag.transform.position.x - player.transform.position.x;
        // length를 슬라이더에 반영함.
        distance.value = 60 - length;
    }

    public void pigeonMove()
    {
        if(!isChange)
        {
            // A영역은 켜짐
            // B영역은 꺼짐
            pigeon1.GetComponent<SpriteRenderer>().sprite = front_img;
            pigeon2.GetComponent<SpriteRenderer>().sprite = back_img;
            pigeon3.GetComponent<SpriteRenderer>().sprite = front_img;

            isChange=true;
        }
        else 
        {
            // A영역은 꺼짐
            // B영역은 켜짐
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
        StartCoroutine("pigeonRepeat", delayTime);      // delayTime마다 비둘기가 뒤 돌아봄.
    }
}
