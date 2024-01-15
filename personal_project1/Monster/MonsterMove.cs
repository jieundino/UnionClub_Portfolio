using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    // �����ʿ��� �ܼ��ϰ� �����̴� ���� ��ũ��Ʈ

    Rigidbody2D rigid;
    public int nextMove;    //���� �ൿ��ǥ�� ������ ����
    Animator animator;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 5); // ����� �� ���� nextMove������ �ʱ�ȭ
    }

    void FixedUpdate()
    {
        // nextMove ������ ���� ������
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //�� ���� ���������� �ڵ��� ���ؼ� ������ Ž��
        // �ڽ��� �� �κп� ������ Ž��
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.4f, rigid.position.y);

        //��ĭ �� �κоƷ� ������ ���� ��
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        //���̸� ���� ���� ������Ʈ�� Ž�� 
        RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));

        //Ž���� ������Ʈ�� null : �� �տ� ������ ����
        if (raycast.collider == null)
        {
            //�׷��� ���ؼ� ���ư�
            Turn();
        }


    }

    void Think()    //���Ͱ� �˾Ƽ� �����ؼ� ���� �ൿ ����(-1 �����̵�, 1 �������̵�, 0 ����)
    {
        nextMove = Random.Range(-1, 2);

        //�ִϸ��̼�
        //WalkSpeed������ nextMove�� �ʱ�ȭ
        animator.SetInteger("WalkSpeed", nextMove);

        // ���Ͱ� ���� �������� �̹��� �¿� ����
        if (nextMove != 0) //������ ������ ���� �� �ٲ�
            spriteRenderer.flipX = nextMove == -1;   //nextMove�� ���� -1�̸� �¿����


        float time = Random.Range(2f, 5f); //�����ϴ� �ð��� �������� �ο� 
        // ����Լ�. ����Լ��� �޼ҵ��� ������ �ٿ� ���� ���� ����.
        Invoke("Think", time); //�Ű������� ���� �Լ��� time���� �����̸� �ο��Ͽ� �������
    }

    void Turn()
    {
        nextMove = nextMove * (-1); //�츮�� ���� ������ �ٲ����� ��ũ�� ��� ���߱�
        spriteRenderer.flipX = nextMove == -1;

        CancelInvoke(); //Think�� ��� ���� �� �ٽ� ����
        Invoke("Think", 2f);
    }

}
