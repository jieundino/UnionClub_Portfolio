using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSound : MonoBehaviour
{
    // �Ҹ� �����ϴ� ��ũ��Ʈ

    // �� ������ �Ҹ�
    public AudioClip DoorOpen;

    AudioSource audioSource;

    string previousScene;   //���� ����Ǿ� �ִ� �� �̸�.
    string presentScene;    //���� �ٲ� ������ ���� ���� �̸��� �߰��� ����.

    string presentMusic;

    #region singleton
    private void Awake()
    {
        var obj = FindObjectsOfType<DoorSound>();
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
            if (presentScene == "StudyRoom"|| presentScene == "BathRoom")
            {
                if (previousScene == "Game Main Scene")
                {
                    PlaySoundEffect("DOOR");
                }
            }
            else if (presentScene == "Game Main Scene")
            {
                if (previousScene == "StudyRoom" || previousScene == "BathRoom")
                {
                    PlaySoundEffect("DOOR");
                }
            }
            else if (presentScene == "GameEnding")
            {
                PlaySoundEffect("DOOR");

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
            case "DOOR":
                audioSource.clip = DoorOpen;
                //presentMusic = "mainHome";
                break;
        }
        audioSource.Play();
    }
}
