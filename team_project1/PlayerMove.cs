using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�÷��̾� �̵�
public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 3.5f;

    CharacterController cc;

    public GameObject player;

    float gravity = -10f;

    float yVelocity = 0;

    public float jumpPower = 10f;

    public bool isJumping = false;


    // ȿ����
    public AudioClip audioWalk;
    public AudioClip audioRun;

    AudioSource audioSource;

    private bool isWalk = false;
    private bool isRun = false;

    //������ �ϱ� UI ������Ʈ ����
    public GameObject LetterPanel;


    public FinalManager Final;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        Final = FindObjectOfType<FinalManager>();
    }

    //���� �ε�ĥ�� ���̵� / ���翡�� �ϱ��� �� �� ����
    void OnTriggerEnter(Collider other)
    {
        if(Final.isLibrary)
        {
            if (other.tag == "Door_library")
            {
                Debug.Log("���� �̵�");
                //���� �� �̵��� �÷��̾� ��ġ ������ �ȵ� �̿� ���� �̾߱��ϱ�

                //player.transform.position += new Vector3(1, -2, -3);
                //transform.Translate(new Vector3(-1, 2, 0));
                SceneManager.LoadScene("StudyRoom");

            }
        }

        if(Final.isBath)
        {
            if (other.tag == "Door_restroom")
            {
                Debug.Log("ȭ��� �̵�");
                //transform.position += new Vector3(1, -2, -3);
                //transform.Translate(new Vector3(-1, 2, 0));
                SceneManager.LoadScene("BathRoom");

            }
        }


        if (other.tag == "Door")
        {
            Debug.Log("�������� �̵�");
            //transform.position += new Vector3(1, -2, -3);
            //transform.Translate(new Vector3(-1, 2, 0));
            SceneManager.LoadScene("Game Main Scene");

        }

        // ���翡 �ִ� ���� �߰��ϸ�
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

    // ȿ���� ��� �޼ҵ�
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
