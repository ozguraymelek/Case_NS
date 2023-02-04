using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerAI ai) == true)
        {
            if (ScoreManager.Instance.lastCollidedAI != ai) return;
        
            ai.Die();

            ScoreManager.Instance.score += 1;
            ScoreManager.Instance.textScore.text = ScoreManager.Instance.score.ToString();
        }
        
        if (other.TryGetComponent(out Player player) == true)
        {
            if (ScoreManager.Instance.lastCollidedAI != ai) return;
        
            player.Die();
            GameManager.Instance.losePanel.SetActive(true);
        }
    }
}
