using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3Manager : MonoBehaviour
{
    public bool isQuest = false;        // 길막이와 대화하면 이 변수가 true로 되고(게임매니저의 isGet 변수 이용하기) 퀘스트 내용 진행 가능하게 해줌.
                                        // 이 변수가 true면 쓰레기통 뒤질 수 있음.
    public bool isDogGum = false;       // 쓰레기통2 뒤지면 true로 바꿔줌. 이걸로 퀘스트 성공여부 확인이 연계됨.
    public bool isComplete = false;     // 퀘스트 성공 여부
    // isComplete 변수 true로 바꿔주고 gameManager의 isMeet로 true로 바꿔주기.

    public GameManager gameManager;

    public bool isBlackTalk = false;    // 흑임자랑 말했는지 여부(미션 성공 이후에 흑임자와 대화하고 나면 이 변수 true로 됨.)

    public GameObject NextBlock;

    public GameObject dog1;

    public GameObject trashCan1;
    public GameObject trashCan2;
    public GameObject trashCan3;

    public string objName = null;

    // 관련 일러스트 이미지 변수
    public Image illust1;
    public Image illust2;
    float time = 0f;
    float F_time = 1f;

    //public GameObject player;

    void Start()
    {
        //player = GameObject.Find("Dust");

        NextBlock = GameObject.Find("Next_Block");
        dog1 = GameObject.Find("dog1");
        trashCan1 = GameObject.Find("trash can 1");
        trashCan2 = GameObject.Find("trash can 2");
        trashCan3 = GameObject.Find("trash can 3");
    }

    void Update()
    {
        // 길막이와 대화하면 
        if(gameManager.isGet)
        {
            isQuest = true;
        }
        // 쓰레기통 뒤지는 퀘스트 활성화
        if (isQuest&&!isComplete)
        {
            objName = gameManager.scanObject.name;
            // 대화한게 쓰레기통들이면 밑의 쓰레기통 뒤지기 활성화.
            if (objName == "trash can 1" || objName == "trash can 2" || objName == "trash can 3")
            {
                gameManager.isMeet = true;
                gameManager.isAction = true;
                if (Input.GetKeyDown(KeyCode.Y))   // 쓰레기통 조사하기.
                {
                    switch (objName)
                    {
                        case "trash can 1":
                            trashCan1.GetComponent<ObjData>().id = 301;
                            break;
                        case "trash can 2":
                            trashCan2.GetComponent<ObjData>().id = 311;
                            isDogGum = true;
                            // 개껌 찾은 일러스트1 보여줌
                            Illust1Fade();
                            break;
                        case "trash can 3":
                            trashCan3.GetComponent<ObjData>().id = 321;
                            break;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.N))   // 쓰레기통 조사 취소.
                {
                    return;
                }
                //Debug.Log("위의 if문 다 돈 이후");
                //gameManager.isMeet = false;
                gameManager.isAction = false;
            }
            else
            {
                gameManager.isMeet = false;
            }

        }

        if(isDogGum)
        {
            gameManager.isMeet = true;
        }

        if (isComplete)
        {
            gameManager.isMeet = true;
            dog1.transform.position = new Vector3(11.6f, 2.22f, 0);
            dog1.GetComponent<ObjData>().id = 1001;
        }

        if(isBlackTalk)
        {
            NextBlock.SetActive(false);
        }
    }

    private void Illust1Fade()
    {
        StartCoroutine(FadeFlow(1));
    }
    public void Illust2Fade()
    {
        StartCoroutine(FadeFlow(2));
    }

    IEnumerator FadeFlow(int num)
    {
        time = 0f;
        if(num==1)
        {
            illust1.gameObject.SetActive(true);
            Color alpha = illust1.color;
            while (alpha.a<1f)
            {
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(0,1,time); // 일러스트의 알파값을 0이었던 것을 1로 조금씩 올려줘서 서서히 나타나게 함.
                illust1.color = alpha;
                yield return null;
            }
            time = 0f;

            yield return new WaitForSeconds(3f);

            while (alpha.a > 0f)
            {
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(1, 0, time); // 일러스트의 알파값을 1이었던 것을 0로 조금씩 내려줘서 서서히 사라지게 함.
                illust1.color = alpha;
                yield return null;
            }
            illust1.gameObject.SetActive(false);
            yield return null;
        }
        else
        {
            illust2.gameObject.SetActive(true);
            Color alpha = illust2.color;
            while (alpha.a < 1f)
            {
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(0, 1, time); // 일러스트의 알파값을 0이었던 것을 1로 조금씩 올려줘서 서서히 나타나게 함.
                illust2.color = alpha;
                yield return null;
            }
            time = 0f;

            yield return new WaitForSeconds(3f);

            while (alpha.a > 0f)
            {
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(1, 0, time); // 일러스트의 알파값을 1이었던 것을 0로 조금씩 내려줘서 서서히 사라지게 함.
                illust2.color = alpha;
                yield return null;
            }
            illust2.gameObject.SetActive(false);
            yield return null;
        }
    }
}
