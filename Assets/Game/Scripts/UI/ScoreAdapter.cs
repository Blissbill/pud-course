using System;
using Game.Enemy;
using Modules.UI;
using UnityEngine;

namespace Game.UI
{
    public sealed class ScoreAdapter : MonoBehaviour
    {
        [SerializeField]
        private ScoreView _scoreView;
        [SerializeField] 
        private EnemyOrchestrator _enemyOrchestrator;
        
        private int _destroyedEnemies;

        private void Awake()
        {
            _enemyOrchestrator.OnDespawned += HandleDespawn;
            _scoreView.SetValue(_destroyedEnemies);
        }

        private void OnDestroy()
        {
            _enemyOrchestrator.OnDespawned -= HandleDespawn;
        }

        private void HandleDespawn()
        {
            _destroyedEnemies += 1;
            _scoreView.SetValue(_destroyedEnemies);
        }
    }
}