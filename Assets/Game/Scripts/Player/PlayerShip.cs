using Game.Ship;
using Modules.Utils;
using UnityEngine;

namespace Game.Player
{
    public sealed class PlayerShip: ShipController
    {
        [SerializeField]
        private TransformBounds _playerArea;
        
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _combatComponent.Fire();

            float dx = Input.GetAxisRaw("Horizontal");
            float dy = Input.GetAxisRaw("Vertical");
            _movementComponent.MoveStep(new Vector2(dx, dy));
        }

        protected override void HandleDied()
        {
            base.HandleDied();
            gameObject.SetActive(false);
        }
        private void LateUpdate()
        {
            transform.position = _playerArea.ClampInBounds(transform.position);
        }
    }
}