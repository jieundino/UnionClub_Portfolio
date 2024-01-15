using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgMusicManager : MonoBehaviour
{
    // ������� �����ϴ� ��ũ��Ʈ

    // �������
    public AudioClip Stage1BGM;
    public AudioClip Stage2BGM;
    public AudioClip Stage3BGM;
    public AudioClip Stage4BGM;
    public AudioClip Stage5BGM;

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
            if (presentScene == "OpeningMov")
            {
                PlaySoundBG("STAGE1");
                Debug.Log("����� OpeningMov. 'STAGE1'��� �÷��� ");
            }
            else if (presentScene == "Stage1")
            {
                if (presentMusic == "stage1")
                {
                    return;
                }
                PlaySoundBG("STAGE1");
                Debug.Log("����� Stage1. 'STAGE1'��� �÷��� ");
            }
            else if (presentScene == "Stage2")
            {
                PlaySoundBG("STAGE2");
                Debug.Log("����� Stage2. 'STAGE2'��� �÷��� ");
            }
            else if (presentScene == "Stage3")
            {
                PlaySoundBG("STAGE3");
                Debug.Log("����� Stage3. 'STAGE3'��� �÷��� ");
            }
            else if (presentScene == "Stage4")
            {
                PlaySoundBG("STAGE4");
                Debug.Log("����� Stage4. 'STAGE4'��� �÷��� ");
            }
            else if (presentScene == "Stage5")
            {
                if (presentMusic == "stage5")
                {
                    return;
                }
                PlaySoundBG("STAGE5");
                Debug.Log("����� Stage5. 'STAGE5'��� �÷��� ");
            }
            else if (presentScene == "Stage5_Desk")
            {
                if (presentMusic == "stage5")
                {
                    return;
                }
                PlaySoundBG("STAGE5");
                Debug.Log("����� Stage5_Desk. 'STAGE5'��� �÷��� ");
            }
            else if (presentScene == "EndingMov")
            {
                if (presentMusic == "stage5")
                {
                    return;
                }
                PlaySoundBG("STAGE5");
                Debug.Log("����� EndingMov. 'STAGE5'��� �÷��� ");
            }
            else if (presentScene == "GameOver1")
            {
                audioSource.Stop();
            }
            else if (presentScene == "GameOver2")
            {
                audioSource.Stop();
            }

            // �׸��� ���� ���� �̸�(presentScene)�� ����Ǿ� �ִ� ���� previousScene�� ����.
            previousScene = presentScene;
        }

        ////������������ �Ͻ�����
        //if (Time.timeScale == 0)
        //{
        //    audioSource.Pause();
        //}
        //else if (Time.timeScale == 1)  //Time.timeScale�� 1�� ���� �������� ������ �Ͻ����� ����
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

    // ��� ��� �޼ҵ�
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
