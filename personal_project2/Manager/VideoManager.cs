using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    // 비디오가 끝나면 자동으로 다음 씬으로 이동함.

    public VideoPlayer vid;
    void Start() { vid.loopPointReached += CheckOver; }
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        print("Video Is Over");
        if (SceneManager.GetActiveScene().name == "OpeningMov")
        {
            SceneManager.LoadScene("Stage1");
        }
        else
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    // 영상 스킵 버튼
    public void skipBtn()
    {
        if (SceneManager.GetActiveScene().name == "OpeningMov")
        {
            SceneManager.LoadScene("Stage1");
        }
        else
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    private void Update()
    {
        //씬멈춰있으면 일시정지
        if (Time.timeScale == 0)
        {
            vid.Pause();
        }
        else if (Time.timeScale == 1)  //Time.timeScale이 1로 씬이 멈춰있지 않으면 일시정지 해제
        {
            vid.Play();
        }
    }
}
