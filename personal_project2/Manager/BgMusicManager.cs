using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgMusicManager : MonoBehaviour
{
    // 배경음악 관리하는 스크립트

    // 배경음악
    public AudioClip Stage1BGM;
    public AudioClip Stage2BGM;
    public AudioClip Stage3BGM;
    public AudioClip Stage4BGM;
    public AudioClip Stage5BGM;

    AudioSource audioSource;

    string previousScene;   //원래 저장되어 있던 씬 이름.
    string presentScene;    //씬이 바뀔 때마다 현재 씬의 이름이 추가될 변수.

    string presentMusic;

    #region singleton
    private void Awake()
    {
        var obj = FindObjectsOfType<BgMusicManager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //열리자마자 오디오소스 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
        // previousScene에 가장 처음에 함수가 실행될 때의 씬의 이름 저장.
        previousScene = SceneManager.GetActiveScene().name;
    }
    #endregion singleton

    // 새로운 씬을 추가
    void OnEnable()     //wake/Start와 달리 활성화 될 때마다 호출되는 함수
    {
        // 씬 매니저의 sceneLoaded에 델리게이트 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 새로운 씬에 아래 내용을 새로 호출. 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("씬 교체됨, 현재 씬: " + scene.name);
        Debug.Log(mode);

        // 교체된 현재 씬의 이름을 저장함.
        presentScene = scene.name;
    }

    // 게임 종료 시
    void OnDisable()    // 비활성화 될 때마다 호출되는 함수
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        // 이전에 저장되어 있던 씬의 이름과 현재 불러온 씬의 이름이 다르면
        if (previousScene != presentScene)
        {
            // 씬 이름 확인 후 해당하는 브금 재생
            if (presentScene == "OpeningMov")
            {
                PlaySoundBG("STAGE1");
                Debug.Log("여기는 OpeningMov. 'STAGE1'브금 플레이 ");
            }
            else if (presentScene == "Stage1")
            {
                if (presentMusic == "stage1")
                {
                    return;
                }
                PlaySoundBG("STAGE1");
                Debug.Log("여기는 Stage1. 'STAGE1'브금 플레이 ");
            }
            else if (presentScene == "Stage2")
            {
                PlaySoundBG("STAGE2");
                Debug.Log("여기는 Stage2. 'STAGE2'브금 플레이 ");
            }
            else if (presentScene == "Stage3")
            {
                PlaySoundBG("STAGE3");
                Debug.Log("여기는 Stage3. 'STAGE3'브금 플레이 ");
            }
            else if (presentScene == "Stage4")
            {
                PlaySoundBG("STAGE4");
                Debug.Log("여기는 Stage4. 'STAGE4'브금 플레이 ");
            }
            else if (presentScene == "Stage5")
            {
                if (presentMusic == "stage5")
                {
                    return;
                }
                PlaySoundBG("STAGE5");
                Debug.Log("여기는 Stage5. 'STAGE5'브금 플레이 ");
            }
            else if (presentScene == "Stage5_Desk")
            {
                if (presentMusic == "stage5")
                {
                    return;
                }
                PlaySoundBG("STAGE5");
                Debug.Log("여기는 Stage5_Desk. 'STAGE5'브금 플레이 ");
            }
            else if (presentScene == "EndingMov")
            {
                if (presentMusic == "stage5")
                {
                    return;
                }
                PlaySoundBG("STAGE5");
                Debug.Log("여기는 EndingMov. 'STAGE5'브금 플레이 ");
            }
            else if (presentScene == "GameOver1")
            {
                audioSource.Stop();
            }
            else if (presentScene == "GameOver2")
            {
                audioSource.Stop();
            }

            // 그리고 현재 씬의 이름(presentScene)을 저장되어 있던 씬인 previousScene에 저장.
            previousScene = presentScene;
        }

        ////씬멈춰있으면 일시정지
        //if (Time.timeScale == 0)
        //{
        //    audioSource.Pause();
        //}
        //else if (Time.timeScale == 1)  //Time.timeScale이 1로 씬이 멈춰있지 않으면 일시정지 해제
        //{
        //    audioSource.UnPause();
        //}
    }

    public void BgPause()
    {
        audioSource.Pause();
    }
    public void BgUnPause()
    {
        audioSource.UnPause();
    }

    // 브금 재생 메소드
    void PlaySoundBG(string action)
    {
        switch (action)
        {
            case "STAGE1":
                audioSource.clip = Stage1BGM;
                presentMusic = "stage1";
                break;
            case "STAGE2":
                audioSource.clip = Stage2BGM;
                presentMusic = "stage2";
                break;
            case "STAGE3":
                audioSource.clip = Stage3BGM;
                presentMusic = "stage3";
                break;
            case "STAGE4":
                audioSource.clip = Stage4BGM;
                presentMusic = "stage4";
                break;
            case "STAGE5":
                audioSource.clip = Stage5BGM;
                presentMusic = "stage5";
                break;
        }
        audioSource.Play();
    }
}
