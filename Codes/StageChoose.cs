//
//  StageChoose.cs
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

public class StageChoose : MonoBehaviour
{
    Vector2[] stage = new Vector2[5]; // stage들의 위치

    private GameObject congding; // 콩딩이 오브젝트
    private Animator congdingAnimator; // 사용할 콩딩이 애니메이터 컴포넌트
    
    private string selectedStage = "Tutorial"; // 선택한 스테이지 
    
    private bool move = false; // 콩딩이 이동중?
    private Vector3 target; // 콩딩이 이동 목표 좌표
    public float speed; // 콩딩이가 움직이는 속도
    private float step; 

    private void Awake()
    {
        // 튜토리얼 스테이지
        stage[0] = new Vector2(-5.5f, 0.8f);

        // 스테이지 1
        stage[1] = new Vector2(1.6f, 0.9f);

        //콩딩이 오브젝트 검색 및 사용할 컴포넌트들 연결
        congding = GameObject.Find("Congding");
        congdingAnimator = congding.GetComponent<Animator>();
    }

    private void Update()
    {
        if(move)
        {
            step = speed * Time.deltaTime;

            // 현재위치에서 목표위치로 일정속도로 이동
            congding.transform.position = Vector2.MoveTowards(congding.transform.position, target, speed);

            //콩딩이가 완쪽으로 가면
            if(target.x - congding.transform.position.x < 0)
            {
                //콩딩이 달리는 애니메이션 좌우 반전
                congdingAnimator.SetBool("Left", true);
            }
            else
            {
                congdingAnimator.SetBool("Left", false);
            }
            
            // 애니메이터의 Move 파라미터를 move값으로 갱신
            congdingAnimator.SetBool("Move", move);

            //target위치로 콩딩이가 이동하면 move끝.
            if(congding.transform.position == target)
            {
                move = false;

                // 애니메이터의 Move 파라미터를 move값으로 갱신
                congdingAnimator.SetBool("Move", move);
                
                //로그 확인
                Debug.Log("이동 완료");
            }
        }
    }

    // 튜토리얼 스테이지 버튼을 클릭했을 때
    public void Tutorial()
    {
        //로그 확인
        Debug.Log("Tutorial Clicked");

        //콩딩이가 Tutorial 스테이지 버튼 위에 있는 상태에서 튜토리얼 스테이지 버튼을 누르면 Tutorial 스테이지 실행.
        if(selectedStage == "Tutorial" && !move)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            //로그 확인
            Debug.Log("Tutorial 좌표로 이동");

            //stge[0]좌표(스테이지 버튼 좌표)로 콩딩이 이동.
            move = true;
            target = stage[0];
            selectedStage = "Tutorial";
        }
    }
    
    // 스테이지1 버튼을 클릭했을 때
    public void Stage1()
    {
        //로그 확인
        Debug.Log("Stage1 Clicked");
        
        if(selectedStage == "Stage1" && !move)
        {
            // 나중에 스테이지1을 만들면 Stage1씬이 실행되도록 한다.
            // SceneManager.LoadScene("Stage");
        }
        else
        {
            //로그 확인
            Debug.Log("Stage1 좌표로 이동");

            //stge[1]좌표(스테이지 버튼 좌표)로 콩딩이 이동.
            move = true;
            target = stage[1];
            selectedStage = "Stage1";
        }
    }

    // 스테이지가 추가...
    
}
