using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ��������5���� å������� �̵��ϰ� �۵��Ǵ� �Ŵ���
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

    // ȿ����
    public AudioClip audioClick;
    AudioSource audioSource;

    public GameObject stage5_is;

    void Start()
    {
        // stage5_is ���ӿ�����Ʈ ã�Ƽ� ������.
        // ��������5�� ���ư��� �� �÷��̾��� å�� �̼� ������ �ߴٰ� Ȯ�� �뵵.
        stage5_is = GameObject.Find("stage5_Is");
        pencil = GameObject.Find("Pencil");
        note = GameObject.Find("NotePad");
        audioSource = GetComponent<AudioSource>();
    }

    // ȭ�� Ŭ������ �� ���̸� ���� �ε��� ������Ʈ �̸� �����ͼ� ����
    public string hitName = null;   
    Vector3 MousePosition;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 Ŭ���ϸ� ȭ�鿡 ���̸� ����
            MousePosition = Input.mousePosition;
            MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, 30f);
            Debug.DrawRay(MousePosition, transform.forward * 10, Color.red, 0.3f);
            // �ε����� ������ �ش� ���ӿ�����Ʈ�� �̸��� �����ͼ� hitName�� ����.
            if(hit)
            {
                hitName = hit.transform.gameObject.name;
            }
            // Ŭ�� ���嵵 Ŭ���� ������ ���.
            PlaySoundEffect("CLICK");
        }

        // ���� hitName�� ����, �޸����� isPencil�� isNote�� true�� ����� �̹����� ���õ� �̹����� �ٲ���.
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

        // �Ʒ��� �� ������ true�� �Ǹ� stage5_is�� isDone ������ true�� �ٲ��ְ� ���� ��ư Ȱ��ȭ��. 
        if (isPencil&&isNote)
        {
            stage5_is.GetComponent<Stage5_isDone>().isDone = true;
            writeBtn.SetActive(true);
        }
    }

    public void FadeBtn()
    {
        Debug.Log("FadeFlow ȣ���.");
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
            alpha.a = Mathf.Lerp(0, 1, time); // �Ϸ���Ʈ�� ���İ��� 0�̾��� ���� 1�� ���ݾ� �÷��༭ ������ ��Ÿ���� ��.
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
            alpha.a = Mathf.Lerp(0, 1, time); // ������ ȭ���� ���İ��� 0�̾��� ���� 1�� ���ݾ� �÷��༭ ������ ��Ÿ���� ��.
            Black.color = alpha;
            yield return null;
        }
        //illust.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        // ȭ�� ��ο����� ���� ���� ��������5�� �̵���.
        nextScene();
        yield return null;
    }

    void nextScene()
    {
        SceneManager.LoadScene("Stage5");
    }

    // ȿ���� ��� �޼ҵ�
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
