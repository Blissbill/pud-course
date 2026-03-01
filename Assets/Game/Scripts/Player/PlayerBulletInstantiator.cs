using Game.Player;
using Game.Scripts.Ship;
using UnityEngine;

namespace Game
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
            _player.OnFire += this.OnFire;
        }

        private void OnDisable()
        {
            _player.OnFire -= this.OnFire;
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