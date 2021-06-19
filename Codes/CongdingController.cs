//
//  CongdingController.cs
//  Congding
//
//  Created by Lee Suyeon on 2021/06/18.
//  Copyright (c) 2021 Will KHU-GCC. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// CongdingController는 플레이어 캐릭터로서 Congding 게임 오브젝트를 제어함.
public class CongdingController : MonoBehaviour
{
    public AudioClip jumpClip; //  점프 시 재생할 오디오 재생 클립
    public AudioClip dashClip; // 대쉬 시 재생할 오디오 재생 클립
    public AudioClip deathClip; // 사망 시 재생할 오디오 재생 클립
    public AudioClip crashClip; // 충돌 시 재생할 오디오 재생 클립
    public AudioClip breakClip; // 장애물 부술 때 재생할 오디오 재생 클립
    public AudioClip heartClip; // 하트아이템을 먹었을 시 재생할 오디오 재생 클립
    public AudioClip starClip; // 별아이템을 먹었을 시 재생할 오디오 재생 클립
    public AudioClip diaClip; // 다이아아이템을 먹었을 시 재생할 오디오 재생 클립
    public AudioClip invincible; // 무적 상태일 때 재생할 오디오 재생 클립

    //public GameObject dashEffect; // 대쉬 시 재생할 대쉬 이펙트
    //public GameObject jumpEffect; // 점프 시 재생할 점프 이펙트
    //public GameObject runEffect; // 달릴 시 재생할 달리기 이펙트

    public float jumpForce = 500f; // 점프 힘
    private float defaultSpeed; // 달리는 속도
    public float speed; // 기본 속도
    public float dashSpeed; // 대쉬시 속도
    public float defaultDashRTime; // 정해진 대쉬토큰 회복시간
    private float dashRecoveryTime; // 현재 남은 대쉬토큰 회복 시간
    public float defaultFCheckTime; // 정해진 floating check시간
    private float floatingTime; // 현재 콩딩이가 떠있는 시간
    public float defaultDTime; // 정해진 대쉬시간
    private float dashTime; // 대쉬 중 남은 대쉬 시간
    public float defaultITime; // 정해진 무적시간
    public float starITime; // 별 아이템을 먹었을 때 무적시간
    private float invincibleTime; // 무적 중 남은 무적 시간
    private Vector3 vector; // 콩딩이의 위치

    private int jumpCount = 0; // 누적 점프 횟수

    public bool isFloating = false; // 콩딩이가 공중에 떠있는 상태인지 감지
    public bool isGrounded = false; // 콩딩이가 바닥에 닿아있는 상태인지 감지
    public bool isCheckingGrounded = false; // Grounded인지 체크하고있는 중인가.
    private bool isTimer = false; // 현재 타이머가 돌아가고 있는가.
    private bool timeUp = false; // 타이머 종료.
    public bool isDash = false; // 대쉬 중인지 나타냄
    public bool isDashRecover = false; // 현재 대쉬 회복 카운트 중인지를 나타냄.
    public bool isDead = false; // 사망상태
    public bool isInvincible = false; // 무적상태
    public bool isPaused = false; // 일시정지

    private float deadTime = 1; // 데미지 입고서 죽기 모션 나오기까지의 시간
    private float timerTime = 0; // 타이머 함수에 들어갈 타임값

    private Rigidbody2D congdingRigidbody; // 사용할 리지드바디 컴포넌트
    private Animator animator; // 사용할 애니메이터 컴포넌트
    private AudioSource congdingAudio; // 사용할 오디오 소스 컴포넌트
    private BreakingObject otherScript; //충돌하는 오브젝트의 스크립트(충돌후에 없어지라고 명령을 줄 것.)

    private void Start()
    {
        // 초기화
        // 게임 오브젝트로부터 사용할 컴포넌트들을 가져와 변수에 할당
        congdingRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        congdingAudio = GetComponent<AudioSource>();

        //시작할 땐 기본 speed로 시작.
        defaultSpeed = speed;

    }

    private void Update()
    {
        // 콩딩이 위치 받아오기
        vector = transform.position;

        // 타이머
        if (isTimer)
        {
            TimeCounter();
        }

        if (!GameManager.instance.isGameover && !GameManager.instance.isPause)
        {
            Jump();
            Dash();
            DashRecovery();
            Invincible();
            CheckGrounded();
            RunScore();
        }
    }

    //타이머 함수
    // 파라메터로 타이머 설정할 시간을 받는다.
    private void Timer(float time)
    {
        timerTime = time;
        isTimer = true;
        timeUp = false;
    }

    //타이머 카운트 함수
    private void TimeCounter()
    {
        if (timerTime > 0)
        {
            timerTime -= Time.deltaTime;
        }
        else
        {
            isTimer = false;

            // 타이머 종료
            timeUp = true;

            // 로그 확인
            Debug.Log("Time up");
        }
    }

