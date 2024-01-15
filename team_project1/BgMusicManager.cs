using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgMusicManager : MonoBehaviour
{
    // ������� �����ϴ� ��ũ��Ʈ

    // �������
    public AudioClip MainBg;
    public AudioClip BookGameBg;
    public AudioClip AnagramBg;

    AudioSource audioSource;

    string previousScene;   //���� ����Ǿ� �ִ� �� �̸�.
    string presentScene;    //���� �ٲ� ������ ���� ���� �̸��� �߰��� ����.

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

        //�����ڸ��� ������ҽ� ������Ʈ ��������
        audioSource = GetComponent<AudioSource>();
        // previousScene�� ���� ó���� �Լ��� ����� ���� ���� �̸� ����.
        previousScene = SceneManager.GetActiveScene().name;
    }
    #endregion singleton


    // ���ο� ���� �߰�
    void OnEnable()     //wake/Start�� �޸� Ȱ��ȭ �� ������ ȣ��Ǵ� �Լ�
    {
        // �� �Ŵ����� sceneLoaded�� ��������Ʈ ü���� �Ǵ�.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ���ο� ���� �Ʒ� ������ ���� ȣ��. ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("�� ��ü��, ���� ��: " + scene.name);
        Debug.Log(mode);

        // ��ü�� ���� ���� �̸��� ������.
        presentScene = scene.name;
    }

    // ���� ���� ��
    void OnDisable()    // ��Ȱ��ȭ �� ������ ȣ��Ǵ� �Լ�
    {
        // ��������Ʈ ü�� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        // ������ ����Ǿ� �ִ� ���� �̸��� ���� �ҷ��� ���� �̸��� �ٸ���
        if (previousScene != presentScene)
        {
            // �� �̸� Ȯ�� �� �ش��ϴ� ��� ���
            if (presentScene == "Game Main Scene")
            {
                if (presentMusic == "mainHome")
                {
                    return;
                }
                PlaySoundEffect("MAINHOME");
                Debug.Log("����� " + presentScene + ". 'MAINHOME'��� �÷��� ");
            }
            else if (presentScene == "Bookgame")
            {
                PlaySoundEffect("BOOKGAME");
                Debug.Log("����� "+ presentScene + ". 'BOOKGAME'��� �÷��� ");
            }
            else if(previousScene== "Bookgame")
            {
                PlaySoundEffect("MAINHOME");
                Debug.Log("����� " + presentScene + ". 'MAINHOME'��� �÷��� ");
            }
            else if (presentScene == "AnagramStart")
            {
                // �ֳʱ׷� �� �ٲ� �ֳʱ׷� ��� ��� ���(�ֳʱ׷� ���ӿ� �ѿ�����)
                if (presentMusic == "anagram")
                {
                    return;
                }
                PlaySoundEffect("ANAGRAM");
                Debug.Log("����� AnagramGame. 'ANAGRAM'��� �÷��� ");
            }

            // �ֳʱ׷� ������ ������ ��ư ������ ���� ���� ������ ���ƿ��鼭 ���Ӹ��ξ� ��� ������ִ°�
            else if (presentScene == "Game Main Scene")
            {
                if(previousScene == "AnagramGame" || previousScene == "AnagramGame2" || previousScene == "AnagramGame3")
                {
                    PlaySoundEffect("MAINHOME");
                    Debug.Log("����� " + presentScene + ". 'MAINHOME'��� �÷��� ");
                }
               
            }
            

            //else if (presentScene == "TitleScene")
            //{
            //    audioSource.Stop();
            //}
            //else if (presentScene == "GameOver")
            //{
            //    audioSource.Stop();
            //}

            // �׸��� ���� ���� �̸�(presentScene)�� ����Ǿ� �ִ� ���� previousScene�� ����.
            previousScene = presentScene;
        }

        //������������ �Ͻ�����
        if (Time.timeScale == 0)
        {
            audioSource.Pause();
        }
        else if (Time.timeScale == 1)  //Time.timeScale�� 1�� ���� �������� ������ �Ͻ����� ����
        {
            audioSource.UnPause();
        }
    }

    // ȿ���� ��� �޼ҵ�
    void PlaySoundEffect(string action)
    {
        switch (action)
        {
            case "MAINHOME":
                audioSource.clip = MainBg;
                presentMusic = "mainHome";
                break;
            case "BOOKGAME":
                audioSource.clip = BookGameBg;
                presentMusic = "bookGame";
                break;
            case "ANAGRAM":
                audioSource.clip = AnagramBg;
                presentMusic = "anagram";
                break;
        }
        audioSource.Play();
    }
}
