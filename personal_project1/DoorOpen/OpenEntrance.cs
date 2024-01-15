using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenEntrance : MonoBehaviour
{
    public PlayerController controller;

    public bool isOpen = false; //�÷��̾ ���� ������ ���� �� ���� �ڵ����� true�� �ٲ㼭 �� �� �� �ְ� ��

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.RightShift))
        {
            if (isOpen)
            {
                //�������� 1 ������ �̵�
                Debug.Log("���� ���������� �̵�");
                SceneManager.LoadScene("Stage1");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //�䳢�� �㰡���� ��
        if(controller.isNextOpen)
        {
            // ���� �ݶ��̴� ���� �ȿ� �� �ְ�
            if (other.tag == "Player") //�� ����� �±װ� �÷��̾��
            {
                isOpen = true;
            }
        }

    }
}
