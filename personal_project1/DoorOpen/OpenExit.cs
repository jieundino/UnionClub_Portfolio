using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenExit : MonoBehaviour
{
    public bool isOpen = false; //�÷��̾ ���� ������ ���� �� ���� �ڵ����� true�� �ٲ㼭 �� �� �� �ְ� ��

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (isOpen)
            {
                //������ ������ �̵�
                Debug.Log("������ ������ �̵�");
                SceneManager.LoadScene("HomeAfter");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // ���� �ݶ��̴� ���� �ȿ� �� �ְ�
        if (other.tag == "Player") //�� ����� �±װ� �÷��̾��
        {
            isOpen = true;
        }
    }
}
