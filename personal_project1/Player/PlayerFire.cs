using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    PlayerController3 Player;   // �÷��̾���Ʈ�ѷ�3�� �ִ� isAttack���� �Ѿ� ��� �� �㰡�ϱ� ���ؼ�

    // �Ѿ��� ������ ���� ������
    public GameObject bulletFactory_R;
    // �ѱ�   ������
    public GameObject firePostion_R;

    // �Ѿ��� ������ ����   ����
    public GameObject bulletFactory_L;
    // �ѱ�   ����
    public GameObject firePostion_L;

    public Animator animator;  //�ִϸ����� ������ ���� ���� 

    // ȿ����
    public AudioClip audioAttack;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Player = FindObjectOfType<PlayerController3>();
    }

    bool isRight=true;  //���������� ���ϸ� Ʈ��
    // Update is called once per frame
    void Update()
    {
        // isAttack�� true�� �Ѿ� �߻� ����
        if(Player.isAttack)
        {
            // �÷��̾ ��Ʈ�� Ű�� ������ �Ѿ� �߻��ϱ�
            // �÷��̾ ��Ʈ�� Ű�� ����
            if (Input.GetKeyDown(KeyCode.LeftControl)|| Input.GetKeyDown(KeyCode.RightControl))
            {
                //�����ϴ� �÷��̾� �ִϸ��̼� ���
                animator.SetTrigger("doAttack");

                PlaySoundEffect("ATTACK");
                //�Ѿ� ���忡�� �Ѿ� �����
                //�Ѿ� �߻�(�Ѿ��� �ѱ� ��ġ�� ������ ����)
                if (isRight) //�������̴�
                {
                    //���������� ���� �Ѿ� �߻�
                    GameObject bullet = Instantiate(bulletFactory_R, firePostion_R.transform.position, firePostion_R.transform.rotation);
                    
                }
                else //�����̴�
                {
                    // �������� ���� �Ѿ� �߻�
                    GameObject bullet = Instantiate(bulletFactory_L, firePostion_L.transform.position, firePostion_L.transform.rotation);

                }
            }
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))    //����Ű ������ ������ �ѱ� ������ Ȱ��ȭ, �׸��� �����ʸ� �� �߻� ����
        {
            firePostion_R.SetActive(true);
            firePostion_L.SetActive(false);
            isRight = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))   //����Ű ���� ������ �ѱ� ���� Ȱ��ȭ, �׸��� ���ʸ� �� �߻� ����
        {
            firePostion_R.SetActive(false);
            firePostion_L.SetActive(true);
            isRight = false;
        }
    }

    // ȿ���� ��� �޼ҵ�
    void PlaySoundEffect(string action)
    {
        switch (action)
        {
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
        }
        audioSource.Play();
    }
}
