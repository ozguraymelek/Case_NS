using System;
using System.Collections;
using System.Collections.Generic;
using NS_Case.DataContainer.Dataset;
using UnityEngine;

namespace NS_Case.DataContainer
{
    [CreateAssetMenu(fileName = "new Data",menuName = "S.Objects/Data/Create a new Data")]
    public class DataContainer : ScriptableObject
    {
        [Header("Data [Structs]")] 
        [SerializeField] internal AIData AiData;
        [SerializeField] internal PlayerData playerData;
    }

    
}

namespace NS_Case.DataContainer.Dataset
{
    [Serializable]
    public struct AIData
    {
        [Header("Settings")]
        [SerializeField] internal float Speed;
        [SerializeField] internal float Strength;
    }
    
    [Serializable]
    public struct PlayerData
    {
        [Header("Settings")]
        [SerializeField] internal float Speed;
        [SerializeField] internal float RotationSpeed;
        [SerializeField] internal float Strength;
    }
}

