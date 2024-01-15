using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageDoor : MonoBehaviour
{
    //���� ���������� ���� ���� �̼� �����ϰ� gameManager.isSuccess = true;���� �ٲ�� ���⼭ ���Ǿ��� ��ȭ�ϸ� ���� ����� �� ����
    public bool isOpen = false; //�÷��̾ ���� ������ ���� �� ���� �ڵ����� true�� �ٲ㼭 �� �� �� �ְ� ��
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //����Ʈ Ű ������ ��
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (isOpen)
            {
                // ���� �� ���� �˾Ƴ��� ���� ������
                Scene scene = SceneManager.GetActiveScene();
                // �� �̸� Ȯ�� �� ���� ���������� �̵�
                if(scene.name=="Stage1")
                {
                    SceneManager.LoadScene("Stage2");
                }
                else if(scene.name == "Stage2")
                {
                    SceneManager.LoadScene("Stage3");
                }
                else if (scene.name == "Stage3")
                {
                    SceneManager.LoadScene("Forest Exit");
                }
                Debug.Log("���� ���������� �̵�");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // ���� �ݶ��̴� ���� �ȿ� �� �ְ�
        if (other.tag == "Player") //�� ����� �±װ� �÷��̾��
        {
            isOpen = true;  //�� �� �ְ� ��
        }
    }
}
