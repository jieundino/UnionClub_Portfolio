using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;

    //대화창
    public GameObject talkPanel;
    public Text talkText;
    public int talkIndex;
    public bool isAction;
    public GameObject scanObject;   // 플레이어로부터 스캔오브젝트를 전달받을 거임.

    public bool isMeet =false; // 조건을 충족하면 이 변수를 true로 바꿔주고 talkData_before
                               // true이면 다음 대화 문장을 말해줌.        talkData_after

    // 스테이지1,3 매니저 관련 변수들
    public bool isGet = false;  // 게임매니저에서 대화 다 끝나면 isGet true로 해주기. 스테이지1매니저 스크립트에서 이용할 거임.
                                // +스테이지3에서 길막이와 대화하고 나면 이걸 트루로 풀어줘서 스테이지3에서 퀘스트 진행하게 함.
    public bool isTry = false;  // 아이템 가지고 카드 단말기 대화할 때 아이템 사용 여부 정해줌.

    public Stage3Manager stage3Manager;

    // 게임메뉴
    public GameObject menuSet;

    // 게임메뉴 나오면 게임 멈출지 말지 결정하는 변수
    bool isPause;

    // 브금 매니저
    public BgMusicManager bgMusic;

    private void Start()
    {
        bgMusic = FindObjectOfType<BgMusicManager>();
    }

    private void Update()
    {
        // Esc버튼 누르면 게임 메뉴 나옴
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuSet.activeSelf) //게임 메뉴 활성화 된 상태
            {
                isPause = false;
                menuSet.SetActive(false);
            }
            else //게임 메뉴 비활성화된 상태
            {
                isPause = true;
                menuSet.SetActive(true);
            }
        }

        if (isPause)
        {
            // 게임 멈추기
            Time.timeScale = 0;
        }
        else
        {
            // 게임 멈추던 거 풀음
            Time.timeScale = 1;
        }

        //씬멈춰있으면 브금 일시정지
        if (Time.timeScale == 0)
        {
            bgMusic.BgPause();
        }
        else if (Time.timeScale == 1)  //Time.timeScale이 1로 씬이 멈춰있지 않으면 브금 일시정지 해제
        {
            bgMusic.BgUnPause();
        }
    }

    //계속하기 버튼 누르면 게임 계속
    public void GameContinue()
    {
        isPause = false;
        menuSet.SetActive(false);
    }

    //종료하기 버튼 누르면 게임 종료
    public void GameExit()
    {
        Application.Quit();
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;

        // 스캔한 오브젝트의 id와 isNPC정보를 가져와야 하기 때문에 objData script 필요.
        ObjData objData = scanObject.GetComponent<ObjData>();
        //objData의 id 정보를 매개변수로 넘김.
        Talk(objData.id);

        talkPanel.SetActive(isAction); //대화창 활성화 상태에 따라 대화창 활성화 변경
    }

    void Talk(int id)
    {
        // 조건을 충족하기 전이면 GetTalk1 대화 내용 보여줌.
        // 조건 충족하고 isMeet 바뀌면 GetTalk2 대화 내용 보여줌

        // 조건이 정해진 오브젝트와 npc들
        if (id >= 100)    //위의 id가 100위라서 이렇게 조건을 잡음.
        {
            if (!isMeet)  // 조건 충족 전
            {
                string talkData = talkManager.GetTalk1(id, talkIndex);

                if (talkData == null) //반환된 것이 null이면 더이상 남은 대사가 없으므로 action상태변수를 false로 설정 
                {
                    // 스테이지1
                    if (SceneManager.GetActiveScene().name == "Stage1" && id < 140)
                    {
                        isGet = true;
                    }
                    // 스테이지3
                    else if (SceneManager.GetActiveScene().name == "Stage3" && id == 1000)  // 길막이와 대화 후 미션 가능하게 하기.
                    {
                        isGet = true;
                    }

                    isAction = false;
                    talkIndex = 0; //talk인덱스는 다음에 또 사용되므로 초기화
                    return; //void에서의 return 함수 강제종료 (밑의 코드는 실행되지 않음)
                }

                talkText.text = talkData;

                //다음 문장을 가져오기 위해 talkData의 인덱스를 늘림
                isAction = true;    //대사가 남아있으므로 계속 진행함.
                talkIndex++;

            }
            else //조건 충족한 이후 대화
            {
                string talkData = talkManager.GetTalk2(id, talkIndex);

                if (talkData == null) //반환된 것이 null이면 더이상 남은 대사가 없으므로 action상태변수를 false로 설정 
                {
                    // 스테이지1
                    if (SceneManager.GetActiveScene().name == "Stage1" && id == 140)
                    {
                        isTry = true;
                    }
                    // 스테이지3
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
                    talkIndex = 0; //talk인덱스는 다음에 또 사용되므로 초기화
                    return; //void에서의 return 함수 강제종료 (밑의 코드는 실행되지 않음)
                }

                talkText.text = talkData;

                //다음 문장을 가져오기 위해 talkData의 인덱스를 늘림
                isAction = true;    //대사가 남아있으므로 계속 진행함.
                talkIndex++;
            }
        }
        else // 단순 오브젝트들(그냥 GetTalk으로 가져오는 것들)
        {
            string talkData = talkManager.GetTalk(id, talkIndex);

            if (talkData == null) //반환된 것이 null이면 더이상 남은 대사가 없으므로 action상태변수를 false로 설정 
            {
                isAction = false;
                talkIndex = 0; //talk인덱스는 다음에 또 사용되므로 초기화
                return; //void에서의 return 함수 강제종료 (밑의 코드는 실행되지 않음)
            }

            talkText.text = talkData;

            //다음 문장을 가져오기 위해 talkData의 인덱스를 늘림
            isAction = true;    //대사가 남아있으므로 계속 진행함.
            talkIndex++;
        }
    }

    public void S1FailAction()    //스테이지1에서 실패 액션
    {
        talkText.text = "...이게 아닌 모양이다. 다른 것을 찾아보자.";

        talkPanel.SetActive(true); //대화창 활성화 상태에 따라 대화창 활성화 변경
    }
    public void S1FailActionClose()    //스테이지1에서 실패 액션
    {
        talkPanel.SetActive(false); //대화창 활성화 상태에 따라 대화창 활성화 변경
    }

}
