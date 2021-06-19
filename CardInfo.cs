//
//  CardInfo.cs
//  Congding
//
//  Created by Jeong So Yun on 2021/06/18.
//  Copyright (c) 2021 Will KHU-GCC. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour
{
    public string cardname; // 카드이름
    public int cardtype; // 1 = move 2 = attack
    public int cardDatatype; // 1 = void , 2 = int, 3 = string, 4 = float
    public string cardDT;
    public GameObject cardsprite; // 카드이미지
    public string cardinformation; // 카드의 신 설명충
    public int parameter; // 값

    public Sprite attackcard_sprite;
    public Sprite movecard_sprite;
    public Text cardnametxt;
    public Text cardtypetxt;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cardDatatype == 1)
        {
            cardDT = "void";
        }
        else if(cardDatatype == 2)
        {
            cardDT = "int";
        }
        else if(cardDatatype == 3)
        {
            cardDT = "string";
        }
        else if(cardDatatype == 4)
        {
            cardDT = "float";
        }
        
        RenderCard();
    }

    public void RenderCard()
    {
        if(cardtype == 1) //move 카드인 경우
        {
            //이미지 출력
            cardsprite.GetComponent<SpriteRenderer>().sprite = movecard_sprite;
            //카드 이름 출력
            cardnametxt.text = cardname;
            //카드 자료형 출력
            cardtypetxt.text = cardDT;

        }
        else if(cardtype == 2)
        {
            //이미지 출력
            cardsprite.GetComponent<SpriteRenderer>().sprite = attackcard_sprite;
            //카드 이름 출력
            cardnametxt.text = cardname;
            //카드 자료형 출력
            cardtypetxt.text = cardDT;
        }
        
    }

    
}
