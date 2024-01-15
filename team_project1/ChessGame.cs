using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 체스 게임 관리하는 스크립트
public class ChessGame : MonoBehaviour
{
    [SerializeField]
    private bool isChess = false;    // 플레이어가 체스 주변 다가가면 활성화 되는 변수
    [SerializeField]
    private GameObject ChessGamePanel;     // 다가가고 플레이어가 G키를 누르면 뜨는 패널
    [SerializeField]
    private GameObject GuidePanel;  // 체스 쥐는 키 알려줌.
    [SerializeField]
    private GameObject ClearPanel;  // 체스 다 넣으면 클리어 패널 보여줌.

    public bool isStart = false;    // 플레이어가 찾기 시작 버튼 예 누르면 활성화됨.

    public bool isGain = false;     // 플레이어가 체스 하나씩만 들게 제한함. 

    public string chessName;        // GetChess에서 여기에 가져온 체스말 이름 넣어줌.


    // 체스말 가져오면 활성화해줄 체스말들
    [SerializeField]
    private GameObject WKnight;
    [SerializeField]
    private GameObject WRook;
    [SerializeField]
    private GameObject WPawn;

    [SerializeField]
    private GameObject BQueen;
    [SerializeField]
    private GameObject BBishop;
    [SerializeField]
    private GameObject BPawn;

    public bool isPawn = false;

    // true면 체스 말 놓을 수 있음.
    public bool isPut = false;


    private bool isShow;

    [SerializeField]
    private int ChessCount = 0;

    // 효과음
    public AudioClip audioKey;
    public AudioClip audioPutChess;

    AudioSource audioSource;

    public FinalManager Final;

    void Start()
    {
        ChessGamePanel.SetActive(false);
        GuidePanel.SetActive(false);
        ClearPanel.SetActive(false);
        isShow = true;
        audioSource = GetComponent<AudioSource>();

        //ChessGamePanel = GameObject.Find("Canvas").transform.Find("Chess Game Panel").gameObject;
        //GuidePanel = GameObject.Find("Canvas").transform.Find("Chess Game Guide").gameObject;
        //ClearPanel = GameObject.Find("Canvas").transform.Find("Chess Game Clear").gameObject;

        Final = FindObjectOfType<FinalManager>();
    }

    void Update()
    {
        if (isChess&&isShow)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("체스 탐색");
                //Debug.Log("작가님이 즐겨 하시던 체스다. 근데 체스판에 뭔가 부족한 것 같은데.. 찾기 시작할까?");
                ChessGamePanel.SetActive(true);
            }

        }

        if (isChess && isStart && isPawn)
        {
            if (chessName == "White Rook")
            {
                PlaySoundEffect("PUTCHESS");

                Debug.Log("White Rook 돌려놓았다.");
                WRook.SetActive(true);
                chessName = "";
                ChessCount++;
            }
            if (chessName == "White Knight")
            {
                PlaySoundEffect("PUTCHESS");

                Debug.Log("White Knight를 돌려놓았다.");
                WKnight.SetActive(true);
                chessName = "";
                ChessCount++;
            }
            if (chessName == "White Pawn")
            {
                PlaySoundEffect("PUTCHESS");

                Debug.Log("White Pawn 돌려놓았다.");
                WPawn.SetActive(true);
                chessName = "";
                ChessCount++;
            }
            if (chessName == "Black Queen")
            {
                PlaySoundEffect("PUTCHESS");

                Debug.Log("Black Queen 돌려놓았다.");
                BQueen.SetActive(true);
                chessName = "";
                ChessCount++;
            }
            if (chessName == "Black Bishop")
            {
                PlaySoundEffect("PUTCHESS");

                Debug.Log("Black Bishop 돌려놓았다.");
                BBishop.SetActive(true);
                chessName = "";
                ChessCount++;
            }
            if (chessName == "Black Pawn")
            {
                PlaySoundEffect("PUTCHESS");

                Debug.Log("Black Pawn 돌려놓았다.");
                BPawn.SetActive(true);
                chessName = "";
                ChessCount++;
            }
        }

        // 체스카운트가 6이면 체스판에 부족한 체스말 다 채운거
        if (ChessCount == 6)
        {
            // 클리어 패널 띄우줌
            ClearPanel.SetActive(true);
            ChessCount++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isChess = true;
        }
        if (other.tag == "Player"&&isStart)
        {
            isPut = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isChess = false;

        if(isStart)
        {
            isPut = false;
            isPawn = false;
        }

    }



    public void OnclickYes()
    {
        //방 패널 텍스트를 set으로
        //"주변을 잘 찾아보자. G키로 체스 말을 주울 수 있어. 한 번에 한 개의 말만 주울 수 있다는 걸 주의하자."
        //"말을 주우면 체스판으로 가서 B키로 내려놓자."
        // 이런 안내 텍스트 띄워주기.
        ChessGamePanel.SetActive(false);
        GuidePanel.SetActive(true);
        Debug.Log("주변을 잘 찾아보자. \nG키로 체스 말을 주울 수 있다. 한 번에 한 개의 말만 주울 수 있다는 걸 주의하자.");
        Debug.Log("말을 주우면 다시 체스판으로 가서 B키로 내려놓자.");
     //   text.text = "주변을 잘 찾아보자.\nG키로 체스 말을 주울 수 있다. 한 번에 한 개의 말만 주울 수 있다는 걸 주의하자.\n말을 주우면 다시 체스판으로 가서 B키로 내려놓자.";
    }
    public void OnclickNo()
    {
        ChessGamePanel.SetActive(false);
    }
    public void OnclickClose()
    {
        GuidePanel.SetActive(false);
        isStart = true;
        isShow = false;
    }

    // 클리어창 닫기 버튼 누르면 체스 게임 시작 못하도록 비활성화 해야함.
    public void OnclickClearClose()
    {
        PlaySoundEffect("GETKEY");

        ClearPanel.SetActive(false);

        isStart = false;
        isChess = false;
        isGain = false;
        isShow = false;

        Final.isLibrary = true;
    }

    // 효과음 재생 메소드
    void PlaySoundEffect(string action)
    {
        switch (action)
        {
            case "GETKEY":
                audioSource.clip = audioKey;
                break;
            case "PUTCHESS":
                audioSource.clip = audioPutChess;
                break;
        }
        audioSource.Play();
    }
}
