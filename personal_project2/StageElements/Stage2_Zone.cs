using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2_Zone : MonoBehaviour
{
    // Stage2Manager ��ũ��Ʈ�� isChange ������ true�� A������ Ȱ��ȭ. false�� B������ Ȱ��ȭ��.
    public Stage2Manager stage2Manager;     
    
    // �� ��ũ��Ʈ�� ���� ������ �̸��� �����ͼ� ����. ������ A�� B�� ���еǾ� �ֱ� ����.
    string ZoneName;

    void Start()
    {
        ZoneName = this.gameObject.name;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (stage2Manager.isChange)  // A������ Ȱ��ȭ��.
        {
            if (ZoneName == "A")
            {
                if (other.gameObject.tag == "Player")
                {
                    Debug.Log("A�������� ��ѱ⿡�� �ɸ�! ���ӿ���.");
                    // ���ӿ���2 ������ �̵�
                    SceneManager.LoadScene("GameOver2");
                }

            }
            else
            {
                //B ������ ��
                return;
                // ���ϵ�.
            }
        }
        else  // B������ Ȱ��ȭ��.
        {
            if (ZoneName == "B")
            {
                if (other.gameObject.tag == "Player")
                {
                    Debug.Log("B �������� ��ѱ⿡�� �ɸ�! ���ӿ���.");
                    // ���ӿ���2 ������ �̵�
                    SceneManager.LoadScene("GameOver2");
                }
            }
            else
            {
                //A ������ ��
                return;
                // ���ϵ�.
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (stage2Manager.isChange)  // A������ Ȱ��ȭ��.
        {
            if (ZoneName == "A")
            {
                if (other.gameObject.tag == "Player")
                {
                    Debug.Log("A�������� ��ѱ⿡�� �ɸ�! ���ӿ���.");
                    // ���ӿ���2 ������ �̵�
                    SceneManager.LoadScene("GameOver2");
                }
            }
            else
            {
                //B ������ ��
                return;         // ���ϵ�.
            }
        }
        else  // B������ Ȱ��ȭ��.
        {
            if (ZoneName == "B")
            {
                if (other.gameObject.tag == "Player")
                {
                    Debug.Log("B �������� ��ѱ⿡�� �ɸ�! ���ӿ���.");
                    // ���ӿ���2 ������ �̵�
                    SceneManager.LoadScene("GameOver2");
                }

            }
            else
            {
                //A ������ ��
                return;         // ���ϵ�.
            }
        }
    }

}
