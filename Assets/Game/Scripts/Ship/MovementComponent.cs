using System;
using UnityEngine;

namespace Game.Scripts.Ship
{
    public class MovementComponent: MonoBehaviour
    {
        public event Action<Vector3> OnMove;
        [SerializeField]
        private Motor _motor;
        
        private Vector3 _moveDirection;

        public void Init(ShipControllerConfig config, Vector3 moveDirection)
        {   
            _motor.SetSpeed(config.MoveSpeed);
            _moveDirection = moveDirection;
        }

        public void MoveStep(Vector2 direction)
        {
            _moveDirection = direction;
            _motor.MoveStep(direction);
        }

        public void Move()
        {
            _motor.FixedUpdate();
            OnMove?.Invoke(_moveDirection);
        }
    }
}