using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(SceneManager.GetActiveScene().name == "Stage2")
            {
                Debug.Log("골인! 스테이지2 클리어.");
                // 스테이지3 으로 이동
                SceneManager.LoadScene("Stage3");
            }
            else if(SceneManager.GetActiveScene().name == "Stage3")
            {
                Debug.Log("스테이지3 클리어.");
                // 스테이지4 으로 이동
                SceneManager.LoadScene("Stage4");
            }
            else if (SceneManager.GetActiveScene().name == "Stage4")
            {
                Debug.Log("골인! 스테이지4 클리어.");
                // 스테이지5 으로 이동
                SceneManager.LoadScene("Stage5");
            }
            else if (SceneManager.GetActiveScene().name == "Stage5")
            {
                Debug.Log("스테이지5 클리어. 엔딩으로 이동.");
                // EndingMov 으로 이동
                SceneManager.LoadScene("EndingMov");
            }
        }
    }
}
