using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerStage3 : MonoBehaviour
{
    //스테이지3의 미션 성공하면 GameManager의 bool 타입 isSuccess ture로 변경.
    // GameManager 스크립트가 대빵이면 GameManagerStage3 얘도 부하 느낌.

    // 플레이어가 npc4한테 말을 걸면 퀘스트가 활성화 됨.
    public bool isQuest;

    // 꽃 개수
    public int FlowerCount = 0;

    [SerializeField]
    private GameObject countPanel;
    [SerializeField]
    private Text countText;

    GameManager gameManager;

    //다음 스테이지로 가는 문
    public GameObject nextDoor;
    public bool isDoor; //문 생성할 지 말지 결정함

    // Start is called before the first frame update
    void Start()
    {
        isQuest = false;    // false로 초기화
        countPanel.SetActive(false);    // 시작할 때에는 패널 안 보이고
        countText.text = "0 / 1";        // 카운트 텍스트는 0 /1으로 초기화
        gameManager = FindObjectOfType<GameManager>();
        // 다음 스테이지로 가는 문은 처음에는 비활성화
        nextDoor.SetActive(false);
        isDoor = false;     //문 안 보임
    }

    // Update is called once per frame
    void Update()
    {
        // 퀘스트를 받으면 카운트 패널 활성화
        if (isQuest)
        {
            countPanel.SetActive(true);

            countText.text = FlowerCount+" / 1";  // 꽃 개수를 업데이트 시켜줌
        }

        if (FlowerCount == 1)  //꽃 채집하면 미션 성공
        {
            gameManager.isSuccess = true;
        }
        else
        {
            gameManager.isSuccess = false;  //아니면 그대로 false
        }

        if (isDoor)  // 문 있다는 bool타입이 트루로 바뀌면 문활성화
        {
            nextDoor.SetActive(true);
        }
        else
        {           // 반대면 그대로 비활성화 
            nextDoor.SetActive(false);
        }
    }
}
