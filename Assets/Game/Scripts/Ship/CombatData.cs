using UnityEngine;

namespace Game.Scripts.Ship
{
    public struct CombatData
    {
        public readonly Transform FirePoint;
        public readonly float BulletSpeed;
        public readonly int BulletDamage;

        public CombatData(Transform firePoint, float bulletSpeed, int bulletDamage)
        {
            FirePoint = firePoint;
            BulletSpeed = bulletSpeed;
            BulletDamage = bulletDamage;
        }
    }
}