using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NS_Case.Controllers
{
    public class AISpawnController : Singleton<AISpawnController>
    {
        [Header("Components")] 
        [SerializeField] private PlayerAI playerAiPrefab;
        [SerializeField] private Player playerPrefab;
        [SerializeField] private CinemachineVirtualCamera playerCamera;
        
        [SerializeField] private Transform floor;

        [Header("Settings")] 
        [SerializeField] internal List<PlayerAI> spawnedPlayersAI;
        [SerializeField] internal Player spawnedPlayer;
        [SerializeField] internal int spawnCount;
        [SerializeField] internal bool spawnCompleted = false;

        #region Built-in Funcs

        private void Start()
        {
            SpawnPlayerAI();
        }

        #endregion

        #region Priv Funcs

        private void SpawnPlayerAI()
        {
            for (var i = 0; i < spawnCount; i++)
            {
                var spawnPos = RandomSpawnPoint(floor, Random.Range(-8.5f, 8.5f));

                var spawnedAI = Instantiate(playerAiPrefab, spawnPos, Quaternion.identity);

                spawnedAI.name = "AI " + i;

                spawnedPlayersAI.Add(spawnedAI);
            }
            
            var spawnPlayerPos = RandomSpawnPoint(floor, Random.Range(-8.5f, 8.5f));

            var spawnedPlayerHolder = Instantiate(playerPrefab, spawnPlayerPos, Quaternion.identity);

            spawnedPlayer = spawnedPlayerHolder;

            playerCamera.Follow = spawnedPlayer.transform;
            spawnedPlayer.joystick = FindObjectOfType<FloatingJoystick>();
            
            spawnCompleted = true;
        }

        private Vector3 RandomSpawnPoint(Transform center, float radius)
        {
            float angle = Random.value * 360;
            
            Vector3 pos;

            pos.x = center.transform.position.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            pos.y = 0f;
            pos.z = center.transform.position.z + radius * Mathf.Cos(angle * Mathf.Deg2Rad);

            return pos;
        }

        #endregion

    }
}