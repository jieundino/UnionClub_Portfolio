using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgMusicManager : MonoBehaviour
{
    // ������� �����ϴ� ��ũ��Ʈ

    // �������
    public AudioClip BgHome;
    public AudioClip BgForest;

    AudioSource audioSource;

    string previousScene;   //���� ����Ǿ� �ִ� �� �̸�.
    string presentScene;    //���� �ٲ� ������ ���� ���� �̸��� �߰��� ����.
    private void Awake()
    {
        //�����ڸ��� ������ҽ� ������Ʈ ��������
        audioSource = GetComponent<AudioSource>();
        // previousScene�� ���� ó���� �Լ��� ����� ���� ���� �̸� ����.
        previousScene = SceneManager.GetActiveScene().name;
    }

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
        if(previousScene!= presentScene)
        {
            // �� �̸� Ȯ�� �� �ش��ϴ� ��� ���
            if (presentScene == "HomeBefore")
            {
                PlaySoundEffect("HOME"); 
                Debug.Log("����� HomeBefore. 'HOME'��� �÷��� ");
            }
            else if (presentScene == "Forest Entrance")
            {
                PlaySoundEffect("FOREST");
                Debug.Log("����� Forest Entrance. 'FOREST'��� �÷��� ");

            }
            else if (presentScene == "HomeAfter")
            {
                PlaySoundEffect("HOME");
                Debug.Log("����� HomeAfter. 'HOME'��� �÷��� ");
            }
            else if (presentScene == "TitleScene")
            {
                audioSource.Stop();
            }
            else if (presentScene == "GameOver")
            {
                audioSource.Stop();
            }

            // �׸��� ���� ���� �̸�(presentScene)�� ����Ǿ� �ִ� ���� previousScene�� ����.
            previousScene = presentScene;
        }

        //������������ �Ͻ�����
        if (Time.timeScale==0)
        {
            audioSource.Pause();
        }
        else if(Time.timeScale==1)  //Time.timeScale�� 1�� ���� �������� ������ �Ͻ����� ����
        {
            audioSource.UnPause();
        }
    }

    // ȿ���� ��� �޼ҵ�
    void PlaySoundEffect(string action)
    {
        switch (action)
        {
            case "HOME":
                audioSource.clip = BgHome;
                break;
            case "FOREST":
                audioSource.clip = BgForest;
                break;
        }
        audioSource.Play();
    }
}
