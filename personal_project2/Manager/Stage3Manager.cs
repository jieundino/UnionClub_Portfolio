using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3Manager : MonoBehaviour
{
    public bool isQuest = false;        // �渷�̿� ��ȭ�ϸ� �� ������ true�� �ǰ�(���ӸŴ����� isGet ���� �̿��ϱ�) ����Ʈ ���� ���� �����ϰ� ����.
                                        // �� ������ true�� �������� ���� �� ����.
    public bool isDogGum = false;       // ��������2 ������ true�� �ٲ���. �̰ɷ� ����Ʈ �������� Ȯ���� �����.
    public bool isComplete = false;     // ����Ʈ ���� ����
    // isComplete ���� true�� �ٲ��ְ� gameManager�� isMeet�� true�� �ٲ��ֱ�.

    public GameManager gameManager;

    public bool isBlackTalk = false;    // �����ڶ� ���ߴ��� ����(�̼� ���� ���Ŀ� �����ڿ� ��ȭ�ϰ� ���� �� ���� true�� ��.)

    public GameObject NextBlock;

    public GameObject dog1;

    public GameObject trashCan1;
    public GameObject trashCan2;
    public GameObject trashCan3;

    public string objName = null;

    // ���� �Ϸ���Ʈ �̹��� ����
    public Image illust1;
    public Image illust2;
    float time = 0f;
    float F_time = 1f;

    //public GameObject player;

    void Start()
    {
        //player = GameObject.Find("Dust");

        NextBlock = GameObject.Find("Next_Block");
        dog1 = GameObject.Find("dog1");
        trashCan1 = GameObject.Find("trash can 1");
        trashCan2 = GameObject.Find("trash can 2");
        trashCan3 = GameObject.Find("trash can 3");
    }

    void Update()
    {
        // �渷�̿� ��ȭ�ϸ� 
        if(gameManager.isGet)
        {
            isQuest = true;
        }
        // �������� ������ ����Ʈ Ȱ��ȭ
        if (isQuest&&!isComplete)
        {
            objName = gameManager.scanObject.name;
            // ��ȭ�Ѱ� ����������̸� ���� �������� ������ Ȱ��ȭ.
            if (objName == "trash can 1" || objName == "trash can 2" || objName == "trash can 3")
            {
                gameManager.isMeet = true;
                gameManager.isAction = true;
                if (Input.GetKeyDown(KeyCode.Y))   // �������� �����ϱ�.
                {
                    switch (objName)
                    {
                        case "trash can 1":
                            trashCan1.GetComponent<ObjData>().id = 301;
                            break;
                        case "trash can 2":
                            trashCan2.GetComponent<ObjData>().id = 311;
                            isDogGum = true;
                            // ���� ã�� �Ϸ���Ʈ1 ������
                            Illust1Fade();
                            break;
                        case "trash can 3":
                            trashCan3.GetComponent<ObjData>().id = 321;
                            break;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.N))   // �������� ���� ���.
                {
                    return;
                }
                //Debug.Log("���� if�� �� �� ����");
                //gameManager.isMeet = false;
                gameManager.isAction = false;
            }
            else
            {
                gameManager.isMeet = false;
            }

        }

        if(isDogGum)
        {
            gameManager.isMeet = true;
        }

        if (isComplete)
        {
            gameManager.isMeet = true;
            dog1.transform.position = new Vector3(11.6f, 2.22f, 0);
            dog1.GetComponent<ObjData>().id = 1001;
        }

        if(isBlackTalk)
        {
            NextBlock.SetActive(false);
        }
    }

    private void Illust1Fade()
    {
        StartCoroutine(FadeFlow(1));
    }
    public void Illust2Fade()
    {
        StartCoroutine(FadeFlow(2));
    }

    IEnumerator FadeFlow(int num)
    {
        time = 0f;
        if(num==1)
        {
            illust1.gameObject.SetActive(true);
            Color alpha = illust1.color;
            while (alpha.a<1f)
            {
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(0,1,time); // �Ϸ���Ʈ�� ���İ��� 0�̾��� ���� 1�� ���ݾ� �÷��༭ ������ ��Ÿ���� ��.
                illust1.color = alpha;
                yield return null;
            }
            time = 0f;

            yield return new WaitForSeconds(3f);

            while (alpha.a > 0f)
            {
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(1, 0, time); // �Ϸ���Ʈ�� ���İ��� 1�̾��� ���� 0�� ���ݾ� �����༭ ������ ������� ��.
                illust1.color = alpha;
                yield return null;
            }
            illust1.gameObject.SetActive(false);
            yield return null;
        }
        else
        {
            illust2.gameObject.SetActive(true);
            Color alpha = illust2.color;
            while (alpha.a < 1f)
            {
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(0, 1, time); // �Ϸ���Ʈ�� ���İ��� 0�̾��� ���� 1�� ���ݾ� �÷��༭ ������ ��Ÿ���� ��.
                illust2.color = alpha;
                yield return null;
            }
            time = 0f;

            yield return new WaitForSeconds(3f);

            while (alpha.a > 0f)
            {
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(1, 0, time); // �Ϸ���Ʈ�� ���İ��� 1�̾��� ���� 0�� ���ݾ� �����༭ ������ ������� ��.
                illust2.color = alpha;
                yield return null;
            }
            illust2.gameObject.SetActive(false);
            yield return null;
        }
    }
}
