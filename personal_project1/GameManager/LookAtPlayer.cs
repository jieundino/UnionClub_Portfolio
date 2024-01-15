using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        // ���ǽ��� �ݶ��̴� ���� �ȿ� �� �ְ�
        if(other.tag=="Player") //�� ����� �±װ� �÷��̾��
        {
            //���Ǿ��� �÷��̾������� �ٶ󺸰� ��.
            int dir;    //���Ǿ��� ������ �� ����
            Vector3 pos = other.transform.position;    //pos�� �÷��̾��� ��ġ����
            if(pos.x>0.9f) //pos�� x���� �����
            {
                dir = 1;    //1�� ����
            }
            else
            {
                dir = -1;   //������ -1
            }

            transform.localScale = new Vector3(dir, 1, 1);
            // x��ǥ�� 1�̸� �������̰� -1�̸� ����

        }
    }
}
