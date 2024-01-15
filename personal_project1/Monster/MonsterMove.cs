using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    // 점프맵에서 단순하게 움직이는 몬스터 스크립트

    Rigidbody2D rigid;
    public int nextMove;    //다음 행동지표를 결정할 변수
    Animator animator;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 5); // 실행될 때 마다 nextMove변수가 초기화
    }

    void FixedUpdate()
    {
        // nextMove 변수에 따라서 움직임
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //맵 앞이 낭떨어지면 뒤돌기 위해서 지형을 탐색
        // 자신의 앞 부분에 지형을 탐색
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.4f, rigid.position.y);

        //한칸 앞 부분아래 쪽으로 빔을 쏨
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        //레이를 쏴서 맞은 오브젝트를 탐지 
        RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));

        //탐지된 오브젝트가 null : 그 앞에 지형이 없음
        if (raycast.collider == null)
        {
            //그러면 턴해서 돌아감
            Turn();
        }


    }

    void Think()    //몬스터가 알아서 생각해서 다음 행동 결정(-1 왼쪽이동, 1 오른쪽이동, 0 멈춤)
    {
        nextMove = Random.Range(-1, 2);

        //애니메이션
        //WalkSpeed변수를 nextMove로 초기화
        animator.SetInteger("WalkSpeed", nextMove);

        // 몬스터가 가는 방향으로 이미지 좌우 반전
        if (nextMove != 0) //서있을 때에는 방향 안 바꿈
            spriteRenderer.flipX = nextMove == -1;   //nextMove의 값이 -1이면 좌우반전


        float time = Random.Range(2f, 5f); //생각하는 시간을 랜덤으로 부여 
        // 재귀함수. 재귀함수는 메소드의 마지막 줄에 쓰는 것이 좋다.
        Invoke("Think", time); //매개변수로 받은 함수를 time초의 딜레이를 부여하여 재실행함
    }

    void Turn()
    {
        nextMove = nextMove * (-1); //우리가 직접 방향을 바꿔저서 띵크는 잠시 멈추기
        spriteRenderer.flipX = nextMove == -1;

        CancelInvoke(); //Think를 잠시 멈춘 후 다시 실행
        Invoke("Think", 2f);
    }

}
