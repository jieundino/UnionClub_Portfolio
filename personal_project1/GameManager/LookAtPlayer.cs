using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        // 엔피시의 콜라이더 영역 안에 들어가 있고
        if(other.tag=="Player") //그 상대의 태그가 플레이어면
        {
            //엔피씨를 플레이어쪽으로 바라보게 함.
            int dir;    //엔피씨의 방향이 들어갈 변수
            Vector3 pos = other.transform.position;    //pos에 플레이어의 위치넣음
            if(pos.x>0.9f) //pos의 x값이 양수면
            {
                dir = 1;    //1로 넣음
            }
            else
            {
                dir = -1;   //음수면 -1
            }

            transform.localScale = new Vector3(dir, 1, 1);
            // x좌표가 1이면 오른쪽이고 -1이면 왼쪽

        }
    }
}
