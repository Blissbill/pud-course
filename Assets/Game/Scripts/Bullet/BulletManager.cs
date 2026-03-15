using System;
using System.Collections.Generic;
using Game.Core;
using Game.Ship.Components;
using Modules.Utils;
using UnityEngine;

namespace Game.Bullet
{
    public sealed class BulletManager : MonoBehaviour
    {
        [SerializeField]
        private BulletFactory _factory;
        
        [SerializeField]
        private TransformBounds _levelBounds;
        
        [SerializeField] 
        private int _initialBulletCount = 10;
        
        private readonly Stack<Bullet> _pool = new();
        private readonly List<Bullet> _bullets = new();
        
        private void Awake()
        {
            for (var i = 0; i < _initialBulletCount; i++)
            {
                Bullet bullet = _factory.CreateBullet();
                bullet.gameObject.SetActive(false);
                _pool.Push(bullet);
            }
        }
        
        private void FixedUpdate()
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = _bullets[i];
                bullet.Move(Time.fixedDeltaTime);
                
                if (!_levelBounds.InBounds(bullet.Position))
                {
                    _bullets.RemoveAt(i);
                
                    bullet.OnTriggerEntered -= this.OnTriggerEntered;
                    bullet.gameObject.SetActive(false);
                    _pool.Push(bullet);
                }
            }
        }
        
        public void Spawn(Vector2 position, Vector2 direction, float speed, int damage, TeamType team)
        {
            if (_pool.TryPop(out Bullet bullet))
                bullet.gameObject.SetActive(true);
            else
                bullet = _factory.CreateBullet();
            int layer = team switch
            {
                TeamType.None => LayerIds.Default.Value,
                TeamType.Player => LayerIds.PlayerBullet.Value,
                TeamType.Enemy => LayerIds.EnemyBullet.Value,
                _ => throw new ArgumentOutOfRangeException(nameof(team), team, null)
            };
            bullet.OnTriggerEntered += this.OnTriggerEntered;
            bullet.
                Clear().
                WithDirection(direction).
                WithSpeed(speed).
                WithDamage(damage).
                WithPosition(position).
                WithRotation(Quaternion.LookRotation(direction, Vector3.forward)).
                WithLayer(layer).
                WithTeam(team);
            _bullets.Add(bullet);
        }
        
        private void OnTriggerEntered(Bullet bullet, Collider2D other)
        {
            if (!other.TryGetComponent(out IDamageable damageable))
                return;
            if (bullet.Team == damageable.Team) 
                return;

            damageable.TakeDamage(bullet.Damage);
            bullet.OnTriggerEntered -= OnTriggerEntered;
            _bullets.Remove(bullet);
            bullet.Hit();
            _pool.Push(bullet);
        }
    }
}