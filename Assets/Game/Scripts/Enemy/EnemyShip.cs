using Game.Scripts.Ship;
using UnityEngine;

namespace Game
{
    public class EnemyShip: ShipController
    {
        [Header("Enemy")]
        private ShipController _target;
        [SerializeField]
        private float _fireCooldown = 1.25f;
        [SerializeField]
        private float _stoppingDistance = 0.25f;

        private float _fireTime;
        private Vector2 _destination;
        
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (_healthComponent.CurrentHealth <= 0 || _target == null || _target.CurrentHealth <= 0)
                return;

            Vector2 distance = _destination - (Vector2) this.transform.position;
            bool isNotReached = distance.sqrMagnitude > _stoppingDistance * _stoppingDistance;

            if (isNotReached)
            {
                _movementComponent.MoveStep(distance.normalized);
            }
            else
            {
                float time = Time.time;
                if (time - _fireTime >= _fireCooldown)
                {
                    _combatComponent.Fire();
                    _fireTime = time;
                }
            }
        }
    }
}