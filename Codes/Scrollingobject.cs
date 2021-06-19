//
//  Scrollingobject.cs
//  Congding
//
//  Created by Lee Suyeon on 2021/06/18.
//  Copyright (c) 2021 Will KHU-GCC. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임 오브젝트를 콩딩의 속도에 맞춰 계속 왼쪽으로 움직이는 스크립트
public class Scrollingobject : MonoBehaviour
{
    private GameObject congding; //배경이 돌아갈 속도를 가져올 콩딩이 오브젝트
    private bool isCongdingDash = false; // 콩딩이가 대쉬 중인가요?
    private float speed; // 콩딩이가 달리는 스피드값, 이게 배경이 돌아갈 스피드 값이 되기도 한다.

    //씬이 실행되는 처음 한번만 실행됨.(초기화)
    void Awake()
    {
        //Congding 오브젝트 검색
        congding = GameObject.Find("Congding");
    }

    private void Update()
    {
        // 콩딩이가 대쉬중인지 아닌지의 정보를 내놔라.
        isCongdingDash = congding.GetComponent<CongdingController>().isDash;

        // 콩딩이가 대쉬 중이라면 배경 돌아가는 속도도 대쉬속도로 설정한다.
        if(isCongdingDash)
        {
            speed = congding.GetComponent<CongdingController>().dashSpeed;
        }
        else
        {
            speed = congding.GetComponent<CongdingController>().speed;
        }

        // 게임 오버 및 일시정지 상태가 아니라면
        if(!GameManager.instance.isGameover && !GameManager.instance.isPause)
        {
            // 게임 오브젝트를 speed속도로 왼쪽으로 평행이동하는 처리.
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
