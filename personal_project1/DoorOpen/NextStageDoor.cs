using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageDoor : MonoBehaviour
{
    //다음 스테이지로 가는 문은 미션 성공하고 gameManager.isSuccess = true;으로 바뀌고 여기서 엔피씨와 대화하면 문이 생기게 할 거임
    public bool isOpen = false; //플레이어가 문에 가까이 가면 문 잠긴게 자동으로 true로 바꿔서 문 열 수 있게 함
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //쉬프트 키 눌렀을 때
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (isOpen)
            {
                // 현재 씬 뭔지 알아내기 위해 가져옴
                Scene scene = SceneManager.GetActiveScene();
                // 씬 이름 확인 후 다음 스테이지로 이동
                if(scene.name=="Stage1")
                {
                    SceneManager.LoadScene("Stage2");
                }
                else if(scene.name == "Stage2")
                {
                    SceneManager.LoadScene("Stage3");
                }
                else if (scene.name == "Stage3")
                {
                    SceneManager.LoadScene("Forest Exit");
                }
                Debug.Log("다음 스테이지로 이동");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // 문의 콜라이더 영역 안에 들어가 있고
        if (other.tag == "Player") //그 상대의 태그가 플레이어면
        {
            isOpen = true;  //열 수 있게 함
        }
    }
}
