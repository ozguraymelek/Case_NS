using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static Action OnGameStarted, OnGameFinished, OnGamePaused, OnTimerFinished;

    private void Awake()
    {
        ResetEvents();
    }

    private void ResetEvents()
    {
        OnGameStarted = null;
        OnGameFinished = null;
        OnGamePaused = null;
        OnTimerFinished = null;
    }
}
