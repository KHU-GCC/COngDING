//
//  CameraControl.cs
//  Congding
//
//  Created by Lee Suyeon on 2021/06/18.
//  Copyright (c) 2021 Will KHU-GCC. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Vector3 cameraPos; // 카메라 좌표
    GameObject congding; // 콩딩이 오브젝트
    private Vector3 congdingPos; // 콩딩이 좌표

    // 초기화
    void Awake()
    {
        congding = GameObject.Find("Congding");
    }

    void Update()
    {
        CameraMove();
    }


    //콩딩이가 카메라 기준 넌무 왼쪽 혹은 오른쪽으로 가게되면 카메라 위치를 조금 옮겨준다.
    private void CameraMove()
    {
        //매 프레임마다 카메라와 콩딩이 좌표 계산
        cameraPos = transform.position;
        congdingPos = congding.GetComponent<Transform>().position;

        //왼쪽으로 치우칠때마다 카메라 이동
        if(cameraPos.x - congdingPos.x > 5.05f)
        {
            transform.Translate(new Vector3(-0.05f, 0, 0));

            // 로그 확인
            Debug.Log("Left Camera Move");
        }
        //오른쪽으로 치우칠때마다 카메라 이동
        else if (cameraPos.x - congdingPos.x < 4.95f)
        {
            transform.Translate(new Vector3(0.05f, 0, 0));

            // 로그 확인
            Debug.Log("Right Camera Move");
        }
        
        /*
        // 위쪽으로 치우칠때마다 카메라 이동
        if(cameraPos.y - congdingPos.y > 4)
        {
            transform.Translate(new Vector3(0, -0.05f, 0));
        }
        // 아래쪽으로 치우칠때마다 카메라 이동
        else if(cameraPos.y - congdingPos.y < -3)
        {
            transform.Translate(new Vector3(0, +0.05f, 0));
        }
        */
    }
}
