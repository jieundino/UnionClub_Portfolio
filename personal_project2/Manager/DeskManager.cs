using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 스테이지5에서 책상씬으로 이동하고 작동되는 매니저
public class DeskManager : MonoBehaviour
{
    public Sprite pencilAfter;
    public Sprite noteAfter;

    public GameObject pencil;
    public GameObject note;

    public bool isPencil = false;
    public bool isNote = false;

    public GameObject writeBtn;

    public Image illust;
    public Image Black;
    float time = 0f;
    float F_time = 1f;

    // 효과음
    public AudioClip audioClick;
    AudioSource audioSource;

    public GameObject stage5_is;

    void Start()
    {
        // stage5_is 게임오브젝트 찾아서 가져옴.
        // 스테이지5로 돌아갔을 때 플레이어의 책상 미션 성공을 했다고 확인 용도.
        stage5_is = GameObject.Find("stage5_Is");
        pencil = GameObject.Find("Pencil");
        note = GameObject.Find("NotePad");
        audioSource = GetComponent<AudioSource>();
    }

    // 화면 클릭했을 때 레이를 쏴서 부딪힌 오브젝트 이름 가져와서 넣음
    public string hitName = null;   
    Vector3 MousePosition;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭하면 화면에 레이를 쏴서
            MousePosition = Input.mousePosition;
            MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, 30f);
            Debug.DrawRay(MousePosition, transform.forward * 10, Color.red, 0.3f);
            // 부딪힌게 있으면 해당 게임오브젝트의 이름을 가져와서 hitName에 넣음.
            if(hit)
            {
                hitName = hit.transform.gameObject.name;
            }
            // 클릭 사운드도 클릭할 때마다 재생.
            PlaySoundEffect("CLICK");
        }

        // 만일 hitName이 연필, 메모지면 isPencil와 isNote를 true로 만들고 이미지도 선택된 이미지로 바꿔줌.
        if (hitName == "Pencil")
        {
            pencil.GetComponent<SpriteRenderer>().sprite = pencilAfter;
            isPencil = true;
        }
        else if(hitName == "NotePad")
        {
            note.GetComponent<SpriteRenderer>().sprite = noteAfter;
            isNote = true;
        }

        // 아래의 두 변수가 true로 되면 stage5_is의 isDone 변수를 true로 바꿔주고 쓰기 버튼 활성화함. 
        if (isPencil&&isNote)
        {
            stage5_is.GetComponent<Stage5_isDone>().isDone = true;
            writeBtn.SetActive(true);
        }
    }

    public void FadeBtn()
    {
        Debug.Log("FadeFlow 호출됨.");
        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow()
    {
        time = 0f;
        illust.gameObject.SetActive(true);
        Color alpha = illust.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time); // 일러스트의 알파값을 0이었던 것을 1로 조금씩 올려줘서 서서히 나타나게 함.
            illust.color = alpha;
            yield return null;
        }
        time = 0f;

        yield return new WaitForSeconds(3f);

        Black.gameObject.SetActive(true);
        alpha = Black.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time); // 검은색 화면의 알파값을 0이었던 것을 1로 조금씩 올려줘서 서서히 나타나게 함.
            Black.color = alpha;
            yield return null;
        }
        //illust.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        // 화면 어두워지고 다음 씬인 스테이지5로 이동함.
        nextScene();
        yield return null;
    }

    void nextScene()
    {
        SceneManager.LoadScene("Stage5");
    }

    // 효과음 재생 메소드
    void PlaySoundEffect(string action)
    {
        switch (action)
        {
            case "CLICK":
                audioSource.clip = audioClick;
                break;
        }
        audioSource.Play();
    }
}
