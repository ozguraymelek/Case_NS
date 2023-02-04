using System;
using System.Collections;
using System.Collections.Generic;
using NS_Case.DataContainer;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Scriptable Objects")] [SerializeField]
    private DataContainer container;
    
    [Header("Components")] [SerializeField]
    internal FloatingJoystick joystick;
    [SerializeField] private Animator animator;
    [SerializeField] internal Rigidbody rb;

    [Header("Settings")] 
    private Vector3 _firstPos;
    private Vector3 _tempPos;
    private Vector3 _lastPos;

    private float _add;
    private void Start()
    {
        this.UpdateAsObservable().Where(_ => GameManager.Instance.canGameStart == true && GameTimer.Instance.timeIsUp == false).Subscribe(unit =>
        {
            var direction = ((Vector3.forward * joystick.Vertical) + (Vector3.right * joystick.Horizontal)).normalized;
            
            transform.position += transform.forward * (container.playerData.Speed * Time.deltaTime);
            animator.enabled = true;
            
            if (direction != Vector3.zero)
            {
                var rotGoal = Quaternion.LookRotation(direction);
                AnimationManager.Request(animator, Options.Bool, "IsHitted", false);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, rotGoal, container.playerData.RotationSpeed * Time.deltaTime);
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        });
    }

    public void AnimationEventReset()
    {
        AnimationManager.Request(animator, Options.Bool, "IsHitted", false);
    }
    
    internal void Die()
    {
        gameObject.SetActive(false);
    }
    
    public void Response(Rigidbody collidedRb)
    {
        var force = new Vector3(0f, 0f, -100f);
        collidedRb.AddRelativeForce(force, ForceMode.Impulse);

        AnimationManager.Request(animator, Options.Bool, "IsHitted", true);
    }
}
