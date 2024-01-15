using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//플레이어 이동
public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 3.5f;

    CharacterController cc;

    public GameObject player;

    float gravity = -10f;

    float yVelocity = 0;

    public float jumpPower = 10f;

    public bool isJumping = false;


    // 효과음
    public AudioClip audioWalk;
    public AudioClip audioRun;

    AudioSource audioSource;

    private bool isWalk = false;
    private bool isRun = false;

    //서재의 일기 UI 오브젝트 변수
    public GameObject LetterPanel;


    public FinalManager Final;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        Final = FindObjectOfType<FinalManager>();
    }

    //문과 부딪칠때 씬이동 / 서재에서 일기장 볼 수 있음
    void OnTriggerEnter(Collider other)
    {
        if(Final.isLibrary)
        {
            if (other.tag == "Door_library")
            {
                Debug.Log("서재 이동");
                //현재 씬 이동시 플레이어 위치 선정이 안됨 이에 대해 이야기하기

                //player.transform.position += new Vector3(1, -2, -3);
                //transform.Translate(new Vector3(-1, 2, 0));
                SceneManager.LoadScene("StudyRoom");

            }
        }

        if(Final.isBath)
        {
            if (other.tag == "Door_restroom")
            {
                Debug.Log("화장실 이동");
                //transform.position += new Vector3(1, -2, -3);
                //transform.Translate(new Vector3(-1, 2, 0));
                SceneManager.LoadScene("BathRoom");

            }
        }


        if (other.tag == "Door")
        {
            Debug.Log("메인으로 이동");
            //transform.position += new Vector3(1, -2, -3);
            //transform.Translate(new Vector3(-1, 2, 0));
            SceneManager.LoadScene("Game Main Scene");

        }

        // 서재에 있는 편지 발견하면
        if(other.tag == "library")
        {
            LetterPanel.SetActive(true);
        }

    }


    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir);


        if (cc.collisionFlags == CollisionFlags.Below)
        {
            if (isJumping)
            {
                isJumping = false;
                yVelocity = 0;
            }
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 7f;

            isWalk = false;
            isRun = true;
        }
        else
        {
            moveSpeed = 3.5f;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (moveSpeed == 3.5f && !isJumping)
            {
                isWalk = true;
                isRun = false;
            }
        }
        else
        {
            isWalk = false;
            isRun = false;
        }

        if(isWalk&&!isRun)
        {
            if(!audioSource.isPlaying)
                PlaySoundEffect("WALK");
        }
        if(!isWalk && isRun)
        {
            if (!audioSource.isPlaying)
                PlaySoundEffect("RUN");
            else
            {
                if(audioSource.clip == audioWalk)
                {
                    audioSource.Stop();
                    PlaySoundEffect("RUN");
                }
            }
        }
        if (!isWalk && !isRun)
        {
            audioSource.Stop();
        }
    }

    // 효과음 재생 메소드
    void PlaySoundEffect(string action)
    {
        switch (action)
        {
            case "WALK":
                audioSource.clip = audioWalk;
                break;
            case "RUN":
                audioSource.clip = audioRun;
                break;
        }
        audioSource.Play();
    }
}
