using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundEffectManager : MonoBehaviour
{
    // 효과음 관리하는 스크립트

    // 효과음
    public AudioClip Stage1SE;  // 버스 하차
    public AudioClip Stage2SE;  // 보도블록 걷는 소리
    public AudioClip Stage3SE;  // 걷는 소리
    public AudioClip Stage4SE;  // 창문 열리는 소리(거실 진입)

    AudioSource audioSource;

    string previousScene;   //원래 저장되어 있던 씬 이름.
    string presentScene;    //씬이 바뀔 때마다 현재 씬의 이름이 추가될 변수.

    string presentMusic;

    #region singleton
    private void Awake()
    {
        var obj = FindObjectsOfType<SoundEffectManager>();
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
        //Debug.Log("씬 교체됨, 현재 씬: " + scene.name);
        //Debug.Log(mode);

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
            // 씬 이름 확인 후 해당하는 효과음 재생
            if (presentScene == "Stage2")
            {
                PlaySoundSE("STAGE1");
                Debug.Log("버스 하차 효과음 재생");

            }
            else if (presentScene == "Stage3")
            {
                PlaySoundSE("STAGE2");
                Debug.Log("보도블럭 걷는 효과음 재생");

            }
            else if (presentScene == "Stage4")
            {
                PlaySoundSE("STAGE3");
                Debug.Log("마을 바닥 걷는 효과음 재생");
            }
            else if (presentScene == "Stage5")
            {
                // 이전 씬이 책상이라면 효과음 다시 재생하지 않고 그냥 리턴해서 빠져나옴.
                if (previousScene == "Stage5_Desk")
                {
                    return;
                }
                PlaySoundSE("STAGE4");
                Debug.Log("창문 열리는 효과음 재생");
            }

            // 그리고 현재 씬의 이름(presentScene)을 저장되어 있던 씬인 previousScene에 저장.
            previousScene = presentScene;
        }
    }


    // 효과음 재생 메소드
    void PlaySoundSE(string action)
    {
        switch (action)
        {
            case "STAGE1":
                audioSource.clip = Stage1SE;
                break;
            case "STAGE2":
                audioSource.clip = Stage2SE;
                break;
            case "STAGE3":
                audioSource.clip = Stage3SE;
                break;
            case "STAGE4":
                audioSource.clip = Stage4SE;
                break;
        }
        audioSource.Play();
    }
}
