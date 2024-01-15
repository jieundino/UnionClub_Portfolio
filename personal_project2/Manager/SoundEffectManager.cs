using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundEffectManager : MonoBehaviour
{
    // ȿ���� �����ϴ� ��ũ��Ʈ

    // ȿ����
    public AudioClip Stage1SE;  // ���� ����
    public AudioClip Stage2SE;  // ������� �ȴ� �Ҹ�
    public AudioClip Stage3SE;  // �ȴ� �Ҹ�
    public AudioClip Stage4SE;  // â�� ������ �Ҹ�(�Ž� ����)

    AudioSource audioSource;

    string previousScene;   //���� ����Ǿ� �ִ� �� �̸�.
    string presentScene;    //���� �ٲ� ������ ���� ���� �̸��� �߰��� ����.

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
        //Debug.Log("�� ��ü��, ���� ��: " + scene.name);
        //Debug.Log(mode);

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
            // �� �̸� Ȯ�� �� �ش��ϴ� ȿ���� ���
            if (presentScene == "Stage2")
            {
                PlaySoundSE("STAGE1");
                Debug.Log("���� ���� ȿ���� ���");

            }
            else if (presentScene == "Stage3")
            {
                PlaySoundSE("STAGE2");
                Debug.Log("������ �ȴ� ȿ���� ���");

            }
            else if (presentScene == "Stage4")
            {
                PlaySoundSE("STAGE3");
                Debug.Log("���� �ٴ� �ȴ� ȿ���� ���");
            }
            else if (presentScene == "Stage5")
            {
                // ���� ���� å���̶�� ȿ���� �ٽ� ������� �ʰ� �׳� �����ؼ� ��������.
                if (previousScene == "Stage5_Desk")
                {
                    return;
                }
                PlaySoundSE("STAGE4");
                Debug.Log("â�� ������ ȿ���� ���");
            }

            // �׸��� ���� ���� �̸�(presentScene)�� ����Ǿ� �ִ� ���� previousScene�� ����.
            previousScene = presentScene;
        }
    }


    // ȿ���� ��� �޼ҵ�
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
