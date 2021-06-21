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
    public int[] bosspos = new int [2];

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
        bosspos[0] = 4;
        bosspos[1] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        bossvec = transform.position;
        //Attack();
        /* 테스트용 이동 키
        if(Input.GetKeyDown(KeyCode.J))
        {
            MoveLeft();
        }
        else if(Input.GetKeyDown(KeyCode.K))
        {
            MoveDown();
        }
        else if(Input.GetKeyDown(KeyCode.L))
        {
            MoveRight();
        }
        else if(Input.GetKeyDown(KeyCode.I))
        {
            MoveUp();
        }
        else if(Input.GetKeyDown(KeyCode.H))
        {
            int[,] a = new int [3, 2] {{0, 0}, {-1, 0}, {1, 0}};
            Attack(1, a);
        }
        */

    }


    private void MoveLeft()
    {

         // 이동 오디오 클립 재생
        bossAudio.clip = MoveClip;
        bossAudio.Play();

        //왼쪽으로 한 칸 움직이는 함수
        //위치가 맨 왼쪽이 아닐 경우에 이동
        if(bosspos[0] > 0)
        {
            // x좌표 변경
            vector.x = -3.2f; 
            vector.y = 0.0f;
            transform.Translate(vector);
            bosspos[0]--; // 발판 위치값 수정


            Debug.Log("Move left");
        }
        
    }

    private void MoveRight()
    {
        //오른쪽으로 한 칸 움직이는 함수

        // 이동 오디오 클립 재생
        bossAudio.clip = MoveClip;
        bossAudio.Play();

        //위치가 맨 오른쪽이 아닐 경우에 이동
        if(bosspos[0] < 4)
        {
            // 콩딩의 x좌표 변경
            vector.x = 3.2f; 
            vector.y = 0.0f;
            transform.Translate(vector);
            bosspos[0]++; // 콩딩의 발판 위치값 수정

            Debug.Log("Move right");
        }
        
    }

    private void MoveUp()
    {
        //위쪽으로 한 칸 움직이는 함수
        // 이동 오디오 클립 재생
        bossAudio.clip = MoveClip;
        bossAudio.Play();

        //위치가 맨 위쪽이 아닐 경우에 이동
        if(bosspos[1] > 0)
        {
            // 콩딩의 y좌표 변경
            vector.x = 0.0f;
            vector.y = 2.0f; 
            transform.Translate(vector);

            bosspos[1]--; // 발판 위치값 수정

            Debug.Log("Move Up");
        }
        
    }

    private void MoveDown()
    {
        //아래쪽으로 한 칸 움직이는 함수

         // 이동 오디오 클립 재생
        bossAudio.clip = MoveClip;
        bossAudio.Play();

        //위치가 맨 아래쪽이 아닐 경우에 이동
        if(bosspos[1] < 2)
        {
            // 콩딩의 y좌표 변경
            vector.x = 0.0f;
            vector.y = -2.0f; 
            transform.Translate(vector);

            bosspos[1]++; // 발판 위치값 수정

            Debug.Log("Move down");
        }
        
    }


    private void Attack(int damage, int[,] range)
    {
        // 공격범위는 공격자의 위치를 (0, 0)이라고 할 때 공격 범위에 해당하는 좌표들의 배열
        // 예를 들어, 우측으로 3칸짜리 공격을 구현하고 싶다면 {{1, 0}, {2, 0}, {3, 0}}를 input
        int[,] field = new int [5, 3]; // 좌표계
       
        int rangesize = range.GetLength(0);

        int cong_x = Congding.GetComponent<BattleCongdingController>().congdingpos[0];
        int cong_y = Congding.GetComponent<BattleCongdingController>().congdingpos[1];

        // 공격 애니메이션 재생
        animator.SetTrigger("Attack");

        // 공격 오디오 클립 재생
        bossAudio.clip = AttackClip;
        bossAudio.Play();

        if(true)
        {

            //적을 공격하는 함수

            // 공격범위의 field 좌표의 값을 1로 설정
            // range값을 콩딩의 좌표에 따라 field의 좌표로 이동하여 맞춤
            for(int p = 0; p < rangesize; p++)
            {
                if(bosspos[0] + range[p,0] >= 0 && bosspos[0] + range[p,0] <= 4 && bosspos[1] + range[p,1] >= 0 && bosspos[1] + range[p,1] <= 2)
                {
                    field[bosspos[0] + range[p,0], bosspos[1] + range[p,1]] = 1;
                }
                
            }

            //공격대상이 공격 범위 안에 있을 경우 공격
            for(int i = 0; i < 5; i++)
            {
                for(int t = 0; t < 3; t++)
                {
                    if(field[i,t] == 1 && cong_x == i && cong_y == t )
                    {
                        Instantiate(AttackEffect, Congding.GetComponent<BattleCongdingController>().position, Quaternion.Euler(Vector2.zero));
                        Congding.GetComponent<BattleCongdingController>().Attacked(damage);
                        Debug.Log("Attack" + damage + " damage!");
                    }
                }
            }

            
        }
           
    }

    public void RandomActivate()
    {
        int random = UnityEngine.Random.Range(0,6);

        if(random == 0)
        {
            MoveLeft();
            Debug.Log("Boss : MOVE-LEFT");
        }
        else if(random == 1)
        {
            MoveRight();
            Debug.Log("Boss : MOVE-RIGHT");
        }
        else if(random == 2)
        {
            MoveUp();
            Debug.Log("Boss : MOVE-UP");
        }
        else if(random == 3)
        {
            MoveDown();
            Debug.Log("Boss : MOVE-DOWN");
        }
        else if(random == 4 || random == 6)
        {
            int[,] range = new int [3, 2] {{-1, 0}, {0, 0}, {1, 0}};
            Attack(1, range);
            Debug.Log("Boss : BASIC ATTACK");
        }
        else if(random == 5)
        {
            MoveLeft();
            int[,] range = new int[2, 2] {{0, 0}, {-1, 0}};
            Attack(2, range);
            Debug.Log("Boss : RUSH ATTACK");
        }
    }

}
