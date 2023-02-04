using System;
using System.Collections;
using System.Collections.Generic;
using NS_Case.Controllers;
using NS_Case.DataContainer;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAI : MonoBehaviour, ICanResponse
{
    [Header("Scriptable Objects")] [SerializeField]
    internal DataContainer dataContainer;
    
    [Header("Components")] 
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    private PlayerAI _targetAI;

    [Header("Settings")] 
    [SerializeField] private bool hasTarget = false;
    [SerializeField] private float minDist = 1000f;
    private int _currentTargetIndex;
    private bool _isDead = false;

    #region Built-in Funcs

    private void Start()
    {
        rb.mass = Random.Range(5, 100);
        
        this.UpdateAsObservable().Where(_ => hasTarget == true && AISpawnController.Instance.spawnCompleted == true
        && GameManager.Instance.canGameStart == true && GameTimer.Instance.timeIsUp == false).Subscribe(unit =>
        {
            print("1");
            Catch();
        });
        
        this.ObserveEveryValueChanged(_ => rb.velocity).Where(_ => Mathf.Abs(rb.velocity.z) < 2f).Subscribe(_ =>
        {
            print("TTEnasdsaf");
            Calculate();
        });
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.collider.TryGetComponent(out ICanResponse ai) == true)
        {
            ai.Response(hit.rigidbody);

            if (_targetAI != hit.collider.GetComponent<PlayerAI>()) return;

            hasTarget = false;
        
            minDist = 1000f;
        }
        
        if (hit.collider.TryGetComponent(out Player player) == true)
        {
            player.Response(rb);
            ScoreManager.Instance.lastCollidedAI = this;
        }
    }

    #endregion

    #region Interface Imps

    public void Response(Rigidbody collidedRb)
    {
        var force = new Vector3(0f, 0f, -100f);
        collidedRb.AddRelativeForce(force, ForceMode.Impulse);
        
        AnimationManager.Request(animator, Options.Bool, "IsHitted", true);
        AnimationManager.Request(collidedRb.transform.GetComponent<PlayerAI>().animator, Options.Bool, "IsHitted", true);
    }

    #endregion

    #region Priv Funcs

    private void Catch()
    {
        var target = AISpawnController.Instance.spawnedPlayersAI[_currentTargetIndex].transform.position;
        
        transform.position = Vector3.MoveTowards(transform.position,
            target,
            dataContainer.AiData.Speed * Time.deltaTime);
        
        transform.LookAt(target);
    }

    private void Calculate()
    {
        for (var i = 0; i < AISpawnController.Instance.spawnedPlayersAI.Count; i++)
        {
            var distance = Vector3.Distance(transform.position, AISpawnController.Instance.spawnedPlayersAI[i].transform.position);
            
            if (AISpawnController.Instance.spawnedPlayersAI[i] == this) continue;
            
            if (distance < minDist)
            {
                minDist = distance;
                AnimationManager.Request(animator, Options.Bool, "IsHitted", false);
                _currentTargetIndex = i;
                
                hasTarget = true;
            }
        }
        
        _targetAI = AISpawnController.Instance.spawnedPlayersAI[_currentTargetIndex];
    }

    internal void Die()
    {
        gameObject.SetActive(false);
    }
    public void AnimationEventReset()
    {
        AnimationManager.Request(animator, Options.Bool, "IsHitted", false);
    }
    
    #endregion

    
}
