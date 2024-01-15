using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 체스말 획득 스크립트
// 체스말들에 붙여질 스크립트
public class GetChess : MonoBehaviour
{
    private ChessGame chessGame;    // isStart를 접근하기 위해서.

    // 플레이어 앞에 체스 띄움
    [SerializeField]
    private GameObject GribPosition;

    // 체스 정보 가져와서 담음
    [SerializeField]
    private string thisChessName="";

    // true면 체스 말 주울 수 있음.
    private bool isGrib = false;

    //// true면 체스 말 놓을 수 있음.
    //private bool isPut = false;

    // 효과음
    public AudioClip audioPickChess;

    AudioSource audioSource;

    void Start()
    {
        chessGame = FindObjectOfType<ChessGame>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        // isGain이 false이면 들 수 있고 이 변수는 체스판에 가서 내려놓으면 true였던걸 다시 false로 바꿔줌.
        // 체스 말 들어올림.
        if (chessGame.isStart&&isGrib&&!chessGame.isGain)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                PlaySoundEffect("PICKCHESS");

                this.transform.SetParent(GribPosition.transform);
                Transform rc = this.GetComponent<Transform>();
                rc.localPosition = Vector2.zero;

                thisChessName = this.gameObject.name;
                chessGame.isGain = true;

                Debug.Log(this.gameObject.name);
            }
        }

        // 들고 있는 체스 말을 체스판에 내려놓음.
        if (chessGame.isPut && chessGame.isGain)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {

                chessGame.chessName = thisChessName;    
                chessGame.isPawn = true;
                chessGame.isGain = false;
                Debug.Log("폰 내려놓고 비활성화");
                Debug.Log(this.gameObject.name);       
                thisChessName = "";
                //chessGame.chessName = "";
                gameObject.SetActive(false);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isGrib = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isGrib = false;
        }
        // isGrib = false;
    }

    // 효과음 재생 메소드
    void PlaySoundEffect(string action)
    {
        switch (action)
        {
            case "PICKCHESS":
                audioSource.clip = audioPickChess;
                break;
        }
        audioSource.Play();
    }
}
