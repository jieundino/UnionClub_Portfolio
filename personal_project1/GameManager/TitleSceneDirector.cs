using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneDirector : MonoBehaviour
{
    void Update()
    {
        // Ÿ��Ʋ ������
        // �ƹ�Ű�� ������ ����
        if (Input.anyKeyDown)
        {
            // ������ �� ������ �̵�.
            SceneManager.LoadScene("HomeBefore");
        }

    }
}
