using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;         // 단순한 오브젝트나 npc의 스크립트

    //특정 npc나 오브젝트는 조건을 충족하면 다른 대사를 말하기 때문에 따로 정의함.
    Dictionary<int, string[]> talkData_before;  
    Dictionary<int, string[]> talkData_after;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();  // 초기화
        talkData_before = new Dictionary<int, string[]>(); // 초기화
        talkData_after = new Dictionary<int, string[]>();  // 초기화 
        GenerateData();
    }

    void GenerateData()
    {
        // 스테이지1의 아이템들 조사했을 때의 대화.
        // 가질 수 있을 때
        talkData_before.Add(110, new string[] {"이것은 버스 카드인 것 같다.","이 아이템을 가져가볼까?"
            ,"G키를 눌러서 가져가자.\n가져가기 싫다면 N키를 누르자."});      //버스카드
        talkData_before.Add(120, new string[] {"이것은 하얀..콩나물인 것 같다.","이 아이템을 가져가볼까?"
            ,"G키를 눌러서 가져가자.\n가져가기 싫다면 N키를 누르자."});      //에어팟
        talkData_before.Add(130, new string[] {"이것은 반짝 빛나는 동전인 것 같다.","이 아이템을 가져가볼까?"
            ,"G키를 눌러서 가져가자.\n가져가기 싫다면 N키를 누르자."});      //동전
        // 가질 수 없을 때
        talkData_after.Add(110, new string[] {"이것은 버스 카드인 것 같다.","이 아이템을 가져가볼까?"
            ,"하지만 더 이상은 가질 수 없다."});      //버스카드
        talkData_after.Add(120, new string[] {"이것은 하얀..콩나물인 것 같다.","이 아이템을 가져가볼까?"
            ,"하지만 더 이상은 가질 수 없다."});      //에어팟
        talkData_after.Add(130, new string[] {"이것은 반짝 빛나는 동전인 것 같다.","이 아이템을 가져가볼까?"
            ,"하지만 더 이상은 가질 수 없다."});      //동전

        // 인벤토리에 아무것도 없을 때 출력함
        talkData_before.Add(140, new string[] {"여기에 무언가를 대면 나갈 수 있을 것 같다."});      //카드 단말기
        // 인벤토리에 아이템 있을 때 출력함
        talkData_after.Add(140, new string[] { "여기에 무언가를 대면 나갈 수 있을 것 같다."
            , "지금 가지고 있는 걸 사용해볼까?","Y키를 누르면 사용할 수 있고 N키를 눌러 취소할 수 있다."});

        // 스테이지3
        // 퀘스트 받기 전에 쓰레기통들을 조사할 경우.
        talkData_before.Add(300, new string[] {"평범한 쓰레기통이다.\n이상한 냄새가 나는 것 같다."});      //쓰레기통1
        talkData_before.Add(310, new string[] { "평범한 쓰레기통이다.\n이상한 냄새가 나는 것 같다." });      //쓰레기통2
        talkData_before.Add(320, new string[] { "평범한 쓰레기통이다.\n이상한 냄새가 나는 것 같다." });      //쓰레기통3
        // 퀘스트 받은 후 쓰레기통들을 조사할 경우.
        talkData_after.Add(300, new string[] { "평범한 쓰레기통이다.\n이상한 냄새가 나는 것 같다."
            ,"이 쓰레기통을 뒤져볼까?","Y키를 누르면 뒤져볼 수 있고 N키를 눌러 취소할 수 있다."});           //쓰레기통1
        talkData_after.Add(301, new string[] { "... 쓰레기만 나왔다."});   //Y키 누른(조사) 후 대사
        talkData_after.Add(310, new string[] { "평범한 쓰레기통이다.\n이상한 냄새가 나는 것 같다."
            ,"이 쓰레기통을 뒤져볼까?","Y키를 누르면 뒤져볼 수 있고 N키를 눌러 취소할 수 있다."});      //쓰레기통2
        talkData_after.Add(311, new string[] { "...!", "맛있어 보이는 개껌이 나왔다!","이 개껌을 가지고 길막이한테 바로 가보자." });   //Y키 누른(조사) 후 대사
        talkData_after.Add(320, new string[] { "평범한 쓰레기통이다.\n이상한 냄새가 나는 것 같다."
            ,"이 쓰레기통을 뒤져볼까?","Y키를 누르면 뒤져볼 수 있고 N키를 눌러 취소할 수 있다."});      //쓰레기통3
        talkData_after.Add(321, new string[] { "... 쓰레기만 나왔다." });   //Y키 누른(조사) 후 대사

        // 일반 오브젝트들 스크립트
        talkData.Add(33, new string[] {"쇠 냄새가 나는 철 울타리이다.", "이쪽은 울타리로 막혀서 지나갈 수 없다."});//철 울타리
        talkData.Add(34, new string[] {"높은 가로등이다.", "아직 불이 켜지지 않았다."}); //가로등
        talkData.Add(35, new string[] {"나뭇가지가 멋있어 보이는 나무이다."}); //나무
        talkData.Add(36, new string[] {"익숙한 건물이다.","문이 잠겨 있어서 들어갈 수 없다."}); //건물들

        //NPC들 스크립트
        talkData_before.Add(1000, new string[] { "길막이 : 이쪽으론 지나갈 수 없어 꼬마!", "길막이 : 뭐? 지나가고 싶다고?", "길막이 : 흠..난 배고파. 뭐가 필요한지 알지?"
            ,"길막이 : 먹을 만한 것 좀 줘.\n여기 주변에 있는 쓰레기통들을 잘 뒤져보면 뭔가 나올지도~" });      //길막이
        talkData_before.Add(2000, new string[] { "흑임자 : 어? 너 왜 아직도 여기있어!", "흑임자 : 밤이 되기 전에 얼른 집으로 가렴!" });      //흑임자
        talkData_before.Add(3000, new string[] { "찹쌀이 : ...외로워.." });      //찹쌀이
        talkData_before.Add(4000, new string[] { "삼색이 : ...왱."});      //삼색이

        talkData_after.Add(1000, new string[] { "길막이 : 흠...", "길막이 : 꽤 괜찮은 개껌이로군.", "길막이 : 자, 이제 가봐라.", "길막이 : 아, 그리고 너한테 줄게 있다. \n받아라." });      //길막이
        talkData_after.Add(1001, new string[] { "길막이 : 가야 할 곳을 모르겠다고?", "길막이 : 흠...", "길막이 : 나는 모르겠다. \n저기 시커먼 녀석한테 한번 물어봐라."
            , "길막이 : 저 녀석이라면 네가 원하는 답을 알려줄지도 모르지." });      //길막이
        talkData_after.Add(2000, new string[] { "흑임자 : 집 가는 길 까먹었어?\n계속 가다보면 길이 있을 거야."
            , "흑임자 : 시간이 다 되기 전에 얼른 가봐!" });      //흑임자
        talkData_after.Add(3000, new string[] { "찹쌀이 : ...외로워..", "찹쌀이 : 넌 부럽다.." });      //찹쌀이
        talkData_after.Add(4000, new string[] { "삼색이 : ...왜앵." });      //삼색이
        //Next Block 스크립트
        //talkData_after.Add(370, new string[] { "이제... 집으로 가자." }); //다음 스테이지로 가는 문(이제 지나갈 수 있음)
        talkData_after.Add(370, new string[] { "가야 할 곳이... 기억이 안 난다.", "주변 개들에게 물어보자." }); //다음 스테이지로 가는 문(아직 못 지나감)

        // 스테이지5
        // 거실
        talkData.Add(50, new string[] { "때가 많이 탄 장난감이다.", "가져가고 싶지만... 두고 가야겠지."}); //장난감
        talkData.Add(51, new string[] { "어둡다.","이것이 내 기억의 처음이었다."}); //액자1
        talkData.Add(52, new string[] { "어릴 때의 내가 있다.","이때 지금의 너를 만났지."}); //액자2
        talkData.Add(53, new string[] { "너와 같이 찍은 사진이다.","너도 많이 행복해 보인다."}); //액자3
        talkData.Add(54, new string[] { "내가 공을 멋지게 물고 있는 사진이다.","즐거워 보인다."}); //액자4
        talkData.Add(55, new string[] { "내가 너를 떠난 날의 사진이다.","네가 너무 슬퍼하지 않았으면 좋겠다."}); //액자5
        talkData.Add(56, new string[] { "활짝 웃는 내 사진과 유골함이다.","......"}); //액자6+유골함
        // 방 안
        talkData.Add(60, new string[] { "내 전용 자리이다.","여기에 서서 너를 구경하는 것도 좋아했지."}); //방석
        talkData.Add(61, new string[] { "내 침대다.","자주 너의 침대 위에서 같이 자곤 했지만,\n넌 내 침대를 사주는 것을 좋아했다."}); //침대
    }

    //단순 오브젝트들
    public string GetTalk(int id, int talkIndex)    //오브젝트의 아이디를 가져와서 이거랑 토크 인덱스로 알맞는 대사로 바꿔줌.
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
    // 조건 충족 전 
    public string GetTalk1(int id, int talkIndex)
    { //충족 전의 대사
        if (talkIndex == talkData_before[id].Length)
            return null;
        else
            return talkData_before[id][talkIndex];
    }
    public string GetTalk2(int id, int talkIndex)
    { //충족 후의 대사
        if (talkIndex == talkData_after[id].Length)
            return null;
        else
            return talkData_after[id][talkIndex];
    }
}
