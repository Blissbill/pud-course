using Modules.Utils;
using UnityEngine;

namespace Game.Player
{
    public class PlayerShip: Scripts.Ship.ShipController
    {
        [SerializeField]
        private TransformBounds _playerArea;
        
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _combatComponent.Fire();

            float dx = Input.GetAxisRaw("Horizontal");
            float dy = Input.GetAxisRaw("Vertical");

            if (_healthComponent.CurrentHealth > 0)
            {
                _movementComponent.MoveStep(new Vector2(dx, dy));
            }
        }

        protected void LateUpdate()
        {
            this.transform.position = _playerArea.ClampInBounds(this.transform.position);
        }
    }
}