using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage1Manager : MonoBehaviour
{
    public GameObject mapCard;
    public GameObject mapCoin;
    public GameObject mapAirpot;

    public GameObject cardTerminal;

    public bool isHaving = false;   // �κ����� ������ 1���� ���� �� �־ �� ������ true �Ǹ� �ٸ� ������ ���� ����.
    
    private string itemName;        // �ֿ� ������ �̸� ������

    public GameObject invenCard;
    public GameObject invenCoin;
    public GameObject invenAirpot;

    public GameManager gameManager;
    private GameObject player;

    public Slider timeSlider;

    

    void Start()
    {
        player = GameObject.Find("Dust");
    }


    void Update()
    {
        // Ÿ�̸� 30��
        if(timeSlider.value > 0.0f)
        {
            // �ð��� ������ ��ŭ timeSlider ������ ��.
            timeSlider.value -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Time is Zero. Ż�� ����!");
            // ���ӿ���1 ������ �̵�.
            SceneManager.LoadScene("GameOver1");
        }

        // ���� ���� ������ ȹ��
        if(gameManager.isGet)
        {
            gameManager.isAction = true;
            if (Input.GetKeyDown(KeyCode.G)) 
            {
                if (!isHaving)
                {
                    itemName = gameManager.scanObject.name;
                    switch (itemName)
                    {
                        case "BusCard":
                            mapCard.SetActive(false);
                            invenCard.SetActive(true);
                            isHaving = true;
                            break;
                        case "Coin":
                            mapCoin.SetActive(false);
                            invenCoin.SetActive(true);
                            isHaving = true;
                            break;
                        case "Airpot":
                            mapAirpot.SetActive(false);
                            invenAirpot.SetActive(true);
                            isHaving = true;
                            break;
                    }
                    gameManager.isMeet = true;
                }
                gameManager.isGet = false;
                gameManager.isAction = false;
            }
            else if (Input.GetKeyDown(KeyCode.N))   // ������ ���� ����
            {
                gameManager.isMeet = false;
                gameManager.isGet = false;
                gameManager.isAction = false;
            }
        }
        
        if(isHaving)
        {
            if(gameManager.isTry)
            {
                gameManager.isAction = true;
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    if (itemName == "BusCard")
                    {
                        Debug.Log("��������1 Ż�� ����!");
                        isHaving = false;
                        invenCard.SetActive(false);
                        gameManager.isTry = false;
                        gameManager.isAction = false;
                        gameManager.isMeet = false;
                        // Ż�� ����!
                        // ���� ������ �̵�.
                        SceneManager.LoadScene("Stage2");
                    }
                    else if(itemName == "Coin"|| itemName == "Airpot")
                    {
                        gameManager.S1FailAction();
                        player.GetComponent<playerController>().PlaySoundEffect("TALK");

                        Debug.Log("����");
                        cardTerminalReset();
                    }   
                }
                else if (Input.GetKeyDown(KeyCode.N))   // ������ ������� ����.
                {
                    gameManager.isTry = false;
                    gameManager.isAction = false;
                }
            }
        }
    }

    private void cardTerminalReset()
    {
        invenCoin.SetActive(false);
        invenAirpot.SetActive(false);
        gameManager.isTry = false;
        gameManager.isAction = false;
        isHaving = false;
        gameManager.isMeet = false;
    }
}
