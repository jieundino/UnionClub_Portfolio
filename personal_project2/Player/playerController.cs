using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // 탑다운일 때의 플레이어 움직임

    public GameManager gameManager;

    public float Speed;

    Rigidbody2D rigid;
    float h;
    float v;
    bool isHorizonMove;     //수평으로 이동 중인가?
                            //->이것으로 플레이어가 투디 쯔꾸르식 움직임인 상하좌우만 움직이고 대각선 이동 못하는 거 구현
    Animator animator;      //애니메이터 조작을 위한 변수 

    Vector3 dirVec;     //현재 바라보고 있는 방향값을 가진 벡터값

    public GameObject scanObject;  // 플레이어가 스캔한 오브젝트를 넣을 변수

    // 효과음
    public AudioClip audioTalk;

    AudioSource audioSource;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 키 관련은 Update에서 처리함. FixedUpdate에서는 키가 먹히는 상황이 발생할 수 있기에
        // 좌우 이동
        h = gameManager.isAction? 0 :Input.GetAxisRaw("Horizontal");    //게임 매니저의 이즈 액션(대화창)이 열려 있으면 0으로 움직이지 못하게 함.
        // 상하 이동
        v = gameManager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        //방향키에 따라서 bool타입 변수 지정
        // 얘도 isAction인 대화창이 활성화 되어있으면 false로 움직이지 못하게 하고 아닌 상태면 움직일 수 있게 함.
        bool hDown = gameManager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = gameManager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = gameManager.isAction ? false : Input.GetButtonUp("Horizontal");      // 좌우 방향키 눌렀다가 땠을 때 트루
        bool vUp = gameManager.isAction ? false : Input.GetButtonUp("Vertical");        // 위아래 방향키 눌렀다가 땠을 때 트루     

        //만약에 좌우방향키 눌렀으면
        //현재 AxisRaw의 값에 따라 수평, 수직 판단하기.
        if (hDown)      //좌우 키 눌렀을 때
            isHorizonMove = true;
        else if (vDown) //위아래 키 눌렀을 때
            isHorizonMove = false; 
        else if (hUp || vUp)    //up키를 했을 때(키 눌렀다가 땐 상태)에는 현재 속도를 측정해서 체크하기
            isHorizonMove = h != 0;

        //애니메이션
        //기존의 파라미터에 같은 값이 들어가 있으면 안 넣음->이걸로 워크 애니메이션이 안 들어갔던 걸 재생시킴
        //애니메이션의 불값으로 바뀌었다면 true로 값 바꾸기.
        if (animator.GetInteger("hAxisRaw") != h)
        {
            animator.SetBool("isChange", true);
            animator.SetInteger("hAxisRaw", (int)h);    //h값인 좌우로 움직이는 쪽이 양수면 오른쪽, 음수면 왼쪽
        }
        else if (animator.GetInteger("vAxisRaw") != v)
        {
            animator.SetBool("isChange", true);
            animator.SetInteger("vAxisRaw", (int)v);    //v값인 위아래로 움직이는 쪽이 양수면 위쪽, 음수면 아래쪽
        }
        else
        {   //안 바뀌었으면 false로
            animator.SetBool("isChange", false);
        }

        //방향
        if (vDown && v == 1)
            dirVec = Vector3.up;    //상하키 누르고 값이 1이면 위쪽
        else if (vDown && v == -1)
            dirVec = Vector3.down;  //-1이면 아래쪽
        else if (hDown && h == -1)
            dirVec = Vector3.left;  //좌우키 누르고 값이 -1이면 왼쪽
        else if (hDown && h == 1)
            dirVec = Vector3.right; //1이면 오른쪽

        //스캔 오브젝트
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            gameManager.Action(scanObject);
            PlaySoundEffect("TALK");
            Debug.Log(scanObject.name);
        } 
    }

    private void FixedUpdate()
    {
        // 움직임(힘을 밀어주는 거)는 FixedUpdate에서 구현
        // 지금 좌우로 움직이고 있는가? 맞으면 좌우 움직임만 주고(h,0) 아니면 상하 움직임(0,v)
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

        //조사 액션
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(1, 0, 0));

        //Layer가 Object인 물체만 rayHit_detect에 감지
        //앞에서 플레이어의 방향을 넣은 direction 변수로 빔을 쏘는 방향을 전환함.
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        // 감지되면 scanObject에 오브젝트 저장.
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
            //Debug.Log(scanObject.name);
        }
        else
        {
            scanObject = null;
        }
    }

    // 효과음 재생 메소드
    public void PlaySoundEffect(string action)
    {
        switch (action)
        {
            case "TALK":
                audioSource.clip = audioTalk;
                break;
        }
        audioSource.Play();
    }
}
