using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : Singleton<GameTimer>
{
    [Header("Settings")] 
    [SerializeField] internal float gameTimeRemaining;
    [SerializeField] internal bool timeIsUp = false;
    
    [Header("Components")] [SerializeField]
    internal TMP_Text textGameTime;

    private void Update()
    {
        if (GameManager.Instance.canGameStart == true)
        {
            if (gameTimeRemaining > 0)
            {
                gameTimeRemaining -= Time.deltaTime;
                Display(gameTimeRemaining);
            }
            else
            {
                gameTimeRemaining = 0;
                timeIsUp = true;

                GameManager.Instance.CheckWinner();
            }
        }
    }

    private void Display(float timeToDisplay)
    {
        timeToDisplay += 1;

        float min = Mathf.FloorToInt(timeToDisplay / 60);
        float sec = Mathf.FloorToInt(timeToDisplay % 60);

        textGameTime.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
