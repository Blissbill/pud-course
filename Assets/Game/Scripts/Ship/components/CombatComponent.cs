using System;
using UnityEngine;

namespace Game.Ship.Components
{
    public sealed class CombatComponent : MonoBehaviour
    {
        public event Action<CombatData> OnFire;

        [SerializeField] 
        private Transform _firePoint;
        [SerializeField] 
        private CombatConfig _config;

        private float _fireTime;

        public void Fire()
        {
            float time = Time.time;
            if (time - _fireTime < _config.FireCooldown)
                return;

            OnFire?.Invoke(new CombatData(_firePoint, _config.BulletSpeed, _config.BulletDamage));
            _fireTime = time;
        }
    }
}
