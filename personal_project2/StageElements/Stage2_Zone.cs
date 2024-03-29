using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2_Zone : MonoBehaviour
{
    // Stage2Manager 스크립트의 isChange 변수가 true면 A영역이 활성화. false면 B영역이 활성화됨.
    public Stage2Manager stage2Manager;     
    
    // 이 스크립트가 붙은 영역의 이름을 가져와서 넣음. 영역이 A와 B로 구분되어 있기 때문.
    string ZoneName;

    // 영역 A와 B가 나뉜 이유 : 난이도 향상을 위해 비둘기 3마리가 엇갈리게 뒤돌기 때문.
    // 영역 A : 비둘기1, 비둘기 3가 같은 타이밍에 플레이어쪽을 봄. 비둘기2는 제외됨.
    // 영역 B : 비둘기2가 나머지 비둘기들이 앞을 보고 있을 때 플레이어 쪽을 봄.

    void Start()
    {
        ZoneName = this.gameObject.name;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (stage2Manager.isChange)  // A영역이 활성화됨.
        {
            if (ZoneName == "A")
            {
                if (other.gameObject.tag == "Player")
                {
                    Debug.Log("A영역에서 비둘기에게 걸림! 게임오버.");
                    // 게임오버2 씬으로 이동
                    SceneManager.LoadScene("GameOver2");
                }

            }
            else
            {
                //B 영역일 때
                return;
                // 리턴됨.
            }
        }
        else  // B영역이 활성화됨.
        {
            if (ZoneName == "B")
            {
                if (other.gameObject.tag == "Player")
                {
                    Debug.Log("B 영역에서 비둘기에게 걸림! 게임오버.");
                    // 게임오버2 씬으로 이동
                    SceneManager.LoadScene("GameOver2");
                }
            }
            else
            {
                //A 영역일 때
                return;
                // 리턴됨.
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (stage2Manager.isChange)  // A영역이 활성화됨.
        {
            if (ZoneName == "A")
            {
                if (other.gameObject.tag == "Player")
                {
                    Debug.Log("A영역에서 비둘기에게 걸림! 게임오버.");
                    // 게임오버2 씬으로 이동
                    SceneManager.LoadScene("GameOver2");
                }
            }
            else
            {
                //B 영역일 때
                return;         // 리턴됨.
            }
        }
        else  // B영역이 활성화됨.
        {
            if (ZoneName == "B")
            {
                if (other.gameObject.tag == "Player")
                {
                    Debug.Log("B 영역에서 비둘기에게 걸림! 게임오버.");
                    // 게임오버2 씬으로 이동
                    SceneManager.LoadScene("GameOver2");
                }

            }
            else
            {
                //A 영역일 때
                return;         // 리턴됨.
            }
        }
    }

}
