//
//  BossController.cs
//  Congding
//
//  Created by Jeong SoYun on 2021/06/18.
//  Copyright (c) 2021 Will KHU-GCC. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{ 
    public AudioClip MoveClip; // 이동할 때 오디오 클립
    public AudioClip AttackClip; // 공격할 때 오디오 클립

    public int bossposition; // 보스가 위치한 발판 (1~15)
    public Vector2 bossvec; // 보스 좌표
    private Vector2 vector; // 이동에 필요한 벡터

    private Animator animator; // 보스 애니메이터
    private AudioSource bossAudio; // 보스 오디오 소스
    GameObject Congding;
    public GameObject AttackEffect;

    // Start is called before the first frame update
    void Start()
    {
        Congding = GameObject.Find("Congding");
        animator = GetComponent<Animator>();
        bossAudio = GetComponent<AudioSource>();
        bossvec = transform.position;
        vector = new Vector2(0, 0);
        bossposition = 10; // 보스 초기 위치 우측 중앙
    }

    // Update is called once per frame
    void Update()
    {
        bossvec = transform.position;
        Attack();
        MoveLeft();
        MoveRight();
        MoveUp();
        MoveDown();
    }


    private void MoveLeft()
    {
        //왼쪽으로 한 칸 움직이는 함수
        //위치가 맨 왼쪽이 아닐 경우에 이동
        if(bossposition % 5 != 1 && Input.GetKeyDown(KeyCode.J))
        {
            // x좌표 변경
            vector.x = -3.2f; 
            vector.y = 0.0f;
            transform.Translate(vector);
            bossposition--; // 발판 위치값 수정

            // 이동 오디오 클립 재생
            bossAudio.clip = MoveClip;
            bossAudio.Play();

            Debug.Log("Move left");
        }
        
    }

    private void MoveRight()
    {
        //오른쪽으로 한 칸 움직이는 함수
        //위치가 맨 오른쪽이 아닐 경우에 이동
        if(bossposition % 5 != 0 && Input.GetKeyDown(KeyCode.L))
        {
            // 콩딩의 x좌표 변경
            vector.x = 3.2f; 
            vector.y = 0.0f;
            transform.Translate(vector);
            bossposition++; // 콩딩의 발판 위치값 수정

            // 이동 오디오 클립 재생
            bossAudio.clip = MoveClip;
            bossAudio.Play();

            Debug.Log("Move right");
        }
        
    }

    private void MoveUp()
    {
        //위쪽으로 한 칸 움직이는 함수
        //위치가 맨 위쪽이 아닐 경우에 이동
        if(bossposition > 5 && Input.GetKeyDown(KeyCode.I))
        {
            // 콩딩의 y좌표 변경
            vector.x = 0.0f;
            vector.y = 2.0f; 
            transform.Translate(vector);

            bossposition -= 5; // 발판 위치값 수정

            // 이동 오디오 클립 재생
            bossAudio.clip = MoveClip;
            bossAudio.Play();

            Debug.Log("Move Up");
        }
        
    }

    private void MoveDown()
    {
        //아래쪽으로 한 칸 움직이는 함수
        //위치가 맨 아래쪽이 아닐 경우에 이동
        if(bossposition < 11 && Input.GetKeyDown(KeyCode.K))
        {
            // 콩딩의 y좌표 변경
            vector.x = 0.0f;
            vector.y = -2.0f; 
            transform.Translate(vector);

            bossposition += 5; // 발판 위치값 수정

            // 이동 오디오 클립 재생
            bossAudio.clip = MoveClip;
            bossAudio.Play();

            Debug.Log("Move down");
        }
        
    }


    private void Attack()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {

            //적을 공격하는 함수
            
            // 공격 애니메이션 재생
            animator.SetTrigger("Attack");

            // 공격 오디오 클립 재생
            bossAudio.clip = AttackClip;
            bossAudio.Play();

            // 주위에 적이 있을 경우 공격
            if(bossposition % 5 != 1 && bossposition % 5 != 0) // 보스가 사이드 칸에 있지 않은 경우
            {
                if(Congding.GetComponent<BattleCongdingController>().congdingposition >= bossposition -1 && Congding.GetComponent<BattleCongdingController>().congdingposition <= bossposition + 1)
                {
                    // 타격 이펙트 생성
                    Instantiate(AttackEffect, Congding.GetComponent<BattleCongdingController>().position, Quaternion.Euler(Vector2.zero));
                    Congding.GetComponent<BattleCongdingController>().isAttacked = true;
                    Debug.Log("Attacked!");
                }
            }
            else if(bossposition % 5 == 1) // 제일 왼쪽 칸에 있는 경우
            {
                if(Congding.GetComponent<BattleCongdingController>().congdingposition == bossposition + 1 || Congding.GetComponent<BattleCongdingController>().congdingposition == bossposition)
                {
                    // 타격 이펙트 생성
                    Instantiate(AttackEffect, Congding.GetComponent<BattleCongdingController>().position, Quaternion.Euler(Vector2.zero));
                    Congding.GetComponent<BattleCongdingController>().isAttacked = true;
                    Debug.Log("Attacked!");
                    
                }
            }
            else // 제일 오른쪽 칸에 있는 경우
            {
                if(Congding.GetComponent<BattleCongdingController>().congdingposition == bossposition - 1 || Congding.GetComponent<BattleCongdingController>().congdingposition == bossposition)
                {
                    // 타격 이펙트 생성
                    Instantiate(AttackEffect, Congding.GetComponent<BattleCongdingController>().position, Quaternion.Euler(Vector2.zero));
                    Congding.GetComponent<BattleCongdingController>().isAttacked = true;
                    Debug.Log("Attacked!");
                }
            }
        }
    }

}
