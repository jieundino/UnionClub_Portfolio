using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneDirector : MonoBehaviour
{
    public void StartBtn()
    {
        // Ÿ��Ʋ ������
        // �����ϱ� ��ư ������ ����
        // OpeningMov ������ �̵�
        SceneManager.LoadScene("OpeningMov");
    }
}
