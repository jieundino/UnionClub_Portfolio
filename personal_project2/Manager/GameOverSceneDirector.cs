using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneDirector : MonoBehaviour
{
    void Update()
    {
        // �ƹ�Ű�� ������
        if (Input.anyKeyDown)
        {
            //Ÿ��Ʋ ������ �̵�.
            SceneManager.LoadScene("TitleScene");
        }
    }
}