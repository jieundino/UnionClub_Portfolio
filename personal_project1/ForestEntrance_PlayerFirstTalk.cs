using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestEntrance_PlayerFirstTalk : MonoBehaviour
{
    public bool isStartLine = false;     //처음 플레이어의 대사 한번만 출력하기 위한 bool 변수.
                                  //변수가 false면 대사 출력하고 GameManager에서 이 대화 다 하면 변수를 true로 바꿔줌.
    public GameObject Object;       //게임 오브젝트 자신.
    
    //오직 숲의 입구에 플레이어가 처음 왔을 때 대화창에 플레이어가 첫 대화를 말하기 위한 스크립트임.

}
