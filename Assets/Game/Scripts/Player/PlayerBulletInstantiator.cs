using Game.Bullet;
using Game.Core;
using Game.Ship.Components;
using UnityEngine;

namespace Game.Player
{
    // +
    public sealed class PlayerBulletInstantiator : MonoBehaviour
    {
        [SerializeField]
        private BulletManager _bulletManager;

        [SerializeField]
        private PlayerShip _player;

        private void OnEnable()
        {
            _player.OnFire += OnFire;
        }

        private void OnDisable()
        {
            _player.OnFire -= OnFire;
        }

        private void OnFire(CombatData combatData)
        {
            _bulletManager.Spawn(
                combatData.FirePoint.position,
                combatData.FirePoint.up,
                combatData.BulletSpeed,
                combatData.BulletDamage,
                TeamType.Player
            );
        }
    }
}