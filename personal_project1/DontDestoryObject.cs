using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryObject : MonoBehaviour
{
    private void Awake()
    {
        var obj = FindObjectsOfType<DontDestoryObject>();
        if (obj.Length == 1)    //중복이 아니라면 그대로 파괴하지 않음
        {
            DontDestroyOnLoad(gameObject);
        }
        else //중복이면 파괴
        {
            Destroy(gameObject);
        }
    }
}
