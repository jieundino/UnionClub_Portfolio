using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
    // 스테이지 2 플레이어 컨트롤러

    public GameManager gameManager;
    Rigidbody2D rigid;  //물리이동을 위한 변수 선언 
    Animator animator;  //애니메이터 조작을 위한 변수 
    float jumpForce = 15f;  //점프 힘
    float maxSpeed = 5f;    //최대 속도
    SpriteRenderer spriteRenderer;  //스프라이트렌더러로 플레이어 방향에 따라 이미지 반전시켜줌
    int direction;  // 방향 변수
    GameObject scanObject;  // 플레이어가 스캔한 오브젝트를 넣을 변수

    bool isReady = false;       // 집지키기 입장 아직 허가X
    public bool isAttack = false;      // 공격 가능하게 할지 말지 결정. 처음에는 공격 못 하고 퀘스트 받고 집지키기 맵에서만 활성화

    // 집지키기 시작시킬 불타입 변수
    public bool isStart = false;

    GameManagerStage2 stage2;

    public GameObject map1; // 집지키기 입장 맵
    public GameObject map2; // 집지키기 맵
    public GameObject map3; // 완료하고 퇴장 맵

    // 효과음
    public AudioClip audioJump;
    public AudioClip audioTalk;
    public AudioClip audioDoor;
    public AudioClip audioItem;
    public AudioClip audioDamaged;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        this.rigid = GetComponent<Rigidbody2D>(); //변수 초기화 
        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>(); // 초기화
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
        // 버튼에서 손을 떄는 등의 단발적인 키보드 입력은 FixedUpdate보다 Update에 쓰는게 키보드 입력이 누락될 확률이 낮아짐

        // 스페이스바 누르면 점프
        // 그리고 점프 애니메이션이 나오고 있는 상태가 아닐 경우 점프함.
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            if (scanObject != null) //조사할게 있으면 대화창뜸. 대신 점프는 안 됨.
            {
                // npc만 스페이스바로 조사 가능하게 함.
                if (scanObject.name == "NPC3")
                {
                    PlaySoundEffect("TALK");
                    gameManager.Action(scanObject);
                    if (!gameManager.isSuccess)  // 성공 안 한 상태면
                    {
                        //여기서 상호작용할 만한 오브젝트가 npc3뿐이라서 여기에서 isReady를 true로 만들어줌
                        //isReady를 트루로 해서 집지키기 맵에 들어갈 준비 가능하게 함.
                        isReady = true;
                    }
                    else
                    {       // 성공한 상태에서 말을 걸면 문이 나타나게 함
                            stage2.isDoor = true;
                    }
                }
            }
            else //아무것도 없으면 점프 가능.
            {
                rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetBool("isJumping", true);
                PlaySoundEffect("JUMP");
            }
        }

        // 방향키 누른 방향으로 이미지 좌우 반전
        if (gameManager.isAction == false && Input.GetButton("Horizontal")) //대신 대화창이 열려있으면 못 움직임.
        {
            if (Input.GetAxisRaw("Horizontal") == -1)   // 캐릭터가 왼쪽을 보면 -1
            {
                spriteRenderer.flipX = true;
                direction = -1;
            }
            else // 오른쪽을 보면 1
            {
                spriteRenderer.flipX = false;
                direction = 1;
            }
        }

        // 멈추면 애니메이션 idle상태로
        if (Mathf.Abs(rigid.velocity.x) < 0.3)  //플레이어의 x축의 속도를 절대값으로 바꿔주고 0.3보다 작으면 idle상태로(멈춤)
            animator.SetBool("isWalking", false);
        else //이동 중
            animator.SetBool("isWalking", true);

        if(goMap2)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                map1.SetActive(false);
                map2.SetActive(true);
                isAttack = true;    //공격 허가
                isStart = true;     //시작
            }
        }

    }

    // 플레이어 캐릭터가 바라보는 방향의 앞부분을 레이를 쏴서 탐사하기 위한 탐지 거리
    //float detect_range = 1.5f;

    //지속적인 키 업데이트
    private void FixedUpdate()
    {
        // 키로 이동하기
        // 대신 대화창이 열려있으면 움직이지 못함.
        float h = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //오른쪽으로 이동 (+) 
        if (rigid.velocity.x > maxSpeed)    //maxSpeed로 너무 빠르게 이동하는 것을 막음
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);   //최대 속력을 넘으면 최대 속력으로 걸어둔 maxSpeed로 속력 바꿔줌.
        // 왼쪽으로 이동 (-) 
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);  //y값은 점프의 영향이므로 0으로 제한을 두면 안됨 


        // 점프하고 땅에 닿으면 원래있던 애니메이션으로 돌아가게 하는 코드

        // 플레이어가 아래로 내려가고 있을 경우
        if (rigid.velocity.y < 0)
        {
            // 아래로 빔을 쏨. (디버그는 게임상에서보이지 않음 ) 시작위치, 어디로 쏠지, 빔의 색 
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            // 쏴서 "Ground"레이어에 해당하는 녀석만 스캔함
            RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Ground"));

            // 레이캐스트의 콜라이더로 닿은 물체를 검색함  -> 맞지않으면 collider도 생성되지않음 
            // 닿은 게 있으면
            if (rayhit.collider != null)
            {
                //플레이어가 땅과 밀착하게 닿았으면(플레이어 크기의 절반 정도가 닿은 거리.
                //(레이캐스트가 플레이어 중심부터 나가서))
                if (rayhit.distance < 0.5f)
                {
                    animator.SetBool("isJumping", false); //거리가 0.5보다 작아지면 변경
                }
            }
        }

        //조사 액션
        Debug.DrawRay(rigid.position, new Vector3(direction * 1, 0, 0), new Color(0, 0, 1));

        //Layer가 Object인 물체만 rayHit_detect에 감지
        //앞에서 플레이어의 방향을 넣은 direction 변수로 빔을 쏘는 방향을 전환함.
        RaycastHit2D rayHit_detect = Physics2D.Raycast(rigid.position, new Vector3(direction, 0, 0), 0.7f, LayerMask.GetMask("Object"));

        // 감지되면 scanObject에 오브젝트 저장.
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

    // 집지키기 맵으로 이동하게 하는 쉬프트 키를 먹게 해주거나 퀘스트 안 받으면 못 가게 함
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
            OnDamaged(collision.transform.position);    //현재 충돌한 오브젝트인 몬스터의 위치값을 넘겨줌
        }
    }

    void OnDamaged(Vector2 tartgetPos)
    {
        // 데미지를 입으면 피가 줄어듦.
        gameManager.HpDown();

        gameObject.layer = 11; // PlayerDamaged Layer number가 11로 지정되어있음

        // 맞으면 튕겨나가게 함
        int dirc = transform.position.x - tartgetPos.x > 0 ? 1 : -1;
        //튕겨나가는 방향지정 -> 플레이어 위치(x) - 충돌한 오브젝트위치(x) > 0: 플레이어가 오브젝트를 기준으로 어디에 있었는지 판별
        //> 0이면 1(오른쪽으로 튕김) , <=0 이면 -1 (왼쪽으로 튕김)
        rigid.AddForce(new Vector2(dirc, 1) * 5, ForceMode2D.Impulse); // *5은 튕겨나가는 강도를 의미 

        // 애니메이션
        animator.SetTrigger("doDamaged");
        Invoke("OffDamaged", 3f);
    }

    void OffDamaged()
    {
        gameObject.layer = 6;
    }

    // 효과음 재생 메소드
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
