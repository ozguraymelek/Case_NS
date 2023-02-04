using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public enum Options
{
    Bool,
    Trigger,
    ResetTrigger
}

public class AnimationManager
{
    public static void Request(Animator animator, Options ops, string parameter, bool state)
    {
        switch (ops)
        {
            case Options.Bool:
                animator.SetBool(parameter, state);
                break;
            case Options.Trigger:
                animator.SetTrigger(parameter);
                break;
            case Options.ResetTrigger:
                animator.ResetTrigger(parameter);
                break;
        }
    }
}
