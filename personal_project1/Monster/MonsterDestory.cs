using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDestory : MonoBehaviour
{
    GameManagerStage2 stage2;

    // û���� ����
    GameObject []monsters;

    // Start is called before the first frame update
    void Start()
    {
        stage2 = FindObjectOfType<GameManagerStage2>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �ð� �� ������
        // ���࿡ ���͵��� �ִٸ� ��2�� �ִ� ���͵� ����.
        if (stage2.setTime<1)
        {
            // ���͵��� �ִ� �� �˻�
            if((monsters=GameObject.FindGameObjectsWithTag("Monster"))!=null)
            {
                //������
                monsters = GameObject.FindGameObjectsWithTag("Monster");
                //�޾ƿ�       // �迭�� ���͵��� ����
                //�׸��� ������
                DestoryMonster();
            }
            //if(GameObject.FindGameObjectsWithTag)
        }
    }

    void DestoryMonster()
    {
        // ���Ͱ� �ִٸ� û�� ����
        if (monsters != null)
        {
            for(int i=0; i<monsters.Length;i++)
            {
                Debug.Log("���� ����");
                Destroy(monsters[i]);
            }

        }
    }
   
}
