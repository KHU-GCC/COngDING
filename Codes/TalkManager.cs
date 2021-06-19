//
//  TalkManager.cs
//  Congding
//
//  Created by Lee Suyeon on 2021/06/18.
//  Copyright (c) 2021 Will KHU-GCC. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    // 대화를 저장할 데이터 형식
    public Dictionary<string, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<string, string[]>();
        GenerateData();

    }

    // 대사 생성
    void GenerateData()
    {
        talkData.Add("Talk1", new string[] {"안녕? 나는 콩딩이라고 해!", "나는 다른 콩들과 같이 콩깍지 속에서 평화롭게 살고있었어.", "그런데 그만 버그마녀가 만든 회오리 바람에 의해 친구들이 다 날라가버렸어!", "친구들이 날 도와줘!", "난 지금 int타입이야! int타입의 장애물이 아니면 부술 수 없어~", "대쉬를 통해서 장애물을 부수면서 나아가도록하자!", "부술 수 있는 장애물 위에는 숫자나 글자가 쓰여있어!"});    
    }


    // Talk를 가져오는 함수
    public string GetTalk(string name, int talkIndex)
    {
        if(talkIndex == talkData[name].Length)
        {
            return null;
        }
        else
        {
            return talkData[name][talkIndex];
        }
    }

    // 딕셔너리의 이름 ex) "Talk1" 
    // 해당 Talk의 대화 길이를 리턴한다.
    public int GetTalkLen(string name)
    {
        return talkData[name].Length;
    }
}
