using Game.Ship.Components;
using UnityEngine;

namespace Game.Ship
{
    public sealed class ShipControllerAdapter : MonoBehaviour
    {

        [SerializeField]
        private ShipController _shipController;
        [SerializeField]
        private ShipView _shipView;

        private void Awake()
        {
            _shipController.OnHealthChanged += OnHealthChanged;
            _shipController.OnDied += OnDied;
            _shipController.OnMoved += OnMoved;
            _shipController.OnFire += OnFire;
        }

        private void OnDestroy()
        {
            _shipController.OnHealthChanged -= OnHealthChanged;
            _shipController.OnDied -= OnDied;
            _shipController.OnMoved -= OnMoved;
            _shipController.OnFire -= OnFire;
        }
        
        private void OnFire(CombatData combatData)
        {
            _shipView.Fire(combatData);
        }
        
        private void OnMoved(Vector3 direction)
        {
            _shipView.Move(direction);
        }
        
        private void OnHealthChanged(int currentHealth)
        {
            _shipView.HealthChanged(currentHealth);
        }
        
        private void OnDied()
        {
            _shipView.Death();
        }
    }
}