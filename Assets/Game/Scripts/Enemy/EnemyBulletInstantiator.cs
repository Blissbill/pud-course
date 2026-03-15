using System;
using Game.Bullet;
using Game.Core;
using Game.Ship.Components;
using UnityEngine;

namespace Game.Enemy
{
    public sealed class EnemyBulletInstantiator : MonoBehaviour
    {
        [SerializeField]
        private BulletManager _bulletManager;
        [SerializeField] 
        private EnemyOrchestrator _enemyOrchestrator;
        [SerializeField]
        private Transform _targetPosition;

        private void Awake()
        {
            _enemyOrchestrator.OnEnemySpawned += OnEnemySpawned;
        }

        private void OnDestroy()
        {
            _enemyOrchestrator.OnEnemySpawned -= OnEnemySpawned;
        }

        private void OnEnemySpawned(EnemyShip enemy)
        {
            void OnDied()
            {
                enemy.OnFire -= OnFire;
                enemy.OnDied -= OnDied;
            }
            enemy.OnFire += OnFire;
            enemy.OnDied += OnDied;
        }

        private void OnFire(CombatData combatData)
        {
            Vector2 direction = (_targetPosition.position - combatData.FirePoint.position).normalized;
            _bulletManager.Spawn(
                combatData.FirePoint.position,
                direction,
                combatData.BulletSpeed,
                combatData.BulletDamage,
                TeamType.Enemy
                );
        }

        
    }
}