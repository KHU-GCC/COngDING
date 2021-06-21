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
    private int[,] CoordinateSystem = new int [5, 3]; // 좌표계 왼쪽 위가 (0, 0)
    public int[] congdingpos = new int [2]; // 콩딩이의 좌표
    public Vector2 position; // 콩딩 위치 좌표
    private Vector2 vector; // 콩딩 이동에 필요한 벡터
    public bool isAttacked; // 맞았니?
    public int turn = 1; // 현재 턴 수
    [SerializeField] float delaytime = 3f;

    GameObject FirstCard; // 선택한 첫 번째 카드
    GameObject SecondCard; // 두번째
    GameObject ThirdCard; // 세번째

    private Animator animator; // 애니메이터
    private AudioSource congdingAudio; // 오디오 소스


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        congdingAudio = GetComponent<AudioSource>();
        vector = new Vector2(0, 0);
        Boss = GameObject.Find("Boss");
        congdingpos[0] = 0;
        congdingpos[1] = 1;
         // 왼쪽 중앙에서 시작

        congdingposition = 6; //왼쪽 중앙에서 시작

        // Battle Manager로부터 선택한 카드 오브젝트 3장을 가져온다.
        FirstCard = BattleManager.instance.gocard1;
        SecondCard = BattleManager.instance.gocard2;
        ThirdCard = BattleManager.instance.gocard3;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        
        /* 테스트용 이동 키
        if(Input.GetKeyDown(KeyCode.W))
        {
            MoveUp();
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            MoveDown();
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            int[,] a = new int [3, 2] {{0, 0}, {-1, 0}, {1, 0}};
            Attack(1, a);
        }
        */

        if(BattleManager.instance.gobattle == true)
        {
            

            if(Input.GetKeyDown(KeyCode.X) == true)
            {
                if(turn == 1)
                {
                    ActiveCard(FirstCard);
                    BattleManager.instance.LifeSystem();
                    turn++;
                }
                else if(turn == 2)
                {
                    Boss.GetComponent<BossController>().RandomActivate();
                    BattleManager.instance.LifeSystem();
                    turn++;
                }
                else if(turn == 3)
                {
                    ActiveCard(SecondCard);
                    BattleManager.instance.LifeSystem();
                    turn++;
                }
                else if(turn == 4)
                {
                    Boss.GetComponent<BossController>().RandomActivate();
                    BattleManager.instance.LifeSystem();
                    turn++;
                }
                else if(turn == 5)
                {
                    ActiveCard(ThirdCard);
                    BattleManager.instance.LifeSystem();
                    turn++;
                }
                else if(turn == 6)
                {
                    Boss.GetComponent<BossController>().RandomActivate();
                    BattleManager.instance.LifeSystem();
                    BattleManager.instance.gobattle = false;
                    Debug.Log("Battle Finished");
                    turn++;
                }
                else if(turn == 7)
                {
                    turn = 1;
                    BattleManager.instance.ready = false;
                    BattleManager.instance.Cleargocard();
                }
                
            }

        }
        

    }

    private void MoveLeft()
    {
        // 이동 오디오 클립 재생
        congdingAudio.clip = MoveClip;
        congdingAudio.Play();

        //왼쪽으로 한 칸 움직이는 함수
        //콩딩의 위치가 맨 왼쪽이 아닐 경우에 이동
        if(congdingpos[0] > 0)
        {
            // 콩딩의 x좌표 변경
            vector.x = -3.2f; 
            vector.y = 0.0f;
            transform.Translate(vector);
            congdingpos[0]--; // 콩딩의 발판 위치값 수정
        }
        
    }

    private void MoveRight()
    {
        // 이동 오디오 클립 재생
        congdingAudio.clip = MoveClip;
        congdingAudio.Play();

        //오른쪽으로 한 칸 움직이는 함수
        //콩딩의 위치가 맨 오른쪽이 아닐 경우에 이동
        if(congdingpos[0] < 4)
        {
            // 콩딩의 x좌표 변경
            vector.x = 3.2f; 
            vector.y = 0.0f;
            transform.Translate(vector);
            congdingpos[0]++; // 콩딩의 발판 위치값 수정

        }
        
    }

    private void MoveUp()
    {
        // 이동 오디오 클립 재생
        congdingAudio.clip = MoveClip;
        congdingAudio.Play();

        //위쪽으로 한 칸 움직이는 함수
        //콩딩의 위치가 맨 위쪽이 아닐 경우에 이동
        if(congdingpos[1] > 0)
        {
            // 콩딩의 y좌표 변경
            vector.x = 0.0f;
            vector.y = 2.0f; 
            transform.Translate(vector);

            congdingpos[1]--; // 콩딩의 발판 위치값 수정

        }
        
    }

    private void MoveDown()
    {
        // 이동 오디오 클립 재생
        congdingAudio.clip = MoveClip;
        congdingAudio.Play();

        //아래쪽으로 한 칸 움직이는 함수
        //콩딩의 위치가 맨 아래쪽이 아닐 경우에 이동
        if(congdingpos[1] < 2)
        {
            // 콩딩의 y좌표 변경
            vector.x = 0.0f;
            vector.y = -2.0f; 
            transform.Translate(vector);

            congdingpos[1]++; // 콩딩의 발판 위치값 수정
        }
        
    }

    private void Attack(int damage, int[,] range) // damage = 공격력, range = 공격 범위
    {
        
        // 공격범위는 공격자의 위치를 (0, 0)이라고 할 때 공격 범위에 해당하는 좌표들의 배열
        // 예를 들어, 우측으로 3칸짜리 공격을 구현하고 싶다면 {{1, 0}, {2, 0}, {3, 0}}를 input
        int[,] field = new int [5, 3]; // 좌표계
       
        int rangesize = range.GetLength(0);

        int boss_x = Boss.GetComponent<BossController>().bosspos[0];
        int boss_y = Boss.GetComponent<BossController>().bosspos[1];


        if(true)
        {

            //적을 공격하는 함수

            // 공격 애니메이션 재생
            animator.SetTrigger("Attack");

            // 공격 오디오 클립 재생
            congdingAudio.clip = AttackClip;
            congdingAudio.Play();

            // 공격범위의 field 좌표의 값을 1로 설정
            // range값을 콩딩의 좌표에 따라 field의 좌표로 이동하여 맞춤
            for(int p = 0; p < rangesize; p++)
            {
                if(congdingpos[0] + range[p,0] >= 0 && congdingpos[0] + range[p,0] <= 4 && congdingpos[1] + range[p,1] >= 0 && congdingpos[1] + range[p,1] <= 2)
                {
                    field[congdingpos[0] + range[p,0], congdingpos[1] + range[p,1]] = 1;
                }
                
            }

            //공격대상이 공격 범위 안에 있을 경우 공격
            for(int i = 0; i < 5; i++)
            {
                for(int t = 0; t < 3; t++)
                {
                    if(field[i,t] == 1 && boss_x == i && boss_y == t )
                    {
                        Instantiate(AttackEffect, Boss.GetComponent<BossController>().bossvec, Quaternion.Euler(Vector2.zero));
                        BattleManager.instance.boss_life -= damage;
                        Debug.Log("Attack" + damage + " damage!");
                    }
                }
            }

            
        }

    }

    public void Attacked(int damage) // damage = 받은 데미지
    {
        // 피격 애니메이션을 재생하고 데미지를 계산하여 라이프를 감소시킨다
        
        animator.SetTrigger("Attacked");
        BattleManager.instance.life -= damage;
            
    }

    private void Heal(int healing) // healing = 힐량
    {
        BattleManager.instance.life += healing;
    }

    private void ActiveCard(GameObject card)
    {
        string name = card.GetComponent<CardInfo>().cardname;
        int type = card.GetComponent<CardInfo>().cardtype;
        int para = card.GetComponent<CardInfo>().parameter;
        

        if(name == "PUNCH")
        {
            int[,] range = new int[3, 2] {{-1, 0}, {0, 0}, {1, 0}};
            Attack(para, range);
            
            Debug.Log("PUNCH");

        }
        else if(name == "MOVE-RIGHT")
        {
            MoveRight();
            Debug.Log("MOVE-RIGHT");
        }
        else if(name == "MOVE-UP")
        {
            MoveUp();
            Debug.Log("MOVE-UP");
        }
        else if(name == "MOVE-DOWN")
        {
            MoveDown();
            Debug.Log("MOVE-DOWN");
        }
        else if(name == "MOVE-LEFT")
        {
            MoveLeft();
            Debug.Log("MOVE-LEFT");
        }
        else if(name == "POWER-PUNCH")
        {
            if(BattleManager.instance.life <= 1)
            {
                para *= 2;
                int[,] range = new int [3, 2] {{-1, 0}, {0, 0}, {1, 0}};
                Attack(para, range);
            }
            else
            {
                int[,] range = new int [3, 2] {{-1, 0}, {0, 0}, {1, 0}};
                Attack(para, range);
            }
            Debug.Log("POWER-PUNCH");
        }
    }
    IEnumerator Delay()
    {
         
        yield return new WaitForSeconds(3f);
        
    }
}
