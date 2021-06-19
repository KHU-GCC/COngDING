//
//  CardSystem.cs
//  Congding
//
//  Created by Jeong So Yun on 2021/06/18.
//  Copyright (c) 2021 Will KHU-GCC. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardSystem : MonoBehaviour
{
    
    private AudioSource CardAudio;
    public AudioClip Clicksound;
    
    public GameObject Card; // 칸 안에 있는 카드

    // Start is called before the first frame update
    void Start()
    {
        CardAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Card.GetComponent<CardInfo>().RenderCard();
    }

    public void CardClicked (int number) 
    {
        BattleManager.instance.isclicked = true;
        BattleManager.instance.clickedcard = number;
        BattleManager.instance.SelectedCard = Card;
        BattleManager.instance.SelectedCardControl();

        CardAudio.clip = Clicksound;
        CardAudio.Play();
    }

    public void EmptyClicked (int number)
    {
        if(BattleManager.instance.isclicked == true)
        {
            BattleManager.instance.isclicked = false;
            BattleManager.instance.clickedgocard = number;
            BattleManager.instance.GoCardControl();

            CardAudio.clip = Clicksound;
            CardAudio.Play();
        }
        else
        {
            BattleManager.instance.isclicked = true;
            BattleManager.instance.clickedgocard = number;
            BattleManager.instance._GoCardControl();

            CardAudio.clip = Clicksound;
            CardAudio.Play();
        }
    }

    
}
