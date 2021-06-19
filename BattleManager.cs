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
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public GameObject windowUI; // 윈도우 UI 오브젝트
    public GameObject cardUI; // 카드 UI 오브젝트

    public GameObject heart1; // 첫번째 하트 UI
    public GameObject heart2; // 두번째 하트 UI
    public GameObject heart3; // 세번째 하트 UI

    public GameObject boss_heart1; // 보스 첫번째 하트 UI
    public GameObject boss_heart2; // 보스 두번째 하트 UI
    public GameObject boss_heart3; // 보스 세번째 하트 UI


    public Sprite full_heart; // 꽉 차 있는 하트 스프라이트
    public Sprite half_heart; // 반 차 있는 하트 스프라이트
    public Sprite empty_heart; // 비어있는 하트 스프라이트

    public int life = 6;
    public int boss_life = 6;

    public bool isclicked = false; // 현재 무엇인가를 클릭하고 있나요?
    public int clickedcard = 0; // 덱에서 현재 선택한 카드 (0 = 선택하지 않음)
    public int clickedgocard = 0;
    public bool isfull = false; // 카드를 3장 모두 선택했니?
    public bool gobattle = false;
    public GameObject SelectedCard; // 선택된 카드 오브젝트 프리펩

    public GameObject card1; // 덱의 첫 번째 카드
    public GameObject card2; // 덱의 두 번째 카드
    public GameObject card3; // 덱의 세 번째 카드
    public GameObject card4; // 덱의 네 번째 카드
    public GameObject card5; // 덱의 다섯 번째 카드

    public GameObject gocard1; // 실행카드 1
    public GameObject gocard2; // 실행카드 2
    public GameObject gocard3; // 실행카드 3

    public bool setgocard1 = false; // 실행카드가 세팅되어있니??????
    public bool setgocard2 = false;
    public bool setgocard3 = false;


    public GameObject AttackCard; // 공격 카드 
    public GameObject MoveCard; // 이동 카드

    public GameObject InfoUI;
    public GameObject Infocard; // 정보 카드
    public Text Infotext; // 카드 정보 텍스트

    public Animator thinkingCongding;




    // 게임 시작과 동시에 싱글턴을 구성
    
    void Awake()
    {
        // 싱글턴 변수 instance가 비어 있는가?
        if(instance == null)
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
    }
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(setgocard1 && setgocard2 && setgocard3)
        {
            isfull = true;
        }
        else
        {
            isfull = false;
        }
        HeartControl();
        
        if(Input.GetKeyDown(KeyCode.X))
        {
             StartCoroutine(MyCo());
             DisactiveWindow();
         }

    }

    // 하트(life)UI를 관리하는 코드
    public void HeartControl()
    {
        if(life == 6)
        {
            heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
            heart2.GetComponent<SpriteRenderer>().sprite = full_heart;
            heart3.GetComponent<SpriteRenderer>().sprite = full_heart;
        }
        else if(life == 5)
        {
            heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
            heart2.GetComponent<SpriteRenderer>().sprite = full_heart;
            heart3.GetComponent<SpriteRenderer>().sprite = half_heart;
        }
        else if(life == 4)
        {
            heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
            heart2.GetComponent<SpriteRenderer>().sprite = full_heart;
            heart3.GetComponent<SpriteRenderer>().sprite = empty_heart;
        }
        else if(life == 3)
        {
            heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
            heart2.GetComponent<SpriteRenderer>().sprite = half_heart;
            heart3.GetComponent<SpriteRenderer>().sprite = empty_heart;
        }
        else if(life == 2)
        {
            heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
            heart2.GetComponent<SpriteRenderer>().sprite = empty_heart;
            heart3.GetComponent<SpriteRenderer>().sprite = empty_heart;
        }
        else if(life == 1)
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

        //보스 하트 관리
        if(boss_life == 6)
        {
            boss_heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
            boss_heart2.GetComponent<SpriteRenderer>().sprite = full_heart;
            boss_heart3.GetComponent<SpriteRenderer>().sprite = full_heart;
        }
        else if(boss_life == 5)
        {
            boss_heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
            boss_heart2.GetComponent<SpriteRenderer>().sprite = full_heart;
            boss_heart3.GetComponent<SpriteRenderer>().sprite = half_heart;
        }
        else if(boss_life == 4)
        {
            boss_heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
            boss_heart2.GetComponent<SpriteRenderer>().sprite = full_heart;
            boss_heart3.GetComponent<SpriteRenderer>().sprite = empty_heart;
        }
        else if(boss_life == 3)
        {
            boss_heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
            boss_heart2.GetComponent<SpriteRenderer>().sprite = half_heart;
            boss_heart3.GetComponent<SpriteRenderer>().sprite = empty_heart;
        }
        else if(boss_life == 2)
        {
            boss_heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
            boss_heart2.GetComponent<SpriteRenderer>().sprite = empty_heart;
            boss_heart3.GetComponent<SpriteRenderer>().sprite = empty_heart;
        }
        else if(boss_life == 1)
        {
            boss_heart1.GetComponent<SpriteRenderer>().sprite = half_heart;
            boss_heart2.GetComponent<SpriteRenderer>().sprite = empty_heart;
            boss_heart3.GetComponent<SpriteRenderer>().sprite = empty_heart;
        }
        else
        {
            boss_heart1.GetComponent<SpriteRenderer>().sprite = empty_heart;
            boss_heart2.GetComponent<SpriteRenderer>().sprite = empty_heart;
            boss_heart3.GetComponent<SpriteRenderer>().sprite = empty_heart;
        }
    }

    
    public void SelectedCardControl()
    {
        
        InfoUI.SetActive(true);
        
        Infocard.GetComponent<CardInfo>().cardname = SelectedCard.GetComponent<CardInfo>().cardname;
        Infocard.GetComponent<CardInfo>().cardtype = SelectedCard.GetComponent<CardInfo>().cardtype;
        Infocard.GetComponent<CardInfo>().cardDatatype = SelectedCard.GetComponent<CardInfo>().cardDatatype;
        Infocard.GetComponent<CardInfo>().cardinformation = SelectedCard.GetComponent<CardInfo>().cardinformation;
        
        Infotext.text = Infocard.GetComponent<CardInfo>().cardinformation;

        Debug.Log("Good");
        //Infoimage.GetComponent<SpriteRenderer>().sprite = 
        //Infocard.GetComponent<CardSystem>().RenderCard();
    }

    public void GoCardControl()
    {
        if(clickedgocard == 1)
        {
            gocard1.SetActive(true);
            setgocard1 = true;
            gocard1.GetComponent<CardInfo>().cardname = SelectedCard.GetComponent<CardInfo>().cardname;
            gocard1.GetComponent<CardInfo>().cardtype = SelectedCard.GetComponent<CardInfo>().cardtype;
            gocard1.GetComponent<CardInfo>().cardDatatype = SelectedCard.GetComponent<CardInfo>().cardDatatype;
            gocard1.GetComponent<CardInfo>().cardinformation = SelectedCard.GetComponent<CardInfo>().cardinformation;
            gocard1.GetComponent<CardInfo>().parameter = SelectedCard.GetComponent<CardInfo>().parameter;
            clickedgocard = 0;
        }
        else if(clickedgocard == 2)
        {
            gocard2.SetActive(true);
            setgocard2 = true;
            gocard2.GetComponent<CardInfo>().cardname = SelectedCard.GetComponent<CardInfo>().cardname;
            gocard2.GetComponent<CardInfo>().cardtype = SelectedCard.GetComponent<CardInfo>().cardtype;
            gocard2.GetComponent<CardInfo>().cardDatatype = SelectedCard.GetComponent<CardInfo>().cardDatatype;
            gocard2.GetComponent<CardInfo>().cardinformation = SelectedCard.GetComponent<CardInfo>().cardinformation;
            gocard2.GetComponent<CardInfo>().parameter = SelectedCard.GetComponent<CardInfo>().parameter;
            clickedgocard = 0;
        }
        else if(clickedgocard == 3)
        {
            gocard3.SetActive(true);
            setgocard3 = true;
            gocard3.GetComponent<CardInfo>().cardname = SelectedCard.GetComponent<CardInfo>().cardname;
            gocard3.GetComponent<CardInfo>().cardtype = SelectedCard.GetComponent<CardInfo>().cardtype;
            gocard3.GetComponent<CardInfo>().cardDatatype = SelectedCard.GetComponent<CardInfo>().cardDatatype;
            gocard3.GetComponent<CardInfo>().cardinformation = SelectedCard.GetComponent<CardInfo>().cardinformation;
            gocard3.GetComponent<CardInfo>().parameter = SelectedCard.GetComponent<CardInfo>().parameter;
            clickedgocard = 0;
        }
        
    }
    public void _GoCardControl()
    {
        if(clickedgocard == 1)
        {
            gocard1.SetActive(false);
            setgocard1 = false;
            clickedgocard = 0;
        }
        else if(clickedgocard == 2)
        {
            gocard2.SetActive(false);
            setgocard2 = false;
            clickedgocard = 0;
        }
        else if(clickedgocard == 3)
        {
            gocard3.SetActive(false);
            setgocard3 = false;
            clickedgocard = 0;
        }
    }

    public void OperateCard()
    {
        if(isfull == true)
        {
            thinkingCongding.SetTrigger("A-ha");
            
        }
        else
        {
            Debug.Log("??");
        }
    }

    private void DisactiveWindow()
    {
        windowUI.SetActive(false);
        cardUI.SetActive(false);
    }

    IEnumerator MyCo()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Delay");
    }

}

