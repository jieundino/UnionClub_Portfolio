using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenEntrance : MonoBehaviour
{
    public PlayerController controller;

    public bool isOpen = false; //플레이어가 문에 가까이 가면 문 잠긴게 자동으로 true로 바꿔서 문 열 수 있게 함

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.RightShift))
        {
            if (isOpen)
            {
                //스테이지 1 씬으로 이동
                Debug.Log("다음 스테이지로 이동");
                SceneManager.LoadScene("Stage1");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //토끼가 허가했을 때
        if(controller.isNextOpen)
        {
            // 문의 콜라이더 영역 안에 들어가 있고
            if (other.tag == "Player") //그 상대의 태그가 플레이어면
            {
                isOpen = true;
            }
        }

    }
}
