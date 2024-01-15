using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ü���� ȹ�� ��ũ��Ʈ
// ü�����鿡 �ٿ��� ��ũ��Ʈ
public class GetChess : MonoBehaviour
{
    private ChessGame chessGame;    // isStart�� �����ϱ� ���ؼ�.

    // �÷��̾� �տ� ü�� ���
    [SerializeField]
    private GameObject GribPosition;

    // ü�� ���� �����ͼ� ����
    [SerializeField]
    private string thisChessName="";

    // true�� ü�� �� �ֿ� �� ����.
    private bool isGrib = false;

    //// true�� ü�� �� ���� �� ����.
    //private bool isPut = false;

    // ȿ����
    public AudioClip audioPickChess;

    AudioSource audioSource;

    void Start()
    {
        chessGame = FindObjectOfType<ChessGame>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        // isGain�� false�̸� �� �� �ְ� �� ������ ü���ǿ� ���� ���������� true������ �ٽ� false�� �ٲ���.
        // ü�� �� ���ø�.
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

        // ��� �ִ� ü�� ���� ü���ǿ� ��������.
        if (chessGame.isPut && chessGame.isGain)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {

                chessGame.chessName = thisChessName;    
                chessGame.isPawn = true;
                chessGame.isGain = false;
                Debug.Log("�� �������� ��Ȱ��ȭ");
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

    // ȿ���� ��� �޼ҵ�
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
