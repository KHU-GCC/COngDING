//
//  GameManager.cs
//  Congding
//
//  Created by Lee Suyeon on 2021/06/18.
//  Copyright (c) 2021 Will KHU-GCC. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// 게임오버 상태를 표현하고, 게임 점수와 UI를 관리하는 게임 매니저
// 씬에는 단 하나의 게임 매니저만 존재할 수 있음
public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글턴을 할당할 전역 변수
    public GameObject talkManager; // 대사창을 관리하는 TalkManager
    public GameObject talkUI; // 대화창 UI 게임 오브젝트
    public Text talkText; // 대화창의 대사를 나타낼 Text오브젝트
    public string talkTag; // 어느 대사를 나타낼 것인가 구분짓는 태그
    public static string talkLog; // 대화를 어디까지 봤는지 체크하는 value, 게임이 재시작되도 기억하도록하여 해당판을 restart했을 때 같은 대화를 또 보는 것을 방지한다.
    public GameObject gameoverUI; // 게임오버 시 활성화할 UI 게임 오브젝트
    public GameObject pauseUI; // 일시정지 시 활성화할 UI 게임 오브젝트
    public int score = 0; // 게임 점수
    public int life = 6; // 콩딩이 life, 최대 6.
    public int dash = 5; // 콩딩이 대쉬 토큰, 최대 5.
    private int talkIndex = 0; // 대사창에 나타낼 대화 길이
    private string talkString; // 대화창에 나올 string

    public bool isGameover = false; // 게임오버 상태
    public bool isPause = false; // 일시정지 상태
    public bool onTalk = false; // 대화 상태
    public Text scoreText; // 점수를 출력할 UI 텍스트
    public Text highscoreText; // 하이스코어를 출력할 UI 텍스트

    public GameObject heart1; // 첫번째 하트 UI
    public GameObject heart2; // 두번째 하트 UI
    public GameObject heart3; // 세번째 하트 UI

    public Sprite full_heart; // 꽉 차 있는 하트 스프라이트
    public Sprite half_heart; // 반 차 있는 하트 스프라이트
    public Sprite empty_heart; // 비어있는 하트 스프라이트

    public GameObject dash1; // 첫번째 대쉬 토큰 UI
    public GameObject dash2; // 두번째 대쉬 토큰 UI
    public GameObject dash3; // 세번째 대쉬 토큰 UI
    public GameObject dash4; // 네번째 대쉬 토큰 UI
    public GameObject dash5; // 다섯번째 대쉬 토큰 UI

    public Sprite dashg; // 대쉬게이지 스프라이트
    public Sprite emptyDashg; // 비어있는 대쉬게이지 스프라이트



    // 게임 시작과 동시에 싱글턴을 구성
    void Awake()
    {
        // 싱글턴 변수 instance가 비어 있는가?
        if (instance == null)
        {
            // instance가 비어 있다면(null)  그곳에 자기 자신을 할당
            instance = this;
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우

            // 씬에 두 개 이상의 GameManager 오브젝트가 존재한다는 의미
            // 싱글턴 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두 개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
        // talkManager지정, 같은 씬 안의 이름이 TalkManager인 오브젝트.
        // talkManager = GameObject.Find("TalkManager");

        // 대사창 UI 오브젝트
        // 비활성화된 오브젝트는 GameObject.Find로 찾는 것이 불가능하다.
        // talkUI = GameObject.Find("TalkUI");

        // talkUI의 자식중 이름이 Text인 게임 오브젝트의 Text컴포넌트의 text
        // talkText = talkUI.transform.Find("Text").gameObject.GetComponent<Text>().text;

    }

    void Update()
    {
        // 게임오버 상태에서 게임을 재시작할 수 있게 하는 처리
        // 현재는 재시작 조건을 마우스 왼쪽 버튼으로 해놨지만 나중에 gameover창을 구현하면
        // 게임 오버 창의 재시작을 누르면 다시 하는 것으로 수정할 것.
        /* 
        if(isGameover && Input.GetMouseButtonDown(0))
        {
            // 게임오버 상태에서 마우스 왼쪽 버튼을 클릭하면 현재 씬 재시작
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        */

        // 현재 콩딩이의 life에 따라 하트 UI를 나타내주는 작업.
        HeartControl();

        // 현재 콩딩이의 dash토큰 수에 따라 대쉬게이지 UI를 나타내주는 작업.
        DashControl();

        // 대사창 관리
        Talking(talkTag);
    }

    // 점수를 증가시키는 메서드
    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            // 점수를 증가
            score += newScore;
            if (score >= 0)
            {
                scoreText.text = "Score : " + score;
            }
            else
            {
                scoreText.text = "Score : " + "0";
            }
        }
    }

    // 하트(life)UI를 관리하는 코드
    public void HeartControl()
    {
        if (life == 6)
        {
            heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
            heart2.GetComponent<SpriteRenderer>().sprite = full_heart;
            heart3.GetComponent<SpriteRenderer>().sprite = full_heart;
        }
        else if (life == 5)
        {
            heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
            heart2.GetComponent<SpriteRenderer>().sprite = full_heart;
            heart3.GetComponent<SpriteRenderer>().sprite = half_heart;
        }
        else if (life == 4)
        {
            heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
            heart2.GetComponent<SpriteRenderer>().sprite = full_heart;
            heart3.GetComponent<SpriteRenderer>().sprite = empty_heart;
        }
        else if (life == 3)
        {
            heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
            heart2.GetComponent<SpriteRenderer>().sprite = half_heart;
            heart3.GetComponent<SpriteRenderer>().sprite = empty_heart;
        }
        else if (life == 2)
        {
            heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
            heart2.GetComponent<SpriteRenderer>().sprite = empty_heart;
            heart3.GetComponent<SpriteRenderer>().sprite = empty_heart;
        }
        else if (life == 1)
        {
            heart1.GetComponent<SpriteRenderer>().sprite = half_heart;
            heart2.GetComponent<SpriteRenderer>().sprite = empty_heart;
            heart3.GetComponent<SpriteRenderer>().sprite = empty_heart;
        }
        else
        {
            heart1.GetComponent<SpriteRenderer>().sprite = empty_heart;
            heart2.GetComponent<SpriteRenderer>().sprite = empty_heart;
            heart3.GetComponent<SpriteRenderer>().sprite = empty_heart;
        }
    }

    public void DashControl()
    {
        if (dash == 5)
        {
            dash1.GetComponent<SpriteRenderer>().sprite = dashg;
            dash2.GetComponent<SpriteRenderer>().sprite = dashg;
            dash3.GetComponent<SpriteRenderer>().sprite = dashg;
            dash4.GetComponent<SpriteRenderer>().sprite = dashg;
            dash5.GetComponent<SpriteRenderer>().sprite = dashg;
        }
        else if (dash == 4)
        {
            dash1.GetComponent<SpriteRenderer>().sprite = dashg;
            dash2.GetComponent<SpriteRenderer>().sprite = dashg;
            dash3.GetComponent<SpriteRenderer>().sprite = dashg;
            dash4.GetComponent<SpriteRenderer>().sprite = dashg;
            dash5.GetComponent<SpriteRenderer>().sprite = emptyDashg;
        }
        else if (dash == 3)
        {
            dash1.GetComponent<SpriteRenderer>().sprite = dashg;
            dash2.GetComponent<SpriteRenderer>().sprite = dashg;
            dash3.GetComponent<SpriteRenderer>().sprite = dashg;
            dash4.GetComponent<SpriteRenderer>().sprite = emptyDashg;
            dash5.GetComponent<SpriteRenderer>().sprite = emptyDashg;
        }
        else if (dash == 2)
        {
            dash1.GetComponent<SpriteRenderer>().sprite = dashg;
            dash2.GetComponent<SpriteRenderer>().sprite = dashg;
            dash3.GetComponent<SpriteRenderer>().sprite = emptyDashg;
            dash4.GetComponent<SpriteRenderer>().sprite = emptyDashg;
            dash5.GetComponent<SpriteRenderer>().sprite = emptyDashg;
        }
        else if (dash == 1)
        {
            dash1.GetComponent<SpriteRenderer>().sprite = dashg;
            dash2.GetComponent<SpriteRenderer>().sprite = emptyDashg;
            dash3.GetComponent<SpriteRenderer>().sprite = emptyDashg;
            dash4.GetComponent<SpriteRenderer>().sprite = emptyDashg;
            dash5.GetComponent<SpriteRenderer>().sprite = emptyDashg;
        }
        else
        {
            dash1.GetComponent<SpriteRenderer>().sprite = emptyDashg;
            dash2.GetComponent<SpriteRenderer>().sprite = emptyDashg;
            dash3.GetComponent<SpriteRenderer>().sprite = emptyDashg;
            dash4.GetComponent<SpriteRenderer>().sprite = emptyDashg;
            dash5.GetComponent<SpriteRenderer>().sprite = emptyDashg;
        }
    }

    // 대사창 띄우기.
    // 콩딩이가 충돌한 Talk collider오브젝트의 이름을 받는다.
    // 나중에 talkManager의 key를 string에서 int로 바꾸면 그때는 talkTag != talkLog 코드를 숫자 비교로 바꾼다.
    // 지금은 대화가 하나밖에 없으니까 되는 코드.
    public void GetTalk(string talkName)
    {
        talkTag = talkName;

        if (talkTag != talkLog)
        {
            if (talkManager.GetComponent<TalkManager>().GetTalk(talkName, talkIndex) != null)
            {
                talkUI.SetActive(true);

                // 대화상태 온
                onTalk = true;

                // 게임 일시정지
                isPause = true;

                talkString = talkManager.GetComponent<TalkManager>().GetTalk(talkName, talkIndex);
            }
            else
            {
                //대화 종료
                onTalk = false;

                // 일시정지 종료
                isPause = false;

                // 대화창 닫기
                talkUI.SetActive(false);

                // 현재 대사까지 봤다고 기록
                talkLog = talkTag;

                //talkIndex 초기화
                talkIndex = 0;
            }
        }
    }

    public void Talking(string talk)
    {
        if (onTalk)
        {
            talkText.text = talkString;
            if (Input.GetMouseButtonDown(0))
            {
                talkIndex++;

                // 나중에 고치기
                GetTalk(talk);
            }
        }
    }


    // 콩딩이가 사망 시 게임오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        isGameover = true;

        Debug.Log("GameOver");

        // 게임 오버 창 활성화(게임오버상태가 아닐때에는 비활성화 상태로 둔다.)
        gameoverUI.SetActive(true);
        highscoreText.text = "Score : " + score;

    }

    public void OnPause()
    {
        if (!onTalk)
        {
            if (!isPause)
            {
                isPause = true;

                Debug.Log("Pause");

                // 일시정지 창 활성화
                pauseUI.SetActive(true);

            }
            else if (isPause)
            {
                isPause = false;

                Debug.Log("Continue");

                // 일시정지 창 비활성화
                pauseUI.SetActive(false);
            }
        }
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("Bossstage");
    }
}
