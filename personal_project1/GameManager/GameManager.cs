using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public PlayerController player;
    public TalkManager talkManager;

    //대화창
    public GameObject talkPanel;
    public Text talkText;
    public int talkIndex;
    public bool isAction;
    public GameObject scanObject;   // 플레이어로부터 스캔오브젝트를 전달받을 거임.
    
    public bool isSuccess; // 스테이지마다 미션의 클리어 조건을 완료하면 이 변수를 true로 바꿔주고
                           // true이면 다음 대화 문장을 말해줌.

    private ForestEntrance_PlayerFirstTalk forestEntrance_PlayerFirstTalk;  //처음 숲 입구에 떨어졌을 때 플레이어의 첫 대화

    // 엔피씨의 애니메이터를 총관리함.
    //애니메이터 조작을 위한 변수 
    [SerializeField] private Animator NpcAni;
    [SerializeField] private GameObject Npc;

    // 플레이어의 hp
    public int hp;

    // UI 창에서 플레이어의 hp바를 보여줌
    public Image[] UiHp;    //이미지가 5개라서 배열로 사용함

    // 게임메뉴
    public GameObject menuSet;

    // 게임메뉴 나오면 게임 멈출지 말지 결정하는 변수
    bool isPause;

    // Start is called before the first frame update
    void Start()
    {
        hp = 5;    // 다음 씬으로 넘어갈 때마다 5칸으로 초기화.(난이도를 위해)
        isSuccess = false;

        if((FindObjectOfType<ForestEntrance_PlayerFirstTalk>())!=null)  //있으면 넣기. 오류 막아줌
            forestEntrance_PlayerFirstTalk = FindObjectOfType<ForestEntrance_PlayerFirstTalk>();

        //player = FindObjectOfType<PlayerController>();

        if (GameObject.FindWithTag("NPC")!=null)   // npc가 있으면
        {
            Npc = GameObject.FindWithTag("NPC"); //해당씬의 하이라키에 있는 NPC를 찾아서 넣음
            NpcAni = Npc.GetComponent<Animator>();   //그리고 그 NPC 오브젝트에 있는 애니메이터를 찾아서 넣음 
            
        }
        //talkIndex = 0;
        // 어차파 게임 매니저 새로운 씬이 로드 될 때마다 여기 새로 되어서 AllClear() 메소드 써도 부담 없음.
        AllClear();
    }

    private void Update()
    {
        // Esc버튼 누르면 게임 메뉴 나옴
        if(Input.GetButtonDown("Cancel"))
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

        if(isPause)
        {
            // 게임 멈추기
            Time.timeScale = 0;
        }
        else
        {
            // 게임 멈추던 거 풀음
            Time.timeScale = 1;
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

    public void Talk(int id)
    {
        // 미션을 클리어하기 전이면 GetTalk1 대화 내용 보여줌.
        // 미션 클리어하고 isSuccess로 바뀌면 GetTalk2 대화 내용 보여줌

        if(!isSuccess)  // 미션 클리어 전
        {
            // npc와의 대화
            if(id>=1000)    //npc들의 id가 1000이상이라 조건을 이렇게 잡음
            {
                string talkData = talkManager.GetTalk1(id, talkIndex);

                if (talkData == null) //반환된 것이 null이면 더이상 남은 대사가 없으므로 action상태변수를 false로 설정 
                {
                    // 대화가 끝나면 npc의 애니메이션을 정면으로 전환
                    NpcAni.SetBool("isTalking", false);
                    isAction = false;
                    talkIndex = 0; //talk인덱스는 다음에 또 사용되므로 초기화
                    return; //void에서의 return 함수 강제종료 (밑의 코드는 실행되지 않음)
                }

                talkText.text = talkData;

                //다음 문장을 가져오기 위해 talkData의 인덱스를 늘림
                isAction = true;    //대사가 남아있으므로 계속 진행함.
                                    // 대화중인 npc의 애니메이션을 사이드로 전환
                NpcAni.SetBool("isTalking", true);
                talkIndex++;
            }
            else // 플레이어의 대사 및, npc 제외한 오브젝트
            {
                string talkData = talkManager.GetTalk1(id, talkIndex);

                if (talkData == null) //반환된 것이 null이면 더이상 남은 대사가 없으므로 action상태변수를 false로 설정 
                {
                    if ((FindObjectOfType<ForestEntrance_PlayerFirstTalk>()) != null)
                    {
                        if (!forestEntrance_PlayerFirstTalk.isStartLine) //이제 자기 혼자서 하는 대사 다 했으면
                        {
                            forestEntrance_PlayerFirstTalk.isStartLine = true;  //true로 바꿔서 자기의 대사를 반복하지 않게 함.
                        }
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
        else //클리어 이후 대화
        {
            // npc와의 대화
            if (id >= 1000)    //npc들의 id가 1000이상이라 조건을 이렇게 잡음
            {
                string talkData = talkManager.GetTalk2(id, talkIndex);

                if (talkData == null) //반환된 것이 null이면 더이상 남은 대사가 없으므로 action상태변수를 false로 설정 
                {
                    // 대화가 끝나면 npc의 애니메이션을 정면으로 전환
                    NpcAni.SetBool("isTalking", false);
                    isAction = false;
                    talkIndex = 0; //talk인덱스는 다음에 또 사용되므로 초기화
                    return; //void에서의 return 함수 강제종료 (밑의 코드는 실행되지 않음)
                }

                talkText.text = talkData;

                //다음 문장을 가져오기 위해 talkData의 인덱스를 늘림
                isAction = true;    //대사가 남아있으므로 계속 진행함.
                                    // 대화중인 npc의 애니메이션을 사이드로 전환
                NpcAni.SetBool("isTalking", true);
                talkIndex++;
            }
            else // 플레이어의 대사 및, npc 제외한 오브젝트
            {
                string talkData = talkManager.GetTalk2(id, talkIndex);

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
            // 플레이어의 hp가 0이 되면 게임 오버 씬으로 이동
            Debug.Log("게임오버");
            SceneManager.LoadScene("GameOver");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 만약 점프맵 같은 곳에서 낙오해서 게임매니저 게임 오브젝트에 있는 박스 콜라이더와 닿으면
        if (collision.gameObject.tag=="Player")
        {
            // 낭떠러지에 떨어진 것으로 간주해서 hp 깎기.
            HpDown();
            // 그리고 플레이어 원위치로 옮겨주기
            collision.transform.position = new Vector3(0, 3, 0);
        }
    }


    void AllClear()
    {
        // 현재의 씬 이름을 받아와서 숲 출구거나 HomeAfter 다 클리어가 되었다는 뜻이니까 여기서
        // isSuccess를 true로 바꿔주기
        // 이유. 게임 매니저를 파괴 안 되는 거로 하려고 했는데 그거 실패해서 isSuccess가 다른 씬으로 갈 때마다 false로 초기화가 되어서
        // 이렇게 바꿈. 그리고 다행히 처음부터 isSuccess가 true인 상태인 씬이 2개 밖에 없어서 이 방식으로 함.

        // 현재 씬 뭔지 알아내기 위해 가져옴  
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name=="Forest Exit"|| scene.name == "HomeAfter")
        {
            isSuccess = true;
        }
    }

}
