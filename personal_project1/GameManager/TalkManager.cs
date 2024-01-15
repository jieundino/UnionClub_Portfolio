using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData_before;  //npc의 요구들어주기 성공 전 대화 내용
    Dictionary<int, string[]> talkData_after;   // 성공 후 대화 내용


    void Awake()
    {
        talkData_before = new Dictionary<int, string[]>();  //초기화
        talkData_after = new Dictionary<int, string[]>();   //초기화
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        // 처음 들어온 맵의 숲 입구의 땅 블록을 밟았을 때 나오게 할 플레이어의 대사
        // 플레이어의 콜라이더와 땅 블록에 넣어둔 콜라이더와 부딪히면 플레이어의 대사가 나오게 하기
        talkData_before.Add(10, new string[] { "엥.. 옷장 속에 이런 곳이?", "여긴 어디지? 저기에 동물이 있는 것 같은데, 다가가서 물어보자."
            , "동물들에게 가까이 다가가서 스페이스 바로 말을 걸 수 있어."});

        // npc1인 숲의 수호자(토끼)의 대사
        talkData_before.Add(1000, new string[] {"안녕?", "너, 여기에서 사는 동물이 아니지?"
            , "나는 이 숲의 수호자야. 만약에 여기에 있는 다른 동물들을 도와주면 집으로 무사히 보내줄게!"
            ,"그럼 부탁할게! 오른쪽에 있는 문을 Shift키로 열고 들어가면 돼."
            , "아 맞다, 이 숲에는 몬스터도 살고 있으니까 부딪히지 않게 조심해~"});

        // npc2인 아기 토끼의 대사
        talkData_before.Add(2000, new string[] {"아, 드디어 왔구나! 기다리고 있었어.","내가 소중히 모아뒀던 클로버들을 몬스터들이 가져가버렸어!"
            ,"혹시 숲을 돌아다니면서 클로버들을 찾는 것을 도와줄 수 있을까?","여기 바구니를 줄테니까 G키를 눌러서 담아오면 돼."
            , "클로버 5개를 찾아와줘. 그럼 부탁할게!"});

        // npc3인 개구리의 대사
        talkData_before.Add(3000, new string[] { "안녕, 네가 바로 도와준다는 애구나.", "저기 있는 멋진 집이 보이지?"
            , "그런데 주변에 있는 못된 몬스터들이 내 집을 부숴버리려고 해.","제한 시간 동안 우리집을 저 나쁜 몬스터들로부터 지켜줘!"
            ,"아, 물론 맨몸으로 지키라고 하는 건 아니야.", "여기 이 도토리들을 줄테니까 Ctrl키를 눌러서 이걸로 몬스터들을 공격하면 돼."
            , "준비됐으면 내 집에 가까이 다가가서 Shift를 눌러줘.", "바로 시작될 거니까 마음 단단히 먹어!"});

        // npc4인 꿀벌의 대사
        talkData_before.Add(4000, new string[] {"안녕? 네가 오기를 기다리고 있었어.","지금 내가 날개를 제대로 필 수 있는 상태가 아니야."
            ,"그래서 말인데, 저기~ 조금 높은 곳에 피어있는 꽃을 대신 따다 줄 수 있을까?","그렇게 걱정하지 않아도 돼! 꽃 하나만 따오면 되니까.","꽃을 따는 키도 G키를 누르면 돼."
            ,"그리고 여기엔 몬스터들이 하나도 없으니까 안심하고 갔다와도 돼~","그럼 부탁할게."});


        // 여우의 방에 있는 물건들과 상호작용하면 뜨는 대사
        talkData_before.Add(100, new string[] { "푹신한 침대이다.", "지금은 자면 안 돼!" });
        talkData_before.Add(110, new string[] { "책상이다.", "책상 위에는 어제 풀다 만 숙제가 있다.", "공부하기 싫다..." });
        talkData_before.Add(120, new string[] { "창밖을 보니 오늘은 날씨가 좋다." });
        talkData_before.Add(130, new string[] { "쓰레기통이다.", "놀고 와서 비울 것이다." });
        talkData_before.Add(140, new string[] { "옷장을 확인하기 전까지는 나가고 싶지 않다." });
        talkData_before.Add(150, new string[] { "어젯밤에 닫아뒀던 옷장 문이 살짝 열려있다.", "옷장 문을 열어볼까?","Shift키를 눌러서 열어보자." });


        // 여기부터는 미션 조건 달성 후 나오는 대사.
        talkData_after.Add(1000, new string[] {"고마워! 너 덕분에 다들 일이 무사히 풀렸어."
            ,"이제 집으로 보내줄게. 왼쪽의 문을 열고 나가면 돼.","어쩌면 여기에서 나가면 우리들을 잊을 수 있겠네.."
            , "..우리들을 도와줘서 정말 고마워.","그럼 잘가! 잘 지내~"});

        talkData_after.Add(2000, new string[] {"클로버 5개를 다 모아왔구나! 정말 고마워!","그럼 다음 장소로 갈 수 있는 문을 열어줄게. 잘가!"});

        talkData_after.Add(3000, new string[] {"내 소중한 집을 지켜줘서 고마워!","다음 장소로 갈 수 있는 문은 열어놨어. 그럼 안녕!"});

        talkData_after.Add(4000, new string[] {"꽃을 무사히 따왔구나? 정말 고마워!","이제 수호자님께 갈 수 있는 문을 열어줄게."
            ,"집으로 무사히 돌아가길 바랄게!"});


        talkData_after.Add(100, new string[] { "푹신한 침대이다.", "지금은 자면 안 돼!" });
        talkData_after.Add(110, new string[] { "책상이다.", "책상 위에는 어제 풀다 만 숙제가 있다.", "공부하기 싫다..." });
        talkData_after.Add(120, new string[] { "창밖을 보니 오늘은 날씨가 좋다.", "창문 선반 위에 처음 보는 네잎 클로버가 있다." });
        talkData_after.Add(130, new string[] { "쓰레기통이다.", "놀고 와서 비울 것이다." });
        talkData_after.Add(140, new string[] { "이제 친구를 만나러 가자!","Shift키를 눌러서 방문을 열고 나가자~!" });
        talkData_after.Add(150, new string[] { "옷장 문은 잘 닫혀 있다." });
    }

    public string GetTalk1(int id, int talkIndex)   //오브젝트의 아이디를 가져와서 이거랑 토크 인덱스로 알맞는 대사로 바꿔줌.
    { //미션 성공 전의 대사
        if (talkIndex == talkData_before[id].Length)
            return null;
        else
            return talkData_before[id][talkIndex];
    }
    public string GetTalk2(int id, int talkIndex)
    { //성공 후의 대사
        if (talkIndex == talkData_after[id].Length)
            return null;
        else
            return talkData_after[id][talkIndex];
    }
}

