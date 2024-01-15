using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ü�� ���� �����ϴ� ��ũ��Ʈ
public class ChessGame : MonoBehaviour
{
    [SerializeField]
    private bool isChess = false;    // �÷��̾ ü�� �ֺ� �ٰ����� Ȱ��ȭ �Ǵ� ����
    [SerializeField]
    private GameObject ChessGamePanel;     // �ٰ����� �÷��̾ GŰ�� ������ �ߴ� �г�
    [SerializeField]
    private GameObject GuidePanel;  // ü�� ��� Ű �˷���.
    [SerializeField]
    private GameObject ClearPanel;  // ü�� �� ������ Ŭ���� �г� ������.

    public bool isStart = false;    // �÷��̾ ã�� ���� ��ư �� ������ Ȱ��ȭ��.

    public bool isGain = false;     // �÷��̾ ü�� �ϳ����� ��� ������. 

    public string chessName;        // GetChess���� ���⿡ ������ ü���� �̸� �־���.


    // ü���� �������� Ȱ��ȭ���� ü������
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

    // true�� ü�� �� ���� �� ����.
    public bool isPut = false;


    private bool isShow;

    [SerializeField]
    private int ChessCount = 0;

    // ȿ����
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
                Debug.Log("ü�� Ž��");
                //Debug.Log("�۰����� ��� �Ͻô� ü����. �ٵ� ü���ǿ� ���� ������ �� ������.. ã�� �����ұ�?");
                ChessGamePanel.SetActive(true);
            }

        }

        if (isChess && isStart && isPawn)
        {
            if (chessName == "White Rook")
            {
                PlaySoundEffect("PUTCHESS");

                Debug.Log("White Rook �������Ҵ�.");
                WRook.SetActive(true);
                chessName = "";
                ChessCount++;
            }
            if (chessName == "White Knight")
            {
                PlaySoundEffect("PUTCHESS");

                Debug.Log("White Knight�� �������Ҵ�.");
                WKnight.SetActive(true);
                chessName = "";
                ChessCount++;
            }
            if (chessName == "White Pawn")
            {
                PlaySoundEffect("PUTCHESS");

                Debug.Log("White Pawn �������Ҵ�.");
                WPawn.SetActive(true);
                chessName = "";
                ChessCount++;
            }
            if (chessName == "Black Queen")
            {
                PlaySoundEffect("PUTCHESS");

                Debug.Log("Black Queen �������Ҵ�.");
                BQueen.SetActive(true);
                chessName = "";
                ChessCount++;
            }
            if (chessName == "Black Bishop")
            {
                PlaySoundEffect("PUTCHESS");

                Debug.Log("Black Bishop �������Ҵ�.");
                BBishop.SetActive(true);
                chessName = "";
                ChessCount++;
            }
            if (chessName == "Black Pawn")
            {
                PlaySoundEffect("PUTCHESS");

                Debug.Log("Black Pawn �������Ҵ�.");
                BPawn.SetActive(true);
                chessName = "";
                ChessCount++;
            }
        }

        // ü��ī��Ʈ�� 6�̸� ü���ǿ� ������ ü���� �� ä���
        if (ChessCount == 6)
        {
            // Ŭ���� �г� �����
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
        //�� �г� �ؽ�Ʈ�� set����
        //"�ֺ��� �� ã�ƺ���. GŰ�� ü�� ���� �ֿ� �� �־�. �� ���� �� ���� ���� �ֿ� �� �ִٴ� �� ��������."
        //"���� �ֿ�� ü�������� ���� BŰ�� ��������."
        // �̷� �ȳ� �ؽ�Ʈ ����ֱ�.
        ChessGamePanel.SetActive(false);
        GuidePanel.SetActive(true);
        Debug.Log("�ֺ��� �� ã�ƺ���. \nGŰ�� ü�� ���� �ֿ� �� �ִ�. �� ���� �� ���� ���� �ֿ� �� �ִٴ� �� ��������.");
        Debug.Log("���� �ֿ�� �ٽ� ü�������� ���� BŰ�� ��������.");
     //   text.text = "�ֺ��� �� ã�ƺ���.\nGŰ�� ü�� ���� �ֿ� �� �ִ�. �� ���� �� ���� ���� �ֿ� �� �ִٴ� �� ��������.\n���� �ֿ�� �ٽ� ü�������� ���� BŰ�� ��������.";
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

    // Ŭ����â �ݱ� ��ư ������ ü�� ���� ���� ���ϵ��� ��Ȱ��ȭ �ؾ���.
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

    // ȿ���� ��� �޼ҵ�
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
