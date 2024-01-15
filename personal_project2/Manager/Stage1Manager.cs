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

    public bool isHaving = false;   // 인벤에는 아이템 1개만 가질 수 있어서 이 변수가 true 되면 다른 아이템 줍지 못함.
    
    private string itemName;        // 주운 아이템 이름 저장함

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
        // 타이머 30초
        if(timeSlider.value > 0.0f)
        {
            // 시간이 변경한 만큼 timeSlider 변경을 함.
            timeSlider.value -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Time is Zero. 탈출 실패!");
            // 게임오버1 씬으로 이동.
            SceneManager.LoadScene("GameOver1");
        }

        // 버스 내의 아이템 획득
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
            else if (Input.GetKeyDown(KeyCode.N))   // 아이템 줍지 않음
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
                        Debug.Log("스테이지1 탈출 성공!");
                        isHaving = false;
                        invenCard.SetActive(false);
                        gameManager.isTry = false;
                        gameManager.isAction = false;
                        gameManager.isMeet = false;
                        // 탈출 성공!
                        // 다음 씬으로 이동.
                        SceneManager.LoadScene("Stage2");
                    }
                    else if(itemName == "Coin"|| itemName == "Airpot")
                    {
                        gameManager.S1FailAction();
                        player.GetComponent<playerController>().PlaySoundEffect("TALK");

                        Debug.Log("실패");
                        cardTerminalReset();
                    }   
                }
                else if (Input.GetKeyDown(KeyCode.N))   // 아이템 사용하지 않음.
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
