using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneDirector : MonoBehaviour
{
    public void StartBtn()
    {
        // 타이틀 씬에서
        // 시작하기 버튼 누르면 시작
        // OpeningMov 씬으로 이동
        SceneManager.LoadScene("OpeningMov");
    }
}