    // 콩딩이가 랜드에 잘 닿아있는지 검사
    private void CheckGrounded()
    {
        // land 에 닿아있을 때는 isGrounded 상태.
        if (!isFloating)
        {
            isGrounded = true;
            isCheckingGrounded = false;
        }
        // 콩딩이가 땅에 닿아있지 않는 상태라면
        else
        {
            // 이미 grounded check가 돌아가고있는가.
            if (!isCheckingGrounded)
            {
                isCheckingGrounded = true;

                floatingTime = defaultFCheckTime;
            }
            else
            {
                // 체크 중이라면
                if(floatingTime <= 0)
                {
                    isGrounded = false;
                    isCheckingGrounded = false;
                }
                else
                {
                    floatingTime -= Time.deltaTime;
                }
            }
        }   
    }

    private void Die()
    {
        // 사망 처리
        animator.SetTrigger("Die");

        // 오디오 소스에 할당된 오디오 클립을 deathClip으로 변경
        congdingAudio.clip = deathClip;
        // 사망 효과음 재생
        congdingAudio.Play();

        // 속도를 제로(0, 0)으로 변경
        congdingRigidbody.velocity = Vector2.zero;

        // 사망상태를 true로 변경
        isDead = true;

        // 게임 매니저의 게임오버 처리 실행
        GameManager.instance.OnPlayerDead();

        // 로그 확인
        Debug.Log("Die");
    }

    private void Jump()
    {
        // 'z'키보드버튼을 눌렀으며 && 최대 점프 횟수(1)에 도달하지 않았다면
        if (Input.GetKeyDown(KeyCode.Z) && jumpCount < 1 && isGrounded)
        {
            //점프 횟수 증가
            jumpCount++;

            //점프 직전에 속도를 순간적으로 제로(0, 0)로 변경
            congdingRigidbody.velocity = Vector2.zero;
            // 리지드바디에 위쪽으로 힘 주기
            congdingRigidbody.AddForce(new Vector2(0, jumpForce));

            //점프 오디오 소스 재생
            congdingAudio.clip = jumpClip;
            congdingAudio.Play();

            //점프 이펙트 재생
            //Instantiate(jumpEffect, vector, Quaternion.Euler(Vector3.zero));

            //Isgrounded값 변경 (OnCollisionExit2D 함수가 작동하지 않아 애니메이션 확인을 위한 임시코드)
            //isGrounded = false;
        }
        // 'z'키보드 버튼에서 손을 떼는 순간 && 속도의 y값이 양수라면(위로 상승 중)
        else if (Input.GetKeyUp(KeyCode.Z) && congdingRigidbody.velocity.y > 0)
        {
            //현재 속도를 절반으로 변경
            congdingRigidbody.velocity = congdingRigidbody.velocity * 0.5f;
        }
        //애니메이터의 Grounded 파라미터를 isFloating 값으로 갱신
        animator.SetBool("Grounded", !isFloating);
    }

    private void Dash()
    {
        //X키 입력을 받은 상태에서 dash토큰이 0개보다 많다면
        if (Input.GetKeyDown(KeyCode.X) && GameManager.instance.dash > 0)
        {
            GameManager.instance.dash--;
            //대쉬 상태로 돌입
            isDash = true;

            //대쉬 오디오 소스 재생
            congdingAudio.clip = dashClip;
            congdingAudio.Play();

            //대쉬 효과 이펙트 재생
            //vector.x += 2;
            //Instantiate(dashEffect, vector, Quaternion.Euler(Vector3.zero));

            //대쉬 0.2초 초기화(대쉬 중이더라도 대쉬키를 한번 더 누르면 누른 시점부터 다시 대쉬.)
            dashTime = defaultDTime;
        }

        if (isDash && dashTime > 0)
        {
            //Debug.Log(isDash);
            dashTime -= Time.deltaTime;
            defaultSpeed = dashSpeed;
        }
        else
        {
            defaultSpeed = speed;
            isDash = false;
            dashTime = defaultDTime;
        }
        animator.SetBool("IsDash", isDash);
    }

    private void DashRecovery()
    {
        // 대쉬 회복카운트가 돌아가고있지 않고 대쉬토큰이 풀로 차있지 않을시에
        if (!isDashRecover && GameManager.instance.dash != 5)
        {
            // 대쉬회복 상태로 돌입
            isDashRecover = true;

            // 남은 대쉬회복 타임을 default로 초기화
            dashRecoveryTime = defaultDashRTime;
        }

        // 대쉬 회복 카운트가 다 돌아갔다면
        if (isDashRecover && dashRecoveryTime <= 0)
        {
            // 대쉬 토큰이 풀로 차있지 않을시에
            if (GameManager.instance.dash < 5)
            {
                // 대쉬 토큰 1 회복
                GameManager.instance.dash++;

                //로그 확인
                Debug.Log("Dash Recovery");
            }

            //대쉬 회복상태 종료
            isDashRecover = false;
        }
        else
        {
            dashRecoveryTime -= Time.deltaTime;
        }
    }

