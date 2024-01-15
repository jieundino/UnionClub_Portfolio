using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage4Manager : MonoBehaviour
{
    // 거리 나타내주는 UI
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
        // 플레이어와 플래그까지의 거리 계산
        float length = flag.transform.position.y - player.transform.position.y;
        // length를 슬라이더에 반영함.
        distance.value = 55 - length;
    }
}
