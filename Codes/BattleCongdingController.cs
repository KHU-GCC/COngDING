//
//  BattleCongdingController.cs
//  Congding
//
//  Created by Jeong So Yun on 2021/06/18.
//  Copyright (c) 2021 Will KHU-GCC. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCongdingController : MonoBehaviour
{
    public AudioClip AttackClip; // 콩딩이가 펀치할 때 나오는 오디오 재생 클립
    public AudioClip MoveClip; // 콩딩이가 움직일 때 나오는 오디오 재생 클립
    public AudioClip HurtClip; // 콩딩이가 쳐맞을 때 나오는 오디오 재생 클립
    public GameObject AttackEffect; // 컹딩이가 타격에 성공했을 때 나오는 이펙트
    GameObject Boss; // 보스 오브젝트

    public int congdingposition; // 콩딩이 위치한 발판 (1~15)
    public Vector2 position; // 콩딩 위치 좌표
    private Vector2 vector; // 콩딩 이동에 필요한 벡터
    public bool isAttacked; // 맞았니?

    private Animator animator; // 애니메이터
    private AudioSource congdingAudio; // 오디오 소스


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        congdingAudio = GetComponent<AudioSource>();
        vector = new Vector2(0, 0);
        Boss = GameObject.Find("Boss");

        congdingposition = 6; //왼쪽 중앙에서 시작

    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        MoveRight();
        MoveLeft();
        MoveDown();
        MoveUp();
        Attack();
        Attacked();
        
    }

    private void MoveLeft()
    {
        //왼쪽으로 한 칸 움직이는 함수
        //콩딩의 위치가 맨 왼쪽이 아닐 경우에 이동
        if(congdingposition % 5 != 1 && Input.GetKeyDown(KeyCode.A))
        {
            // 콩딩의 x좌표 변경
            vector.x = -3.2f; 
            vector.y = 0.0f;
            transform.Translate(vector);
            congdingposition--; // 콩딩의 발판 위치값 수정

            // 이동 오디오 클립 재생
            congdingAudio.clip = MoveClip;
            congdingAudio.Play();

            Debug.Log("Move left");
        }
        
    }

    private void MoveRight()
    {
        //오른쪽으로 한 칸 움직이는 함수
        //콩딩의 위치가 맨 오른쪽이 아닐 경우에 이동
        if(congdingposition % 5 != 0 && Input.GetKeyDown(KeyCode.D))
        {
            // 콩딩의 x좌표 변경
            vector.x = 3.2f; 
            vector.y = 0.0f;
            transform.Translate(vector);
            congdingposition++; // 콩딩의 발판 위치값 수정

            // 이동 오디오 클립 재생
            congdingAudio.clip = MoveClip;
            congdingAudio.Play();

            Debug.Log("Move right");
        }
        
    }

    private void MoveUp()
    {
        //위쪽으로 한 칸 움직이는 함수
        //콩딩의 위치가 맨 위쪽이 아닐 경우에 이동
        if(congdingposition > 5 && Input.GetKeyDown(KeyCode.W))
        {
            // 콩딩의 y좌표 변경
            vector.x = 0.0f;
            vector.y = 2.0f; 
            transform.Translate(vector);

            congdingposition -= 5; // 콩딩의 발판 위치값 수정

            // 이동 오디오 클립 재생
            congdingAudio.clip = MoveClip;
            congdingAudio.Play();

            Debug.Log("Move Up");
        }
        
    }

    private void MoveDown()
    {
        //아래쪽으로 한 칸 움직이는 함수
        //콩딩의 위치가 맨 아래쪽이 아닐 경우에 이동
        if(congdingposition < 11 && Input.GetKeyDown(KeyCode.S))
        {
            // 콩딩의 y좌표 변경
            vector.x = 0.0f;
            vector.y = -2.0f; 
            transform.Translate(vector);

            congdingposition += 5; // 콩딩의 발판 위치값 수정

            // 이동 오디오 클립 재생
            congdingAudio.clip = MoveClip;
            congdingAudio.Play();

            Debug.Log("Move down");
        }
        
    }

    private void Attack()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {

            //적을 공격하는 함수
            
            // 공격 애니메이션 재생
            animator.SetTrigger("Attack");

            // 공격 오디오 클립 재생
            congdingAudio.clip = AttackClip;
            congdingAudio.Play();

            // 주위에 적이 있을 경우 공격
            if(congdingposition % 5 != 1 && congdingposition % 5 != 0) // 콩딩이 사이드 칸에 있지 않은 경우
            {
                if(Boss.GetComponent<BossController>().bossposition >= congdingposition -1 && Boss.GetComponent<BossController>().bossposition <= congdingposition + 1)
                {
                    // 타격 이펙트 생성
                    Instantiate(AttackEffect, Boss.GetComponent<BossController>().bossvec, Quaternion.Euler(Vector2.zero));
                    BattleManager.instance.boss_life--;
                    Debug.Log("Attack!");
                }
            }
            else if(congdingposition % 5 == 1) // 제일 왼쪽 칸에 있는 경우
            {
                if(Boss.GetComponent<BossController>().bossposition == congdingposition - 1 || Boss.GetComponent<BossController>().bossposition == congdingposition)
                {
                    // 타격 이펙트 생성
                    Instantiate(AttackEffect, Boss.GetComponent<BossController>().bossvec, Quaternion.Euler(Vector2.zero));
                    BattleManager.instance.boss_life--;
                    Debug.Log("Attack!");
                    
                }
            }
            else // 제일 오른쪽 칸에 있는 경우
            {
                if(Boss.GetComponent<BossController>().bossposition == congdingposition + 1 || Boss.GetComponent<BossController>().bossposition == congdingposition)
                {
                    // 타격 이펙트 생성
                    Instantiate(AttackEffect, Boss.GetComponent<BossController>().bossvec, Quaternion.Euler(Vector2.zero));
                    BattleManager.instance.boss_life--;
                    Debug.Log("Attack!");
                }
            }
        }

    }

    private void Attacked()
    {
        if(isAttacked){
            animator.SetTrigger("Attacked");
            BattleManager.instance.life--;
            isAttacked = false;
        }
    }
}
