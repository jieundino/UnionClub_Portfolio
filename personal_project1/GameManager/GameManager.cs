using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public PlayerController player;
    public TalkManager talkManager;

    //��ȭâ
    public GameObject talkPanel;
    public Text talkText;
    public int talkIndex;
    public bool isAction;
    public GameObject scanObject;   // �÷��̾�κ��� ��ĵ������Ʈ�� ���޹��� ����.
    
    public bool isSuccess; // ������������ �̼��� Ŭ���� ������ �Ϸ��ϸ� �� ������ true�� �ٲ��ְ�
                           // true�̸� ���� ��ȭ ������ ������.

    private ForestEntrance_PlayerFirstTalk forestEntrance_PlayerFirstTalk;  //ó�� �� �Ա��� �������� �� �÷��̾��� ù ��ȭ

    // ���Ǿ��� �ִϸ����͸� �Ѱ�����.
    //�ִϸ����� ������ ���� ���� 
    [SerializeField] private Animator NpcAni;
    [SerializeField] private GameObject Npc;

    // �÷��̾��� hp
    public int hp;

    // UI â���� �÷��̾��� hp�ٸ� ������
    public Image[] UiHp;    //�̹����� 5���� �迭�� �����

    // ���Ӹ޴�
    public GameObject menuSet;

    // ���Ӹ޴� ������ ���� ������ ���� �����ϴ� ����
    bool isPause;

    // Start is called before the first frame update
    void Start()
    {
        hp = 5;    // ���� ������ �Ѿ ������ 5ĭ���� �ʱ�ȭ.(���̵��� ����)
        isSuccess = false;

        if((FindObjectOfType<ForestEntrance_PlayerFirstTalk>())!=null)  //������ �ֱ�. ���� ������
            forestEntrance_PlayerFirstTalk = FindObjectOfType<ForestEntrance_PlayerFirstTalk>();

        //player = FindObjectOfType<PlayerController>();

        if (GameObject.FindWithTag("NPC")!=null)   // npc�� ������
        {
            Npc = GameObject.FindWithTag("NPC"); //�ش���� ���̶�Ű�� �ִ� NPC�� ã�Ƽ� ����
            NpcAni = Npc.GetComponent<Animator>();   //�׸��� �� NPC ������Ʈ�� �ִ� �ִϸ����͸� ã�Ƽ� ���� 
            
        }
        //talkIndex = 0;
        // ������ ���� �Ŵ��� ���ο� ���� �ε� �� ������ ���� ���� �Ǿ AllClear() �޼ҵ� �ᵵ �δ� ����.
        AllClear();
    }

    private void Update()
    {
        // Esc��ư ������ ���� �޴� ����
        if(Input.GetButtonDown("Cancel"))
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

        if(isPause)
        {
            // ���� ���߱�
            Time.timeScale = 0;
        }
        else
        {
            // ���� ���ߴ� �� Ǯ��
            Time.timeScale = 1;
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

    public void Talk(int id)
    {
        // �̼��� Ŭ�����ϱ� ���̸� GetTalk1 ��ȭ ���� ������.
        // �̼� Ŭ�����ϰ� isSuccess�� �ٲ�� GetTalk2 ��ȭ ���� ������

        if(!isSuccess)  // �̼� Ŭ���� ��
        {
            // npc���� ��ȭ
            if(id>=1000)    //npc���� id�� 1000�̻��̶� ������ �̷��� ����
            {
                string talkData = talkManager.GetTalk1(id, talkIndex);

                if (talkData == null) //��ȯ�� ���� null�̸� ���̻� ���� ��簡 �����Ƿ� action���º����� false�� ���� 
                {
                    // ��ȭ�� ������ npc�� �ִϸ��̼��� �������� ��ȯ
                    NpcAni.SetBool("isTalking", false);
                    isAction = false;
                    talkIndex = 0; //talk�ε����� ������ �� ���ǹǷ� �ʱ�ȭ
                    return; //void������ return �Լ� �������� (���� �ڵ�� ������� ����)
                }

                talkText.text = talkData;

                //���� ������ �������� ���� talkData�� �ε����� �ø�
                isAction = true;    //��簡 ���������Ƿ� ��� ������.
                                    // ��ȭ���� npc�� �ִϸ��̼��� ���̵�� ��ȯ
                NpcAni.SetBool("isTalking", true);
                talkIndex++;
            }
            else // �÷��̾��� ��� ��, npc ������ ������Ʈ
            {
                string talkData = talkManager.GetTalk1(id, talkIndex);

                if (talkData == null) //��ȯ�� ���� null�̸� ���̻� ���� ��簡 �����Ƿ� action���º����� false�� ���� 
                {
                    if ((FindObjectOfType<ForestEntrance_PlayerFirstTalk>()) != null)
                    {
                        if (!forestEntrance_PlayerFirstTalk.isStartLine) //���� �ڱ� ȥ�ڼ� �ϴ� ��� �� ������
                        {
                            forestEntrance_PlayerFirstTalk.isStartLine = true;  //true�� �ٲ㼭 �ڱ��� ��縦 �ݺ����� �ʰ� ��.
                        }
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
        else //Ŭ���� ���� ��ȭ
        {
            // npc���� ��ȭ
            if (id >= 1000)    //npc���� id�� 1000�̻��̶� ������ �̷��� ����
            {
                string talkData = talkManager.GetTalk2(id, talkIndex);

                if (talkData == null) //��ȯ�� ���� null�̸� ���̻� ���� ��簡 �����Ƿ� action���º����� false�� ���� 
                {
                    // ��ȭ�� ������ npc�� �ִϸ��̼��� �������� ��ȯ
                    NpcAni.SetBool("isTalking", false);
                    isAction = false;
                    talkIndex = 0; //talk�ε����� ������ �� ���ǹǷ� �ʱ�ȭ
                    return; //void������ return �Լ� �������� (���� �ڵ�� ������� ����)
                }

                talkText.text = talkData;

                //���� ������ �������� ���� talkData�� �ε����� �ø�
                isAction = true;    //��簡 ���������Ƿ� ��� ������.
                                    // ��ȭ���� npc�� �ִϸ��̼��� ���̵�� ��ȯ
                NpcAni.SetBool("isTalking", true);
                talkIndex++;
            }
            else // �÷��̾��� ��� ��, npc ������ ������Ʈ
            {
                string talkData = talkManager.GetTalk2(id, talkIndex);

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

    }

    public void HpDown()
    {
        if (hp > 0)
        {
            hp -= 1;
            UiHp[hp].color = new Color(1, 1, 1, 0.3f);
        }
        if(hp==0)
        {
            // �÷��̾��� hp�� 0�� �Ǹ� ���� ���� ������ �̵�
            Debug.Log("���ӿ���");
            SceneManager.LoadScene("GameOver");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� ������ ���� ������ �����ؼ� ���ӸŴ��� ���� ������Ʈ�� �ִ� �ڽ� �ݶ��̴��� ������
        if (collision.gameObject.tag=="Player")
        {
            // ���������� ������ ������ �����ؼ� hp ���.
            HpDown();
            // �׸��� �÷��̾� ����ġ�� �Ű��ֱ�
            collision.transform.position = new Vector3(0, 3, 0);
        }
    }


    void AllClear()
    {
        // ������ �� �̸��� �޾ƿͼ� �� �ⱸ�ų� HomeAfter �� Ŭ��� �Ǿ��ٴ� ���̴ϱ� ���⼭
        // isSuccess�� true�� �ٲ��ֱ�
        // ����. ���� �Ŵ����� �ı� �� �Ǵ� �ŷ� �Ϸ��� �ߴµ� �װ� �����ؼ� isSuccess�� �ٸ� ������ �� ������ false�� �ʱ�ȭ�� �Ǿ
        // �̷��� �ٲ�. �׸��� ������ ó������ isSuccess�� true�� ������ ���� 2�� �ۿ� ��� �� ������� ��.

        // ���� �� ���� �˾Ƴ��� ���� ������  
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name=="Forest Exit"|| scene.name == "HomeAfter")
        {
            isSuccess = true;
        }
    }

}