    //무적 상태
    private void Invincible()
    {
        if (isInvincible && invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;

            //무적 오디오 소스 재생
            //무적 애니메이션

        }
        else
        {
            isInvincible = false;
            invincibleTime = defaultITime;

            //무적 오디오 소스 재생중지
            //무적 애니메이션 중지
        }
    }

    //충돌 처리 함수
    //파라메터 : 충돌한 게임 오브젝트 콜라이더
    private void Hurt(Collider2D other)
    {
        //무적상태가 아닐 때
        if (!isInvincible)
        {
            //life 1감소
            GameManager.instance.life--;

            //score 감소
            GameManager.instance.AddScore(-100);

            // 충돌 소리 출력
            congdingAudio.clip = crashClip;
            congdingAudio.Play();

            //life가 0이면(1보다 작으면) 콩딩이는 죽은 거랍니다.
            if (GameManager.instance.life < 1)
            {
                // 사망 처리
                Die();

                // 로그 확인
                Debug.Log("Die");
            }
            else
            {
                //콩딩이 아야 애니메이션

                //충돌 후 일정 시간동안 무적
                isInvincible = true;

                // 로그 확인
                Debug.Log("crash.");
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
        //Tag별로 구분.

        if (other.tag == "Indestructible")
        {
            Hurt(other);
        }
        else if (other.tag == "Destructible" && isDash && other.GetComponent<isBreakable>().breakable == true)
        {
            // 부술 수 있는 장애물과 충돌 && 대쉬 중일 시
            // 해당 오브젝트의 충돌을 관리하는 코드에 부서졌다고 알린다.
            otherScript = other.GetComponent<BreakingObject>();
            otherScript.isBreaked = true;

            // 파괴 소리 출력
            congdingAudio.clip = breakClip;
            congdingAudio.Play();

            // 스코어 업
            GameManager.instance.AddScore(10);

            // 로그 확인
            Debug.Log("break");
        }
        else if (other.tag == "Destructible")
        {
            Hurt(other);
        }
        else if (other.tag == "Fall")
        {
            GameManager.instance.life = 0;
            Die();
        }
        else if (other.tag == "Heart")
        {
            // 하트 아이템을 먹었을 시
            // 라이프 +1(최대 6)
            GameManager.instance.life++;

            if (GameManager.instance.life > 6)
            {
                GameManager.instance.life = 6;
            }

            // 충돌한 하트 오브젝트 제거
            Destroy(other.gameObject);

            // 하트 소리 출력
            congdingAudio.clip = heartClip;
            congdingAudio.Play();

            // 스코어 업
            GameManager.instance.AddScore(10);

            // 로그 확인
            Debug.Log("Heart item");

        }
        else if (other.tag == "Star")
        {
            //별 아이템을 먹었을 시
            //3초간 무적
            invincibleTime = starITime;
            isInvincible = true;

            //충돌한 하트 오브젝트 제거
            Destroy(other.gameObject);

            //스타 소리 출력
            congdingAudio.clip = starClip;
            congdingAudio.Play();

            // 스코어 업
            GameManager.instance.AddScore(10);

            //로그 확인
            Debug.Log("Star item");
        }
        else if (other.tag == "Diamond")
        {
            //다이아 아이템을 먹었을 시
            //대쉬스택 풀 충전
            GameManager.instance.dash = 5;

            //충돌한 다이아 오브젝트 제거
            Destroy(other.gameObject);

            //다이아 소리 출력
            congdingAudio.clip = diaClip;
            congdingAudio.Play();

            // 스코어 업
            GameManager.instance.AddScore(10);

            //로그 확인
            Debug.Log("Diamond item");
        }
        else if(other.tag == "Talk")
        {
            // 대사창을 띄워야하는 지점이 도달했을 시.
            // 충돌한 오브젝트의 이름을 GameManager의 Talk함수로 넘긴다.
            GameManager.instance.onTalk = true;
            GameManager.instance.GetTalk(other.name);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿았음을 감지하는 처리
        //어떤 콜라이더와 닿았으며, 충돌 표면이 위쪽을 보고 있다면
        if (collision.contacts[0].normal.y > 0.7)
        {
            //isGrounded를 true로 변경하고, 누적 점프 횟수를 0으로 리셋
            isFloating = false;
            jumpCount = 0;

            //달리는 이펙트 재생
            //Instantiate(runEffect, vector, Quaternion.Euler(Vector3.zero));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 벗어났음을 감지하는 처리
        //Land 콜라이더에서 떼어진 경우 isGrounded를 false로 변경
        isFloating = true;
    }

    private void RunScore()
    {
        //그냥 달리면서 얻는 스코어 처리

        GameManager.instance.AddScore(Mathf.CeilToInt(speed * 0.1f));
        //적당한게 안떠올라서 임시로 속도비례 처리함 (나중에 바꿔야 하는 이유가 무지성대쉬로 스피드런하면 점수가 더 안나옴 ㅎㅎ;)
    }
}
