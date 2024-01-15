using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerStage3 : MonoBehaviour
{
    //��������3�� �̼� �����ϸ� GameManager�� bool Ÿ�� isSuccess ture�� ����.
    // GameManager ��ũ��Ʈ�� �뻧�̸� GameManagerStage3 �굵 ���� ����.

    // �÷��̾ npc4���� ���� �ɸ� ����Ʈ�� Ȱ��ȭ ��.
    public bool isQuest;

    // �� ����
    public int FlowerCount = 0;

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
        countText.text = "0 / 1";        // ī��Ʈ �ؽ�Ʈ�� 0 /1���� �ʱ�ȭ
        gameManager = FindObjectOfType<GameManager>();
        // ���� ���������� ���� ���� ó������ ��Ȱ��ȭ
        nextDoor.SetActive(false);
        isDoor = false;     //�� �� ����
    }

    // Update is called once per frame
    void Update()
    {
        // ����Ʈ�� ������ ī��Ʈ �г� Ȱ��ȭ
        if (isQuest)
        {
            countPanel.SetActive(true);

            countText.text = FlowerCount+" / 1";  // �� ������ ������Ʈ ������
        }

        if (FlowerCount == 1)  //�� ä���ϸ� �̼� ����
        {
            gameManager.isSuccess = true;
        }
        else
        {
            gameManager.isSuccess = false;  //�ƴϸ� �״�� false
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
}
