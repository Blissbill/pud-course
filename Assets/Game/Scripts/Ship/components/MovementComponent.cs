using System;
using UnityEngine;

namespace Game.Ship.Components
{
    public sealed class MovementComponent : MonoBehaviour
    {
        public event Action<Vector3> OnMoved;

        [SerializeField] 
        private Motor _motor;
        [SerializeField] 
        private MovementConfig _config;

        private Vector3 _moveDirection;

        private void Awake()
        {
            _motor.SetSpeed(_config.MoveSpeed);
        }

        public void MoveStep(Vector2 direction)
        {
            _moveDirection = direction;
            _motor.MoveStep(direction);
        }

        public void Move()
        {
            _motor.FixedUpdate();
            OnMoved?.Invoke(_moveDirection);
        }
    }
}
