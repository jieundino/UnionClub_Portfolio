using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_Monster : MonoBehaviour
{
    // 스테이지2에서의 몬스터 전용 스크립트

    Rigidbody2D rigid;
    public GameObject target;       // 스테이지2에서는 몬스터가 개구리의 집으로 향해 움직일 것이라서 타겟.
    SpriteRenderer spriteRenderer;
    Animator animator;  //애니메이터 조작을 위한 변수 

    // 몬스터 이동속도
    float moveSpeed = 0.8f;

    // 몬스터의 hp
    public int monsterHP = 3;

    GameManagerStage2 stage2;

    public AudioClip audioDamaged;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.animator = GetComponent<Animator>();
        stage2 = FindObjectOfType<GameManagerStage2>();
        //타겟 설정
        target= GameObject.FindWithTag("S2_House");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 몬스터가 오른쪽에서 오고 있으면 이미지 좌우 반전 안 하고
        if (transform.position.x>0)
        {
            spriteRenderer.flipX = true;
        }
        else        // 왼쪽에서 오고 있으면 이미지 좌우 반전
        {
            spriteRenderer.flipX = false;
        }
        MonsterDie();
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget() //집을 향해 움직임
    {
        if (target != null) //혹시 모를 오류를 위해 타겟인 집이 있는지 검사
        {
            Vector3 targetPosition = target.transform.position; //타겟의 위치를 벡터 변수에 저장
            Vector3 myPosition = transform.position;            //몬스터 자신의 위치 벡터 변수에 저장

            transform.position = Vector2.MoveTowards(myPosition, targetPosition, moveSpeed * Time.deltaTime);   //자기 위치에서 타겟 위치로 이동
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 총알에 부딪히면 몬스터의 hp 감소
        if(collision.gameObject.tag=="Bullet")
        {
            OnDamaged(collision.transform.position);

            //총알 부딪혔으니까 총알 삭제
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //몬스터와 하우스와 충돌하면
    {
        if (collision.gameObject.tag == "S2_House")
        {
            Debug.Log("몬스터와 하우스 충돌");
            Destroy(gameObject,0.5f);  //몬스터 삭제하고
            //하우스에 데미지 들어옴
            stage2.HouseDamaged();
        }
    }

    void MonsterDie()
    {
        // 몬스터 hp 0이 되면 몬스터 죽음
        if(monsterHP==0)
        {
            //몬스터 멈추고
            moveSpeed = 0.1f;
            //몬스터 Die 애니메이션
            animator.SetTrigger("doDie");
            //몬스터 죽는 애니메이션 보이고 삭제해야 해서 1.5초 뒤에 삭제
            Destroy(gameObject,1.5f);
        }
    }

    void OnDamaged(Vector2 tartgetPos)
    {
        PlaySoundEffect("DAMAGED");
        // 데미지를 입으면 피가 줄어듦.
        monsterHP -= 1;

        gameObject.layer = 14; // MonsterDamaged Layer number가 11로 지정되어있음

        // 맞으면 튕겨나가게 함
        int dirc = transform.position.x - tartgetPos.x > 0 ? 1 : -1;
        //튕겨나가는 방향지정 -> 몬스터 위치(x) - 충돌한 오브젝트위치(x) > 0: 몬스터가 오브젝트를 기준으로 어디에 있었는지 판별
        //> 0이면 1(오른쪽으로 튕김) , <=0 이면 -1 (왼쪽으로 튕김)
        rigid.AddForce(new Vector2(dirc, 1) * 5, ForceMode2D.Impulse); // *5은 튕겨나가는 강도를 의미 

        // 애니메이션
        animator.SetTrigger("doDamaged");
        Invoke("OffDamaged", 1f);   //몬스터가 공격당하면 1초간 무적이었다가 무적 해제.
    }

    void OffDamaged()
    {
        // 원래의 몬스터 레이어로 바꿈
        gameObject.layer = 10;
    }

    // 효과음 재생 메소드
    void PlaySoundEffect(string action)
    {
        switch (action)
        {
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
        }
        audioSource.Play();
    }
}
