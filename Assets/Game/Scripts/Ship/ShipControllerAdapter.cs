using System;
using UnityEngine;

namespace Game.Scripts.Ship
{
    public class ShipControllerAdapter: MonoBehaviour, IShipControllerAdapter
    {
        public event Action<int, int> OnHealthChanged;
        public event Action OnDead;
        public event Action<Vector3> OnMove;
        public event Action<CombatData> OnFire;

        [SerializeField]
        private ShipController _shipController;

        private void Awake()
        {
            _shipController.OnHealthChanged += HandleHealthChange;
            _shipController.OnDead += HandleDeath;
            _shipController.OnMove += HandleMove;
            _shipController.OnFire += HandleFire;
        }

        private void OnDestroy()
        {
            _shipController.OnHealthChanged -= HandleHealthChange;
            _shipController.OnDead -= HandleDeath;
            _shipController.OnMove -= HandleMove;
            _shipController.OnFire -= HandleFire;
        }
        
        private void HandleFire(CombatData combatData)
        {
            OnFire?.Invoke(combatData);
        }
        
        private void HandleMove(Vector3 direction)
        {
            OnMove?.Invoke(direction);
        }
        
        private void HandleHealthChange(int currentHealth)
        {
            OnHealthChanged?.Invoke(currentHealth, _shipController.MaxHealth);
        }
        
        private void HandleDeath()
        {
            OnDead?.Invoke();
        }
    }
}