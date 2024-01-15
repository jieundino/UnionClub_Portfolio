using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_Monster : MonoBehaviour
{
    // ��������2������ ���� ���� ��ũ��Ʈ

    Rigidbody2D rigid;
    public GameObject target;       // ��������2������ ���Ͱ� �������� ������ ���� ������ ���̶� Ÿ��.
    SpriteRenderer spriteRenderer;
    Animator animator;  //�ִϸ����� ������ ���� ���� 

    // ���� �̵��ӵ�
    float moveSpeed = 0.8f;

    // ������ hp
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
        //Ÿ�� ����
        target= GameObject.FindWithTag("S2_House");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ���Ͱ� �����ʿ��� ���� ������ �̹��� �¿� ���� �� �ϰ�
        if (transform.position.x>0)
        {
            spriteRenderer.flipX = true;
        }
        else        // ���ʿ��� ���� ������ �̹��� �¿� ����
        {
            spriteRenderer.flipX = false;
        }
        MonsterDie();
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget() //���� ���� ������
    {
        if (target != null) //Ȥ�� �� ������ ���� Ÿ���� ���� �ִ��� �˻�
        {
            Vector3 targetPosition = target.transform.position; //Ÿ���� ��ġ�� ���� ������ ����
            Vector3 myPosition = transform.position;            //���� �ڽ��� ��ġ ���� ������ ����

            transform.position = Vector2.MoveTowards(myPosition, targetPosition, moveSpeed * Time.deltaTime);   //�ڱ� ��ġ���� Ÿ�� ��ġ�� �̵�
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �Ѿ˿� �ε����� ������ hp ����
        if(collision.gameObject.tag=="Bullet")
        {
            OnDamaged(collision.transform.position);

            //�Ѿ� �ε������ϱ� �Ѿ� ����
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //���Ϳ� �Ͽ콺�� �浹�ϸ�
    {
        if (collision.gameObject.tag == "S2_House")
        {
            Debug.Log("���Ϳ� �Ͽ콺 �浹");
            Destroy(gameObject,0.5f);  //���� �����ϰ�
            //�Ͽ콺�� ������ ����
            stage2.HouseDamaged();
        }
    }

    void MonsterDie()
    {
        // ���� hp 0�� �Ǹ� ���� ����
        if(monsterHP==0)
        {
            //���� ���߰�
            moveSpeed = 0.1f;
            //���� Die �ִϸ��̼�
            animator.SetTrigger("doDie");
            //���� �״� �ִϸ��̼� ���̰� �����ؾ� �ؼ� 1.5�� �ڿ� ����
            Destroy(gameObject,1.5f);
        }
    }

    void OnDamaged(Vector2 tartgetPos)
    {
        PlaySoundEffect("DAMAGED");
        // �������� ������ �ǰ� �پ��.
        monsterHP -= 1;

        gameObject.layer = 14; // MonsterDamaged Layer number�� 11�� �����Ǿ�����

        // ������ ƨ�ܳ����� ��
        int dirc = transform.position.x - tartgetPos.x > 0 ? 1 : -1;
        //ƨ�ܳ����� �������� -> ���� ��ġ(x) - �浹�� ������Ʈ��ġ(x) > 0: ���Ͱ� ������Ʈ�� �������� ��� �־����� �Ǻ�
        //> 0�̸� 1(���������� ƨ��) , <=0 �̸� -1 (�������� ƨ��)
        rigid.AddForce(new Vector2(dirc, 1) * 5, ForceMode2D.Impulse); // *5�� ƨ�ܳ����� ������ �ǹ� 

        // �ִϸ��̼�
        animator.SetTrigger("doDamaged");
        Invoke("OffDamaged", 1f);   //���Ͱ� ���ݴ��ϸ� 1�ʰ� �����̾��ٰ� ���� ����.
    }

    void OffDamaged()
    {
        // ������ ���� ���̾�� �ٲ�
        gameObject.layer = 10;
    }

    // ȿ���� ��� �޼ҵ�
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
