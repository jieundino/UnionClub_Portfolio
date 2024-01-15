using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // ž�ٿ��� ���� �÷��̾� ������

    public GameManager gameManager;

    public float Speed;

    Rigidbody2D rigid;
    float h;
    float v;
    bool isHorizonMove;     //�������� �̵� ���ΰ�?
                            //->�̰����� �÷��̾ ���� ��ٸ��� �������� �����¿츸 �����̰� �밢�� �̵� ���ϴ� �� ����
    Animator animator;      //�ִϸ����� ������ ���� ���� 

    Vector3 dirVec;     //���� �ٶ󺸰� �ִ� ���Ⱚ�� ���� ���Ͱ�

    public GameObject scanObject;  // �÷��̾ ��ĵ�� ������Ʈ�� ���� ����

    // ȿ����
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
        // Ű ������ Update���� ó����. FixedUpdate������ Ű�� ������ ��Ȳ�� �߻��� �� �ֱ⿡
        // �¿� �̵�
        h = gameManager.isAction? 0 :Input.GetAxisRaw("Horizontal");    //���� �Ŵ����� ���� �׼�(��ȭâ)�� ���� ������ 0���� �������� ���ϰ� ��.
        // ���� �̵�
        v = gameManager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        //����Ű�� ���� boolŸ�� ���� ����
        // �굵 isAction�� ��ȭâ�� Ȱ��ȭ �Ǿ������� false�� �������� ���ϰ� �ϰ� �ƴ� ���¸� ������ �� �ְ� ��.
        bool hDown = gameManager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = gameManager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = gameManager.isAction ? false : Input.GetButtonUp("Horizontal");      // �¿� ����Ű �����ٰ� ���� �� Ʈ��
        bool vUp = gameManager.isAction ? false : Input.GetButtonUp("Vertical");        // ���Ʒ� ����Ű �����ٰ� ���� �� Ʈ��     

        //���࿡ �¿����Ű ��������
        //���� AxisRaw�� ���� ���� ����, ���� �Ǵ��ϱ�.
        if (hDown)      //�¿� Ű ������ ��
            isHorizonMove = true;
        else if (vDown) //���Ʒ� Ű ������ ��
            isHorizonMove = false; 
        else if (hUp || vUp)    //upŰ�� ���� ��(Ű �����ٰ� �� ����)���� ���� �ӵ��� �����ؼ� üũ�ϱ�
            isHorizonMove = h != 0;

        //�ִϸ��̼�
        //������ �Ķ���Ϳ� ���� ���� �� ������ �� ����->�̰ɷ� ��ũ �ִϸ��̼��� �� ���� �� �����Ŵ
        //�ִϸ��̼��� �Ұ����� �ٲ���ٸ� true�� �� �ٲٱ�.
        if (animator.GetInteger("hAxisRaw") != h)
        {
            animator.SetBool("isChange", true);
            animator.SetInteger("hAxisRaw", (int)h);    //h���� �¿�� �����̴� ���� ����� ������, ������ ����
        }
        else if (animator.GetInteger("vAxisRaw") != v)
        {
            animator.SetBool("isChange", true);
            animator.SetInteger("vAxisRaw", (int)v);    //v���� ���Ʒ��� �����̴� ���� ����� ����, ������ �Ʒ���
        }
        else
        {   //�� �ٲ������ false��
            animator.SetBool("isChange", false);
        }

        //����
        if (vDown && v == 1)
            dirVec = Vector3.up;    //����Ű ������ ���� 1�̸� ����
        else if (vDown && v == -1)
            dirVec = Vector3.down;  //-1�̸� �Ʒ���
        else if (hDown && h == -1)
            dirVec = Vector3.left;  //�¿�Ű ������ ���� -1�̸� ����
        else if (hDown && h == 1)
            dirVec = Vector3.right; //1�̸� ������

        //��ĵ ������Ʈ
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            gameManager.Action(scanObject);
            PlaySoundEffect("TALK");
            Debug.Log(scanObject.name);
        } 
    }

    private void FixedUpdate()
    {
        // ������(���� �о��ִ� ��)�� FixedUpdate���� ����
        // ���� �¿�� �����̰� �ִ°�? ������ �¿� �����Ӹ� �ְ�(h,0) �ƴϸ� ���� ������(0,v)
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

        //���� �׼�
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(1, 0, 0));

        //Layer�� Object�� ��ü�� rayHit_detect�� ����
        //�տ��� �÷��̾��� ������ ���� direction ������ ���� ��� ������ ��ȯ��.
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        // �����Ǹ� scanObject�� ������Ʈ ����.
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

    // ȿ���� ��� �޼ҵ�
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
