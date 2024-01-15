using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopDownPlayerController : MonoBehaviour
{
    // ž�ٿ��� ���� �÷��̾� ������

    public GameManager gameManager;

    public float Speed;

    Rigidbody2D rigid;
    float h;
    float v;
    bool isHorizonMove; //�������� �̵� ���ΰ�?
                        //->�̰����� �÷��̾ ���� ��ٸ��� �������� �����¿츸 �����̰� �밢�� �̵� ���ϴ� �� ����
    Animator animator;  //�ִϸ����� ������ ���� ���� 

    Vector3 dirVec;     //���� �ٶ󺸰� �ִ� ���Ⱚ�� ���� ���Ͱ�

    GameObject scanObject;  // �÷��̾ ��ĵ�� ������Ʈ�� ���� ����

    // ȿ����
    public AudioClip audioTalk;
    public AudioClip audioDoor;     //Ű�� ������ ������ �Ҹ��� ����� �ǰ� ���� ������ �̵������� ���ڴµ� �̰͵� �� �� ��.

    AudioSource audioSource;

    // ���� �� ���� ���� �����ϴ� ����
    public bool isCloset = false;   //������
    // ������ ������ ��
    public bool isMoon = false;     //����

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
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
        bool hUp = gameManager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = gameManager.isAction ? false : Input.GetButtonUp("Vertical");

        //���࿡ �¿����Ű ��������
        //���� AxisRaw�� ���� ���� ����, ���� �Ǵ��ϱ�.
        if (hDown)
            isHorizonMove = true;   //Ʈ���
        else if (vDown) //�ƴϸ�
            isHorizonMove = false;  //�޽�  
        else if (hUp || vUp)    //upŰ�� ���� ������ ���� �ӵ��� �����ؼ� üũ�ϱ�
            isHorizonMove = h != 0; 

        //�ִϸ��̼�
        //������ �Ķ���Ϳ� ���� ���� �� ������ �� ����->�̰ɷ� ��ũ �ִϸ��̼��� �� ���� �� �����Ŵ
        //�ִϸ��̼��� �Ұ����� �ٲ���ٸ� true�� �� �ٲٱ�.
        if (animator.GetInteger("hAxisRaw")!=h)
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
        if (Input.GetButtonDown("Jump")&& scanObject != null)
        {
            gameManager.Action(scanObject);
            PlaySoundEffect("TALK");

            // ��ĵ�� ������Ʈ�̸��� �޾ƿͼ� �̰� ���࿡ �����̸�
            if(scanObject.name=="Closet")
            {
                if (!gameManager.isSuccess)  //Ŭ���� �� �� ������ �� ���� �� ���� Ȱ��ȭ
                {
                    isCloset = true;
                }
                else //Ŭ���� �ѰŸ� ���� ��Ȱ��ȭ
                {
                    isCloset = false;
                }
            } 
            else if (scanObject.name == "RoomDoor")   //���࿡ �湮�̸�
            {
                if (!gameManager.isSuccess)  //Ŭ���� �� �� ������ �� �湮 ��Ȱ��ȭ
                {
                    isMoon = false;
                }
                else //Ŭ���� �ѰŸ� �湮 Ȱ��ȭ
                {
                    isMoon = true;
                }
            }
        }


        // ���� Ȱ��ȭ �� ���¶�� ����ƮŰ ������ ���� ������ �̵�
        if(isCloset)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.RightShift))
            {
                PlaySoundEffect("DOOR");
                //�� �Ա� ������ �̵�
                Debug.Log("�� �Ա��� �̵�");
                SceneManager.LoadScene("Forest Entrance");
            }
        }
       if(isMoon)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                PlaySoundEffect("DOOR");
                //����
                Debug.Log("���ǿ���");
                SceneManager.LoadScene("TitleScene");
            }
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
    void PlaySoundEffect(string action)
    {
        switch (action)
        {
            case "TALK":
                audioSource.clip = audioTalk;
                break;
            case "DOOR":
                audioSource.clip = audioDoor;
                break;
        }
        audioSource.Play();
    }

}
