using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyManager : MonoBehaviour
{
    // ���� �ð�
    float currentTime;

    // �ּ� �ð�
    float minTime = 5;
    // �ִ� �ð�
    float maxTime = 10;

    // ���� �ð�
    public float createTime;
    // ���� ����
    public GameObject monsterFactory;

    // ���͵� 1�� ���ȸ� �����ϰ� ��
    float setTime = 60;

    // ����
    GameObject monster;

    // Start is called before the first frame update
    void Start()
    {
        // �ٷ� ���Ͱ� �� ������ ��� �����ϱ� ó���� Start���� ���� �ѹ� ������
        //���� ���忡�� ���� �����ؼ�
        monster = Instantiate(monsterFactory);
        //�ʿ� ���� �α�
        monster.transform.position = transform.position;

        // �¾ �� ���� ���� �ð� �����ϰ�
        createTime = UnityEngine.Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        setTime -= Time.deltaTime;

        // ���� �ð� ���ȿ��� ���� ������.
        if(setTime>0)
        {
            // �ð��� �帣�ٰ�
            currentTime += Time.deltaTime;

            // ���� ���� �ð��� ���� �ð��� �Ǹ�
            if (currentTime > createTime)
            {
                //���� ���忡�� ���� �����ؼ�
                monster = Instantiate(monsterFactory);
                //�ʿ� ���� �α�
                monster.transform.position = transform.position;
                // ���� �ð��� 0���� �ʱ�ȭ
                currentTime = 0;
                // ���� ������ �Ŀ� ���� ���� �ð��� �ٽ� �����ϱ�
                createTime = UnityEngine.Random.Range(minTime, maxTime);
            }
        }

    }


}
