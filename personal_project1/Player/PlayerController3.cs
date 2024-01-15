using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
    // �������� 2 �÷��̾� ��Ʈ�ѷ�

    public GameManager gameManager;
    Rigidbody2D rigid;  //�����̵��� ���� ���� ���� 
    Animator animator;  //�ִϸ����� ������ ���� ���� 
    float jumpForce = 15f;  //���� ��
    float maxSpeed = 5f;    //�ִ� �ӵ�
    SpriteRenderer spriteRenderer;  //��������Ʈ�������� �÷��̾� ���⿡ ���� �̹��� ����������
    int direction;  // ���� ����
    GameObject scanObject;  // �÷��̾ ��ĵ�� ������Ʈ�� ���� ����

    bool isReady = false;       // ����Ű�� ���� ���� �㰡X
    public bool isAttack = false;      // ���� �����ϰ� ���� ���� ����. ó������ ���� �� �ϰ� ����Ʈ �ް� ����Ű�� �ʿ����� Ȱ��ȭ

    // ����Ű�� ���۽�ų ��Ÿ�� ����
    public bool isStart = false;

    GameManagerStage2 stage2;

    public GameObject map1; // ����Ű�� ���� ��
    public GameObject map2; // ����Ű�� ��
    public GameObject map3; // �Ϸ��ϰ� ���� ��

    // ȿ����
    public AudioClip audioJump;
    public AudioClip audioTalk;
    public AudioClip audioDoor;
    public AudioClip audioItem;
    public AudioClip audioDamaged;

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
        stage2 = FindObjectOfType<GameManagerStage2>();
        isReady = false;
        //isAttack = false;
        //isStart = false;
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
                // npc�� �����̽��ٷ� ���� �����ϰ� ��.
                if (scanObject.name == "NPC3")
                {
                    PlaySoundEffect("TALK");
                    gameManager.Action(scanObject);
                    if (!gameManager.isSuccess)  // ���� �� �� ���¸�
                    {
                        //���⼭ ��ȣ�ۿ��� ���� ������Ʈ�� npc3���̶� ���⿡�� isReady�� true�� �������
                        //isReady�� Ʈ��� �ؼ� ����Ű�� �ʿ� �� �غ� �����ϰ� ��.
                        isReady = true;
                    }
                    else
                    {       // ������ ���¿��� ���� �ɸ� ���� ��Ÿ���� ��
                            stage2.isDoor = true;
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

        if(goMap2)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                map1.SetActive(false);
                map2.SetActive(true);
                isAttack = true;    //���� �㰡
                isStart = true;     //����
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

    // ����Ű�� ������ �̵��ϰ� �ϴ� ����Ʈ Ű�� �԰� ���ְų� ����Ʈ �� ������ �� ���� ��
    bool goMap2 = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(isReady)
        {
            if (collision.gameObject.tag == "Ready")
            {
                goMap2 = true;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            PlaySoundEffect("DAMAGED");
            OnDamaged(collision.transform.position);    //���� �浹�� ������Ʈ�� ������ ��ġ���� �Ѱ���
        }
    }

    void OnDamaged(Vector2 tartgetPos)
    {
        // �������� ������ �ǰ� �پ��.
        gameManager.HpDown();

        gameObject.layer = 11; // PlayerDamaged Layer number�� 11�� �����Ǿ�����

        // ������ ƨ�ܳ����� ��
        int dirc = transform.position.x - tartgetPos.x > 0 ? 1 : -1;
        //ƨ�ܳ����� �������� -> �÷��̾� ��ġ(x) - �浹�� ������Ʈ��ġ(x) > 0: �÷��̾ ������Ʈ�� �������� ��� �־����� �Ǻ�
        //> 0�̸� 1(���������� ƨ��) , <=0 �̸� -1 (�������� ƨ��)
        rigid.AddForce(new Vector2(dirc, 1) * 5, ForceMode2D.Impulse); // *5�� ƨ�ܳ����� ������ �ǹ� 

        // �ִϸ��̼�
        animator.SetTrigger("doDamaged");
        Invoke("OffDamaged", 3f);
    }

    void OffDamaged()
    {
        gameObject.layer = 6;
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
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
        }
        audioSource.Play();
    }

}
