using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("UI Components")] [SerializeField]
    internal GameObject[] panels;
    [SerializeField] internal GameObject losePanel;
    [SerializeField] internal GameObject winPanel;
    [SerializeField] internal GameObject scorePanel;
    [SerializeField] internal GameObject gameTimerPanel;
    
    [Header("Settings")] [SerializeField] internal bool canGameStart = false;

    public void CheckWinner()
    {
        if (ScoreManager.Instance.score == 0)
        {
            Instance.losePanel.SetActive(true);
        }
        else
        {
            Instance.winPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}
