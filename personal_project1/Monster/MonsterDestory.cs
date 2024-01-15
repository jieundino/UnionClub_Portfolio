using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDestory : MonoBehaviour
{
    GameManagerStage2 stage2;

    // 청소할 몬스터
    GameObject []monsters;

    // Start is called before the first frame update
    void Start()
    {
        stage2 = FindObjectOfType<GameManagerStage2>();
    }

    // Update is called once per frame
    void Update()
    {
        // 제한 시간 다 끝나면
        // 만약에 몬스터들이 있다면 맵2에 있던 몬스터들 제거.
        if (stage2.setTime<1)
        {
            // 몬스터들이 있는 지 검색
            if((monsters=GameObject.FindGameObjectsWithTag("Monster"))!=null)
            {
                //있으면
                monsters = GameObject.FindGameObjectsWithTag("Monster");
                //받아옴       // 배열에 몬스터들을 넣음
                //그리고 삭제함
                DestoryMonster();
            }
            //if(GameObject.FindGameObjectsWithTag)
        }
    }

    void DestoryMonster()
    {
        // 몬스터가 있다면 청소 시작
        if (monsters != null)
        {
            for(int i=0; i<monsters.Length;i++)
            {
                Debug.Log("몬스터 제거");
                Destroy(monsters[i]);
            }

        }
    }
   
}
