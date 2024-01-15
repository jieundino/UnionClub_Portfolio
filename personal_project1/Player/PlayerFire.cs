using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    PlayerController3 Player;   // 플레이어컨트롤러3에 있는 isAttack으로 총알 쏘는 거 허가하기 위해서

    // 총알을 생산할 공장 오른쪽
    public GameObject bulletFactory_R;
    // 총구   오른쪽
    public GameObject firePostion_R;

    // 총알을 생산할 공장   왼쪽
    public GameObject bulletFactory_L;
    // 총구   왼쪽
    public GameObject firePostion_L;

    public Animator animator;  //애니메이터 조작을 위한 변수 

    // 효과음
    public AudioClip audioAttack;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Player = FindObjectOfType<PlayerController3>();
    }

    bool isRight=true;  //오른쪽으로 향하면 트루
    // Update is called once per frame
    void Update()
    {
        // isAttack이 true면 총알 발사 가능
        if(Player.isAttack)
        {
            // 플레이어가 컨트롤 키를 누르면 총알 발사하기
            // 플레이어가 컨트롤 키를 누름
            if (Input.GetKeyDown(KeyCode.LeftControl)|| Input.GetKeyDown(KeyCode.RightControl))
            {
                //공격하는 플레이어 애니메이션 재생
                animator.SetTrigger("doAttack");

                PlaySoundEffect("ATTACK");
                //총알 공장에서 총알 만들고
                //총알 발사(총알을 총구 위치로 가져다 놓기)
                if (isRight) //오른쪽이다
                {
                    //오른쪽으로 가는 총알 발사
                    GameObject bullet = Instantiate(bulletFactory_R, firePostion_R.transform.position, firePostion_R.transform.rotation);
                    
                }
                else //왼쪽이다
                {
                    // 왼쪽으로 가는 총알 발사
                    GameObject bullet = Instantiate(bulletFactory_L, firePostion_L.transform.position, firePostion_L.transform.rotation);

                }
            }
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))    //방향키 오른쪽 누르면 총구 오른쪽 활성화, 그리고 오른쪽만 총 발사 가능
        {
            firePostion_R.SetActive(true);
            firePostion_L.SetActive(false);
            isRight = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))   //방향키 왼쪽 누르면 총구 왼쪽 활성화, 그리고 왼쪽만 총 발사 가능
        {
            firePostion_R.SetActive(false);
            firePostion_L.SetActive(true);
            isRight = false;
        }
    }

    // 효과음 재생 메소드
    void PlaySoundEffect(string action)
    {
        switch (action)
        {
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
        }
        audioSource.Play();
    }
}
