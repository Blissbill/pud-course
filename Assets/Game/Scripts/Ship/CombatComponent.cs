using System;
using UnityEngine;

namespace Game.Scripts.Ship
{
    public class CombatComponent: MonoBehaviour
    {
        public event Action<CombatData> OnFire;
        
        [SerializeField]
        private Transform _firePoint;
        [SerializeField]
        private float _bulletSpeed;
        [SerializeField]
        private int _bulletDamage;
        [SerializeField]
        private float _fireTime;

        private float _fireCooldown;

        public void Init(ShipControllerConfig config)
        {
            _fireCooldown = config.FireCooldown;
        }
        
        public void Fire()
        {
            float time = Time.time;
            if (time - _fireTime < _fireCooldown)
                return;

            OnFire?.Invoke(new CombatData(_firePoint, _bulletSpeed, _bulletDamage));
            _fireTime = time;
        }
    }
}