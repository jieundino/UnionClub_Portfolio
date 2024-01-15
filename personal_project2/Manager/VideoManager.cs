using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    // ������ ������ �ڵ����� ���� ������ �̵���.

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

    // ���� ��ŵ ��ư
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
        //������������ �Ͻ�����
        if (Time.timeScale == 0)
        {
            vid.Pause();
        }
        else if (Time.timeScale == 1)  //Time.timeScale�� 1�� ���� �������� ������ �Ͻ����� ����
        {
            vid.Play();
        }
    }
}
