using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_isDone : MonoBehaviour
{
    // 비파괴 개체로 만들어서 책상씬으로 갔을 때 isDone을 체크하고 돌아옴.
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
