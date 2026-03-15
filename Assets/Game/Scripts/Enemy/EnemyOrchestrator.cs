using System;
using System.Collections;
using System.Collections.Generic;
using Game.Bullet;
using Game.Ship;
using Modules.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Enemy
{
    public sealed class EnemyOrchestrator : MonoBehaviour
    {
        public event Action OnDespawned;
        public event Action<EnemyShip> OnEnemySpawned;
        
        [Header("Spawn")]
        [SerializeField]
        private float _minSpawnCooldown = 2;

        [SerializeField]
        private float _maxSpawnCooldown = 3;
        
        private float _spawnCooldown;
        private float _spawnTime;
        
        [Header("Pool")]
        [SerializeField]
        private EnemyShip _prefab;

        [SerializeField]
        private Transform _container;
        
        private readonly Queue<EnemyShip> _pool = new();
        private readonly List<EnemyShip> _activeEnemies = new();
        
        [Header("Target")]
        [SerializeField]
        private ShipController _player;
        
        [Header("Points")]
        [SerializeField]
        private Transform[] _spawnPositions;
        
        [SerializeField]
        private Transform[] _attackPositions;
        
        private int _spawnIndex;
        private int _attackIndex;
        
        private bool _isActive = true;

        private void Awake()
        {
            _spawnPositions.Shuffle();
            _attackPositions.Shuffle();
            _player.OnDied += OnPlayerDied;
        }

        private void OnDestroy()
        {
            _player.OnDied -= OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _isActive = false;
            foreach (EnemyShip enemy in _activeEnemies)
                enemy.gameObject.SetActive(false);
            _activeEnemies.Clear();
        }
        
        private void Start()
        {
            this.ResetSpawnCooldown();
        }
        
        private void FixedUpdate()
        {
            float time = Time.fixedTime;
            if (time - _spawnTime < _spawnCooldown || !_isActive)
                return;
            Spawn();
            ResetSpawnCooldown();
        }

        private void Spawn()
        {
            if (_pool.TryDequeue(out EnemyShip enemy))
                enemy.gameObject.SetActive(true);
            else
                enemy = Instantiate(_prefab, _container);
            
            void OnEnemyDied()
            {
                enemy.OnDied -= OnEnemyDied;
                Despawn(enemy);
            }

            enemy.OnDied += OnEnemyDied;
            _activeEnemies.Add(enemy);

            enemy.transform.position = NextSpawnPosition();
            enemy.SetDestination(NextDestination());
            OnEnemySpawned?.Invoke(enemy);
        }
        
        private Vector3 NextSpawnPosition()
        {
            if (_spawnIndex >= _spawnPositions.Length)
            {
                _spawnPositions.Shuffle();
                _spawnIndex = 0;
            }

            return _spawnPositions[_spawnIndex++].position;
        }
        
        private Vector3 NextDestination()
        {
            if (_attackIndex >= _attackPositions.Length)
            {
                _attackPositions.Shuffle();
                _attackIndex = 0;
            }

            return _attackPositions[_attackIndex++].position;
        }
        
        private void Despawn(EnemyShip enemy)
        {
            _activeEnemies.Remove(enemy);
            OnDespawned?.Invoke();
            StartCoroutine(DespawnInNextFrame(enemy));
        }
        
        private IEnumerator DespawnInNextFrame(EnemyShip enemy)
        {
            yield return null;
            enemy.gameObject.SetActive(false);
            _pool.Enqueue(enemy);
        }
        
        private void ResetSpawnCooldown()
        {
            _spawnCooldown = Random.Range(_minSpawnCooldown, _maxSpawnCooldown);
            _spawnTime = Time.fixedTime;
        }
    }
}