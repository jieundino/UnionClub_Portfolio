using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_isDone : MonoBehaviour
{
    // ���ı� ��ü�� ���� å������� ���� �� isDone�� üũ�ϰ� ���ƿ�.
    public bool isDone = false;

    #region singleton
    private void Awake()
    {
        var obj = FindObjectsOfType<Stage5_isDone>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion singleton
}
