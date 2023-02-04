using System;
using System.Collections;
using System.Collections.Generic;
using NS_Case.Controllers;
using TMPro;
using UniRx;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [Header("Player Settings")]
    [SerializeField] internal int score;

    [Header("Flow Settings")] [SerializeField]
    internal PlayerAI lastCollidedAI;
    
    [Header("Components")]
    [SerializeField] internal TMP_Text textScore;

    private void Start()
    {
        this.ObserveEveryValueChanged(_ => score).Where(_ => score == AISpawnController.Instance.spawnCount).Subscribe(
            unit =>
            {
                GameManager.Instance.canGameStart = false;
                GameManager.Instance.winPanel.SetActive(true);
            });
    }
}
