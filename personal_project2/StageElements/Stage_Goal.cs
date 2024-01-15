using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(SceneManager.GetActiveScene().name == "Stage2")
            {
                Debug.Log("����! ��������2 Ŭ����.");
                // ��������3 ���� �̵�
                SceneManager.LoadScene("Stage3");
            }
            else if(SceneManager.GetActiveScene().name == "Stage3")
            {
                Debug.Log("��������3 Ŭ����.");
                // ��������4 ���� �̵�
                SceneManager.LoadScene("Stage4");
            }
            else if (SceneManager.GetActiveScene().name == "Stage4")
            {
                Debug.Log("����! ��������4 Ŭ����.");
                // ��������5 ���� �̵�
                SceneManager.LoadScene("Stage5");
            }
            else if (SceneManager.GetActiveScene().name == "Stage5")
            {
                Debug.Log("��������5 Ŭ����. �������� �̵�.");
                // EndingMov ���� �̵�
                SceneManager.LoadScene("EndingMov");
            }
        }
    }
}
