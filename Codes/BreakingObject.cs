//
//  BreakingObject.cs
//  Congding
//
//  Created by Lee Suyeon on 2021/06/18.
//  Copyright (c) 2021 Will KHU-GCC. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingObject : MonoBehaviour
{
    public bool isBreaked; // object가 부서졌는지 아닌지 나타내는 bool 변수
    public bool isTimer; // 타이머가 돌아가고 있는가.
    private float timerTime; // 타이머 함수에 들어갈 타임값
    public float destroyTime; // 물체를 몇 초 후에 없앨건지 재는 시간값.
    private bool timeUp = false; // 카운트 종료

    public GameObject effect; // 장애물 파괴 시 효과 애니메이션 이펙트
    private Animator animator; // 사용할 애니메이터 컴포넌트
    private Vector3 vector; // 위치

    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        Break();

        // 장애물 위치 가져오기
        vector = transform.position;

        if(timeUp)
        {
            Destroy(gameObject);
        }
    }

    // 시간을 재서 오브젝트를 Destroy한다.
    private void Break()
    {
        if(isBreaked && !isTimer)
        {
            // 타이머 재생
            Timer(destroyTime);

            // 부서지는 애니메이션 재생
            animator.SetTrigger("Breaked");

            // 부서지는 이펙트 재생
            Instantiate(effect, vector, Quaternion.Euler(Vector3.zero));
        }
        else if(isBreaked && isTimer)
        {
            TimeCounter();
        }
    }

    //타이머 함수
    // 파라메터로 타이머 설정할 시간을 받는다.
    private void Timer(float time)
    {
        timerTime = time;
        isTimer = true;
    }

    //타이머 카운트 함수
    private void TimeCounter()
    {
        if(timerTime > 0)
        {
            timerTime -= Time.deltaTime;
        }
        else
        {
            isTimer = false;
            
            // 타이머 종료
            timeUp = true;
        }
    }
}
