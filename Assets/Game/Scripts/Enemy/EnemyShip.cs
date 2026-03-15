using Game.Ship;
using UnityEngine;

namespace Game.Enemy
{
    public sealed class EnemyShip : ShipController
    {
        [SerializeField]
        private float _stoppingDistance = 0.25f;

        private Vector2 _destination;

        public void SetDestination(Vector2 destination) => _destination = destination;
        
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (_healthComponent.CurrentHealth <= 0)
                return;

            Vector2 distance = _destination - (Vector2) this.transform.position;
            bool isNotReached = distance.sqrMagnitude > _stoppingDistance * _stoppingDistance;

            if (isNotReached)
            {
                _movementComponent.MoveStep(distance.normalized);
            }
            else
            {
                _combatComponent.Fire();
            }
        }
    }
}