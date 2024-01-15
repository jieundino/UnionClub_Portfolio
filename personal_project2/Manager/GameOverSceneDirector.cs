using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 게임오버씬 관리함
public class GameOverSceneDirector : MonoBehaviour
{
    void Update()
    {
        // 아무키나 누르면
        if (Input.anyKeyDown)
        {
            //타이틀 씬으로 이동.
            SceneManager.LoadScene("TitleScene");
        }
    }
}
