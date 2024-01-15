using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerStage2 : MonoBehaviour
{
    // 집 hp
    public static int HouseHp = 10;

    // 최대 체력 변수
    //int maxHp = 10;

    // map2에 있는 UI들(하우스 체력바와 타임텍스트)
    public GameObject map2UI;

    // 집hp 어느정도 있는지 보여주는 UI
    public Slider hpSlider;

    // 하우스 애니메이션
    public Animator HouseAni;

    // 시간 표시 보여주는 ui 텍스트
    public Text timeText;

    // 전체 제한 시간을 설정해준다.
    public float setTime = 60;

    // 분단위와 초단위를 담당할 변수를 만들어준다.
    int min;
    float sec;

    public GameManager gameManager;

    // 플레이어 콘트롤3에서 isStart 받아와서 시작시키기 위한거
    PlayerController3 playerController3;

    // 플레이어가 npc3한테 말을 걸면 퀘스트가 활성화 됨.
    //public bool isQuest;

    //다음 스테이지로 가는 문
    public GameObject nextDoor;
    public bool isDoor; //문 생성할 지 말지 결정함

    // Start is called before the first frame update
    void Start()
    {
        //isQuest = false;    // false로 초기화
       // gameManager = FindObjectOfType<GameManager>();
        playerController3 = FindObjectOfType<PlayerController3>();

        // 집hp 슬라이더와 타임 텍스트는 map2가 아니면 비활성화
        map2UI.SetActive(false);

        // 다음 스테이지로 가는 문은 처음에는 비활성화
        nextDoor.SetActive(false);
        isDoor = false;     //문 안 보임
    }

    // Update is called once per frame
    void Update()
    {
        // 현재 집의 hp를 hp 슬라이더의 value에 반영함.
        hpSlider.value = HouseHp;

        if(playerController3.isStart)
        {
            map2UI.SetActive(true);
            StartTime();
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

    // 제한시간
    void StartTime()
    {
        // 시간 제한
        // 남은 시간을 감소시켜준다.
        setTime -= Time.deltaTime;

        // 전체 시간이 60초 보다 클 때
        if (setTime >= 60f)
        {
            // 60으로 나눠서 생기는 몫을 분단위로 변경
            min = (int)setTime / 60;
            // 60으로 나눠서 생기는 나머지를 초단위로 설정
            sec = setTime % 60;
            // UI를 표현해준다
            timeText.text = "남은 시간 : " + min + "분" + (int)sec + "초";
        }

        // 전체시간이 60초 미만일 때
        if (setTime < 60f)
        {
            // 분 단위는 필요없어지므로 초단위만 남도록 설정
            timeText.text = "남은 시간 : " + (int)setTime + "초";
        }

        // 남은 시간이 0보다 작아질 때
        if (setTime <= 0)
        {
            // UI 텍스트를 0초로 고정시킴.
            timeText.text = "남은 시간 : 0초";

            StartCoroutine(HouseKeep());
        }

    }

    // 집지키는 거 성공하면 띄울 메소드
    IEnumerator HouseKeep()
    {
        yield return new WaitForSeconds(2f);

        if (HouseHp > 0)
        {
            timeText.text = "집 지키기 성공!";
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
            Debug.Log("게임오버");
            SceneManager.LoadScene("GameOver");
        }
    }

    void nextMap()
    {
        //Map2에서 집지키기 성공하면 map3으로 자동 이동
        if (gameManager.isSuccess)
        {
            playerController3.map2.SetActive(false);
            playerController3.map3.SetActive(true);
            playerController3.isAttack = false;    //공격 닫음
            playerController3.isStart = false;     //비활성화
            // map2 UI 비활성화
            map2UI.SetActive(false);
        }
    }
}
