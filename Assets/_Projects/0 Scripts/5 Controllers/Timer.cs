using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Serialization;

public class Timer : MonoBehaviour
{
   [Header("Settings")] [SerializeField] internal float time;

   [Header("Components")] [SerializeField]
   internal TMP_Text textTime;

   #region Built-in Funcs

   private void Start()
   {
      this.UpdateAsObservable().Where(_ => time > 0).Subscribe(unit =>
      {
         time -= Time.deltaTime;
         
         DecreaseTime(time);
      }).ObserveEveryValueChanged(_ => time).Where(_ => time < 0).Subscribe(unit =>
      {
         DOVirtual.DelayedCall(1f, () =>
         {
            var instanceGameManager = GameManager.Instance;
            
            textTime.text = "GOOO";
            
            var sequence = DOTween.Sequence();
            
            sequence.Append(instanceGameManager.panels[0].transform.DOLocalMoveY(1150, 1f));
            sequence.Append(instanceGameManager.panels[1].transform.DOLocalMoveY(-1150, 1f));

            sequence.OnComplete(() =>
            {
               instanceGameManager.panels[0].SetActive(false);
               instanceGameManager.panels[1].SetActive(false);
            });
            
            transform.DOScale(new Vector3(0f,0f,0f), 1f).OnComplete(() =>
            {
               instanceGameManager.gameTimerPanel.transform.DOLocalMoveX(440f, .5f);
               instanceGameManager.scorePanel.transform.DOLocalMoveY(784.2f, .5f);
               
               GameManager.Instance.canGameStart = true;
               gameObject.SetActive(false);
            });

         });
      });

      this.ObserveEveryValueChanged(_ => time).Where(_ => time > 0).Subscribe(unit =>
      {
         DOVirtual.Float(95, 120, 1f, v => textTime.fontSize = v).OnStepComplete(() =>
         {
            textTime.fontSize = 95f;
         });
      });
   }

   #endregion

   #region Priv Funcs

   private void DecreaseTime(float timeDisp)
   {
      timeDisp += 1;
      
      float seconds = Mathf.FloorToInt(timeDisp % 60);

      textTime.text = seconds.ToString();

      

   }

   #endregion
}