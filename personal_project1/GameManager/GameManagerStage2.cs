using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerStage2 : MonoBehaviour
{
    // �� hp
    public static int HouseHp = 10;

    // �ִ� ü�� ����
    //int maxHp = 10;

    // map2�� �ִ� UI��(�Ͽ콺 ü�¹ٿ� Ÿ���ؽ�Ʈ)
    public GameObject map2UI;

    // ��hp ������� �ִ��� �����ִ� UI
    public Slider hpSlider;

    // �Ͽ콺 �ִϸ��̼�
    public Animator HouseAni;

    // �ð� ǥ�� �����ִ� ui �ؽ�Ʈ
    public Text timeText;

    // ��ü ���� �ð��� �������ش�.
    public float setTime = 60;

    // �д����� �ʴ����� ����� ������ ������ش�.
    int min;
    float sec;

    public GameManager gameManager;

    // �÷��̾� ��Ʈ��3���� isStart �޾ƿͼ� ���۽�Ű�� ���Ѱ�
    PlayerController3 playerController3;

    // �÷��̾ npc3���� ���� �ɸ� ����Ʈ�� Ȱ��ȭ ��.
    //public bool isQuest;

    //���� ���������� ���� ��
    public GameObject nextDoor;
    public bool isDoor; //�� ������ �� ���� ������

    // Start is called before the first frame update
    void Start()
    {
        //isQuest = false;    // false�� �ʱ�ȭ
       // gameManager = FindObjectOfType<GameManager>();
        playerController3 = FindObjectOfType<PlayerController3>();

        // ��hp �����̴��� Ÿ�� �ؽ�Ʈ�� map2�� �ƴϸ� ��Ȱ��ȭ
        map2UI.SetActive(false);

        // ���� ���������� ���� ���� ó������ ��Ȱ��ȭ
        nextDoor.SetActive(false);
        isDoor = false;     //�� �� ����
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���� hp�� hp �����̴��� value�� �ݿ���.
        hpSlider.value = HouseHp;

        if(playerController3.isStart)
        {
            map2UI.SetActive(true);
            StartTime();
        }

        if (isDoor)  // �� �ִٴ� boolŸ���� Ʈ��� �ٲ�� ��Ȱ��ȭ
        {
            nextDoor.SetActive(true);
        }
        else
        {           // �ݴ�� �״�� ��Ȱ��ȭ 
            nextDoor.SetActive(false);
        }
    }

    // ���ѽð�
    void StartTime()
    {
        // �ð� ����
        // ���� �ð��� ���ҽ����ش�.
        setTime -= Time.deltaTime;

        // ��ü �ð��� 60�� ���� Ŭ ��
        if (setTime >= 60f)
        {
            // 60���� ������ ����� ���� �д����� ����
            min = (int)setTime / 60;
            // 60���� ������ ����� �������� �ʴ����� ����
            sec = setTime % 60;
            // UI�� ǥ�����ش�
            timeText.text = "���� �ð� : " + min + "��" + (int)sec + "��";
        }

        // ��ü�ð��� 60�� �̸��� ��
        if (setTime < 60f)
        {
            // �� ������ �ʿ�������Ƿ� �ʴ����� ������ ����
            timeText.text = "���� �ð� : " + (int)setTime + "��";
        }

        // ���� �ð��� 0���� �۾��� ��
        if (setTime <= 0)
        {
            // UI �ؽ�Ʈ�� 0�ʷ� ������Ŵ.
            timeText.text = "���� �ð� : 0��";

            StartCoroutine(HouseKeep());
        }

    }

    // ����Ű�� �� �����ϸ� ��� �޼ҵ�
    IEnumerator HouseKeep()
    {
        yield return new WaitForSeconds(2f);

        if (HouseHp > 0)
        {
            timeText.text = "�� ��Ű�� ����!";
            gameManager.isSuccess = true;
            nextMap();
        }
    }

    public void HouseDamaged()
    {
        if (HouseHp>0)
        {
            HouseAni.SetTrigger("doDamaged");
            HouseHp--;
        }
        else
        {
            Debug.Log("���ӿ���");
            SceneManager.LoadScene("GameOver");
        }
    }

    void nextMap()
    {
        //Map2���� ����Ű�� �����ϸ� map3���� �ڵ� �̵�
        if (gameManager.isSuccess)
        {
            playerController3.map2.SetActive(false);
            playerController3.map3.SetActive(true);
            playerController3.isAttack = false;    //���� ����
            playerController3.isStart = false;     //��Ȱ��ȭ
            // map2 UI ��Ȱ��ȭ
            map2UI.SetActive(false);
        }
    }
}
