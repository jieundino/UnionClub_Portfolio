using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneDirector : MonoBehaviour
{
    void Update()
    {
        // 타이틀 씬에서
        // 아무키나 누르면 시작
        if (Input.anyKeyDown)
        {
            // 여우의 방 씬으로 이동.
            SceneManager.LoadScene("HomeBefore");
        }

    }
}
