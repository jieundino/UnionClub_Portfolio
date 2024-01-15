using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyManager : MonoBehaviour
{
    // 현재 시간
    float currentTime;

    // 최소 시간
    float minTime = 5;
    // 최대 시간
    float maxTime = 10;

    // 일정 시간
    public float createTime;
    // 몬스터 공장
    public GameObject monsterFactory;

    // 몬스터도 1분 동안만 생성하게 함
    float setTime = 60;

    // 몬스터
    GameObject monster;

    // Start is called before the first frame update
    void Start()
    {
        // 바로 몬스터가 안 나오면 재미 없으니까 처음에 Start에서 몬스터 한번 생성함
        //몬스터 공장에서 몬스터 생성해서
        monster = Instantiate(monsterFactory);
        //맵에 갖다 두기
        monster.transform.position = transform.position;

        // 태어날 때 몬스터 생성 시간 설정하고
        createTime = UnityEngine.Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        setTime -= Time.deltaTime;

        // 제한 시간 동안에만 몬스터 생성함.
        if(setTime>0)
        {
            // 시간이 흐르다가
            currentTime += Time.deltaTime;

            // 만약 현재 시간이 일정 시간이 되면
            if (currentTime > createTime)
            {
                //몬스터 공장에서 몬스터 생성해서
                monster = Instantiate(monsterFactory);
                //맵에 갖다 두기
                monster.transform.position = transform.position;
                // 현재 시간을 0으로 초기화
                currentTime = 0;
                // 적을 생성한 후에 적의 생성 시간을 다시 설정하기
                createTime = UnityEngine.Random.Range(minTime, maxTime);
            }
        }

    }


}
