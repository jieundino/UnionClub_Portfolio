using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerStage1 : MonoBehaviour
{
    //��������1�� �̼� �����ϸ� GameManager�� bool Ÿ�� �� ���� ����.
    // GameManager ��ũ��Ʈ�� �뻧�̸� GameManagerStage1 �갡 ���� ����.

    // �÷��̾ npc2���� ���� �ɸ� ����Ʈ�� Ȱ��ȭ ��.
    public bool isQuest;

    // Ŭ�ι� ����
    public int cloverCount = 0;

    [SerializeField]
    private GameObject countPanel;
    [SerializeField]
    private Text countText;

    GameManager gameManager;

    //���� ���������� ���� ��
    public GameObject nextDoor;
    public bool isDoor; //�� ������ �� ���� ������

    // Start is called before the first frame update
    void Start()
    {
        isQuest = false;    // false�� �ʱ�ȭ
        countPanel.SetActive(false);    // ������ ������ �г� �� ���̰�
        countText.text = "" + 0;        // ī��Ʈ �ؽ�Ʈ�� 0���� �ʱ�ȭ
        gameManager = FindObjectOfType<GameManager>();
        // ���� ���������� ���� ���� ó������ ��Ȱ��ȭ
        nextDoor.SetActive(false);
        isDoor = false;     //�� �� ����
    }

    // Update is called once per frame
    void Update()
    {
        // ����Ʈ�� ������ ī��Ʈ �г� Ȱ��ȭ
        if(isQuest)
        {
            countPanel.SetActive(true);

            countText.text = "" + cloverCount;  // Ŭ�ι� ������ ������Ʈ ������
        }

        if(cloverCount==5)  //���� Ŭ�ι� ������ 5���� �̼� ����
        {
            gameManager.isSuccess = true;
        }
        else
        {
            gameManager.isSuccess = false;  //�ƴϸ� �״�� false
        }

        if(isDoor)  // �� �ִٴ� boolŸ���� Ʈ��� �ٲ�� ��Ȱ��ȭ
        {
            nextDoor.SetActive(true);
        }
        else
        {           // �ݴ�� �״�� ��Ȱ��ȭ 
            nextDoor.SetActive(false);
        }
    }
}
