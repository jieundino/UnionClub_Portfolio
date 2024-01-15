using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;

    //��ȭâ
    public GameObject talkPanel;
    public Text talkText;
    public int talkIndex;
    public bool isAction;
    public GameObject scanObject;   // �÷��̾�κ��� ��ĵ������Ʈ�� ���޹��� ����.

    public bool isMeet =false; // ������ �����ϸ� �� ������ true�� �ٲ��ְ� talkData_before
                               // true�̸� ���� ��ȭ ������ ������.        talkData_after

    // ��������1,3 �Ŵ��� ���� ������
    public bool isGet = false;  // ���ӸŴ������� ��ȭ �� ������ isGet true�� ���ֱ�. ��������1�Ŵ��� ��ũ��Ʈ���� �̿��� ����.
                                // +��������3���� �渷�̿� ��ȭ�ϰ� ���� �̰� Ʈ��� Ǯ���༭ ��������3���� ����Ʈ �����ϰ� ��.
    public bool isTry = false;  // ������ ������ ī�� �ܸ��� ��ȭ�� �� ������ ��� ���� ������.

    public Stage3Manager stage3Manager;

    // ���Ӹ޴�
    public GameObject menuSet;

    // ���Ӹ޴� ������ ���� ������ ���� �����ϴ� ����
    bool isPause;

    // ��� �Ŵ���
    public BgMusicManager bgMusic;

    private void Start()
    {
        bgMusic = FindObjectOfType<BgMusicManager>();
    }

    private void Update()
    {
        // Esc��ư ������ ���� �޴� ����
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuSet.activeSelf) //���� �޴� Ȱ��ȭ �� ����
            {
                isPause = false;
                menuSet.SetActive(false);
            }
            else //���� �޴� ��Ȱ��ȭ�� ����
            {
                isPause = true;
                menuSet.SetActive(true);
            }
        }

        if (isPause)
        {
            // ���� ���߱�
            Time.timeScale = 0;
        }
        else
        {
            // ���� ���ߴ� �� Ǯ��
            Time.timeScale = 1;
        }

        //������������ ��� �Ͻ�����
        if (Time.timeScale == 0)
        {
            bgMusic.BgPause();
        }
        else if (Time.timeScale == 1)  //Time.timeScale�� 1�� ���� �������� ������ ��� �Ͻ����� ����
        {
            bgMusic.BgUnPause();
        }
    }

    //����ϱ� ��ư ������ ���� ���
    public void GameContinue()
    {
        isPause = false;
        menuSet.SetActive(false);
    }

    //�����ϱ� ��ư ������ ���� ����
    public void GameExit()
    {
        Application.Quit();
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;

        // ��ĵ�� ������Ʈ�� id�� isNPC������ �����;� �ϱ� ������ objData script �ʿ�.
        ObjData objData = scanObject.GetComponent<ObjData>();
        //objData�� id ������ �Ű������� �ѱ�.
        Talk(objData.id);

        talkPanel.SetActive(isAction); //��ȭâ Ȱ��ȭ ���¿� ���� ��ȭâ Ȱ��ȭ ����
    }

    void Talk(int id)
    {
        // ������ �����ϱ� ���̸� GetTalk1 ��ȭ ���� ������.
        // ���� �����ϰ� isMeet �ٲ�� GetTalk2 ��ȭ ���� ������

        // ������ ������ ������Ʈ�� npc��
        if (id >= 100)    //���� id�� 100���� �̷��� ������ ����.
        {
            if (!isMeet)  // ���� ���� ��
            {
                string talkData = talkManager.GetTalk1(id, talkIndex);

                if (talkData == null) //��ȯ�� ���� null�̸� ���̻� ���� ��簡 �����Ƿ� action���º����� false�� ���� 
                {
                    // ��������1
                    if (SceneManager.GetActiveScene().name == "Stage1" && id < 140)
                    {
                        isGet = true;
                    }
                    // ��������3
                    else if (SceneManager.GetActiveScene().name == "Stage3" && id == 1000)  // �渷�̿� ��ȭ �� �̼� �����ϰ� �ϱ�.
                    {
                        isGet = true;
                    }

                    isAction = false;
                    talkIndex = 0; //talk�ε����� ������ �� ���ǹǷ� �ʱ�ȭ
                    return; //void������ return �Լ� �������� (���� �ڵ�� ������� ����)
                }

                talkText.text = talkData;

                //���� ������ �������� ���� talkData�� �ε����� �ø�
                isAction = true;    //��簡 ���������Ƿ� ��� ������.
                talkIndex++;

            }
            else //���� ������ ���� ��ȭ
            {
                string talkData = talkManager.GetTalk2(id, talkIndex);

                if (talkData == null) //��ȯ�� ���� null�̸� ���̻� ���� ��簡 �����Ƿ� action���º����� false�� ���� 
                {
                    // ��������1
                    if (SceneManager.GetActiveScene().name == "Stage1" && id == 140)
                    {
                        isTry = true;
                    }
                    // ��������3
                    if (SceneManager.GetActiveScene().name == "Stage3" && id == 1000)
                    {
                        stage3Manager.Illust2Fade();
                        stage3Manager.isComplete = true;
                    }
                    else if(SceneManager.GetActiveScene().name == "Stage3" && id == 2000)
                    {
                        stage3Manager.isBlackTalk = true;
                    }

                    isAction = false;
                    talkIndex = 0; //talk�ε����� ������ �� ���ǹǷ� �ʱ�ȭ
                    return; //void������ return �Լ� �������� (���� �ڵ�� ������� ����)
                }

                talkText.text = talkData;

                //���� ������ �������� ���� talkData�� �ε����� �ø�
                isAction = true;    //��簡 ���������Ƿ� ��� ������.
                talkIndex++;
            }
        }
        else // �ܼ� ������Ʈ��(�׳� GetTalk���� �������� �͵�)
        {
            string talkData = talkManager.GetTalk(id, talkIndex);

            if (talkData == null) //��ȯ�� ���� null�̸� ���̻� ���� ��簡 �����Ƿ� action���º����� false�� ���� 
            {
                isAction = false;
                talkIndex = 0; //talk�ε����� ������ �� ���ǹǷ� �ʱ�ȭ
                return; //void������ return �Լ� �������� (���� �ڵ�� ������� ����)
            }

            talkText.text = talkData;

            //���� ������ �������� ���� talkData�� �ε����� �ø�
            isAction = true;    //��簡 ���������Ƿ� ��� ������.
            talkIndex++;
        }
    }

    public void S1FailAction()    //��������1���� ���� �׼�
    {
        talkText.text = "...�̰� �ƴ� ����̴�. �ٸ� ���� ã�ƺ���.";

        talkPanel.SetActive(true); //��ȭâ Ȱ��ȭ ���¿� ���� ��ȭâ Ȱ��ȭ ����
    }
    public void S1FailActionClose()    //��������1���� ���� �׼�
    {
        talkPanel.SetActive(false); //��ȭâ Ȱ��ȭ ���¿� ���� ��ȭâ Ȱ��ȭ ����
    }

}
