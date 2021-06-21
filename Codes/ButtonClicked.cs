//
//  ButtonClicked.cs
//  Congding
//
//  Created by Jeong SoYun on 2021/06/18.
//  Copyright (c) 2021 Will KHU-GCC. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClicked : MonoBehaviour
{
    public AudioClip tapSound; // 버튼 탭 사운드
    public AudioClip rejectSound; // 지금은 안돼 사운드
    GameObject congding; // 오늘의 게스트로 콩딩이를 모셔왔습니다
    public GameObject PauseUI; // 일시정지 UI
    
    private AudioSource ButtonAudio;
    // Start is called before the first frame update
    void Start()
    {
        ButtonAudio = GetComponent<AudioSource>();
        congding = GameObject.Find("Congding");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartButtonClicked()
    {

        // 버튼 탭 효과음 출력
        ButtonAudio.clip = tapSound;
        ButtonAudio.Play();

        // Restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    

    public void PauseButtonClicked()
    {
        // 버튼 효과음 출력
        ButtonAudio.clip = tapSound;
        ButtonAudio.Play();

        // GameManager의 일시정지 함수 호출
        GameManager.instance.OnPause();
    }

    public void ContinueButtonClicked()
    {
        // 버튼 효과음 출력
        ButtonAudio.clip = tapSound;
        ButtonAudio.Play();

        // Gamemanager의 일시정지 함수 호출
        GameManager.instance.OnPause();
    }
    
    public void GoButtonClicked()
    {
        //sound
        if(BattleManager.instance.isfull)
        {
            ButtonAudio.clip = tapSound;
            ButtonAudio.Play();
        }
        else
        {
            ButtonAudio.clip = rejectSound;
            ButtonAudio.Play();
        }

        //클릭 시 불러올 함수
        BattleManager.instance.OperateCard();

    }

    public void LeftArrowButtonClicked()
    {
        ButtonAudio.clip = tapSound;
        ButtonAudio.Play();

        BattleManager.instance.GotoLeftDeck();
    }

    public void RightArrowButtonClicked()
    {
        ButtonAudio.clip = tapSound;
        ButtonAudio.Play();

        BattleManager.instance.GotoRightDeck();
    }

    public void GameResetButtonClicked()
    {
        ButtonAudio.clip = tapSound;
        ButtonAudio.Play();
        
        SceneManager.LoadScene("OpeningScene");
    }
}
