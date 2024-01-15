using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController4 : MonoBehaviour
{
    // �������� 3 �÷��̾� ��Ʈ�ѷ�

    public GameManager gameManager;

    Rigidbody2D rigid;  //�����̵��� ���� ���� ���� 
    Animator animator;  //�ִϸ����� ������ ���� ���� 
    float jumpForce = 15f;  //���� ��
    float maxSpeed = 10f;    //�ִ� �ӵ�
    SpriteRenderer spriteRenderer;  //��������Ʈ�������� �÷��̾� ���⿡ ���� �̹��� ����������
    int direction;  // ���� ����
    GameObject scanObject;  // �÷��̾ ��ĵ�� ������Ʈ�� ���� ����

    bool isGet = false;     // �÷��̾ �������� ���� �� ���� ������
    bool getItem = false;   // �÷��̾ GŰ�� ������ �� ������ true�� �ٲ�

    GameManagerStage3 stage3;

    // ȿ����
    public AudioClip audioJump;
    public AudioClip audioTalk;
    public AudioClip audioDoor;
    public AudioClip audioItem;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        this.rigid = GetComponent<Rigidbody2D>(); //���� �ʱ�ȭ 
        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>(); // �ʱ�ȭ
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        stage3 = FindObjectOfType<GameManagerStage3>();
    }

    void Update()
    {
        // ��ư���� ���� ���� ���� �ܹ����� Ű���� �Է��� FixedUpdate���� Update�� ���°� Ű���� �Է��� ������ Ȯ���� ������

        // �����̽��� ������ ����
        // �׸��� ���� �ִϸ��̼��� ������ �ִ� ���°� �ƴ� ��� ������.
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            if (scanObject != null) //�����Ұ� ������ ��ȭâ��. ��� ������ �� ��.
            {
                PlaySoundEffect("TALK");
                // npc�� �����̽��ٷ� ���� �����ϰ� ��.
                if (scanObject.name == "NPC4")
                {
                    gameManager.Action(scanObject);
                    if (!gameManager.isSuccess)  // ���� �� �� ���¸� ����Ʈ ����
                    {
                        //���⼭ ��ȣ�ۿ��� ���� ������Ʈ�� npc4 ���̶� ���⿡�� isQuest�� true�� �������
                        stage3.isQuest = true;
                    }
                    else
                    {       // ������ ���¿��� ���� �ɸ� ���� ��Ÿ���� ��
                        stage3.isDoor = true;
                    }
                }
            }
            else //�ƹ��͵� ������ ���� ����.
            {
                rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetBool("isJumping", true);
                PlaySoundEffect("JUMP");
            }
        }

        // ����Ű ���� �������� �̹��� �¿� ����
        if (gameManager.isAction == false && Input.GetButton("Horizontal")) //��� ��ȭâ�� ���������� �� ������.
        {
            if (Input.GetAxisRaw("Horizontal") == -1)   // ĳ���Ͱ� ������ ���� -1
            {
                spriteRenderer.flipX = true;
                direction = -1;
            }
            else // �������� ���� 1
            {
                spriteRenderer.flipX = false;
                direction = 1;
            }
        }

        // ���߸� �ִϸ��̼� idle���·�
        if (Mathf.Abs(rigid.velocity.x) < 0.3)  //�÷��̾��� x���� �ӵ��� ���밪���� �ٲ��ְ� 0.3���� ������ idle���·�(����)
            animator.SetBool("isWalking", false);
        else //�̵� ��
            animator.SetBool("isWalking", true);

        if (isGet)   //���� �� �ִٰ� Ȱ��ȭ �� ���� GŰ�� �������� ����
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                getItem = true;
                PlaySoundEffect("ITEM");
            }
        }

    }

    // �÷��̾� ĳ���Ͱ� �ٶ󺸴� ������ �պκ��� ���̸� ���� Ž���ϱ� ���� Ž�� �Ÿ�
    //float detect_range = 1.5f;

    //�������� Ű ������Ʈ
    private void FixedUpdate()
    {
        // Ű�� �̵��ϱ�
        // ��� ��ȭâ�� ���������� �������� ����.
        float h = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //���������� �̵� (+) 
        if (rigid.velocity.x > maxSpeed)    //maxSpeed�� �ʹ� ������ �̵��ϴ� ���� ����
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);   //�ִ� �ӷ��� ������ �ִ� �ӷ����� �ɾ�� maxSpeed�� �ӷ� �ٲ���.
        // �������� �̵� (-) 
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);  //y���� ������ �����̹Ƿ� 0���� ������ �θ� �ȵ� 


        // �����ϰ� ���� ������ �����ִ� �ִϸ��̼����� ���ư��� �ϴ� �ڵ�

        // �÷��̾ �Ʒ��� �������� ���� ���
        if (rigid.velocity.y < 0)
        {
            // �Ʒ��� ���� ��. (����״� ���ӻ󿡼������� ���� ) ������ġ, ���� ����, ���� �� 
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            // ���� "Ground"���̾ �ش��ϴ� �༮�� ��ĵ��
            RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Ground"));

            // ����ĳ��Ʈ�� �ݶ��̴��� ���� ��ü�� �˻���  -> ���������� collider�� ������������ 
            // ���� �� ������
            if (rayhit.collider != null)
            {
                //�÷��̾ ���� �����ϰ� �������(�÷��̾� ũ���� ���� ������ ���� �Ÿ�.
                //(����ĳ��Ʈ�� �÷��̾� �߽ɺ��� ������))
                if (rayhit.distance < 0.5f)
                {
                    animator.SetBool("isJumping", false); //�Ÿ��� 0.5���� �۾����� ����
                }
            }
        }

        //���� �׼�
        Debug.DrawRay(rigid.position, new Vector3(direction * 1, 0, 0), new Color(0, 0, 1));

        //Layer�� Object�� ��ü�� rayHit_detect�� ����
        //�տ��� �÷��̾��� ������ ���� direction ������ ���� ��� ������ ��ȯ��.
        RaycastHit2D rayHit_detect = Physics2D.Raycast(rigid.position, new Vector3(direction, 0, 0), 0.7f, LayerMask.GetMask("Object"));

        // �����Ǹ� scanObject�� ������Ʈ ����.
        if (rayHit_detect.collider != null)
        {
            if (rayHit_detect.distance < 0.3f)
            {
                scanObject = rayHit_detect.collider.gameObject;
                //Debug.Log(scanObject.name);
            }
        }
        else
        {
            scanObject = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            isGet = true;   // �÷��̾�� �����۰� ��������� ���� �� �ִ� �� Ȱ��ȭ

            if (getItem)     // GŰ�� ������ ������
            {
                // ȹ���� �� ���� �÷���.
                ++stage3.FlowerCount;

                Debug.Log("�� ȹ��!");

                Destroy(collision.gameObject);  //������Ʈ �Ҹ�.
                getItem = false;    //�ٽ� ��Ȱ��ȭ ����.
            }
        }
        else
        {
            isGet = false;  //�ƴ� ��쿡�� ��Ȱ��ȭ
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            isGet = false;   // �÷��̾�� �������� �������� ��Ȱ��ȭ
        }
    }

    // ȿ���� ��� �޼ҵ�
    void PlaySoundEffect(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "TALK":
                audioSource.clip = audioTalk;
                break;
            case "DOOR":
                audioSource.clip = audioDoor;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
        }
        audioSource.Play();
    }

}
