using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// npc, 단순 오브젝트의 id를 지정할 수 있게 해줌.
// GamaMager에서는 objData의 id 정보를 매개변수로 넘김. Talk(objData.id)
// 그리고 이 Talk 함수에서 TalkManager를 호출하여 id로  TalkManager에서 id 번호와 맞는 talkData를 가져옴.
public class ObjData : MonoBehaviour
{
    public int id;
}
