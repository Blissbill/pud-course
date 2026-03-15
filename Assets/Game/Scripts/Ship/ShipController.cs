using System;
using Game.Core;
using Game.Ship.Components;
using UnityEngine;

namespace Game.Ship
{
    public abstract class ShipController : MonoBehaviour
    {
        public event Action<int> OnHealthChanged;
        public event Action OnDied;
        public event Action<Vector3> OnMoved;
        public event Action<CombatData> OnFire;

        public int MaxHealth => _healthComponent.MaxHealth;
        public int CurrentHealth => _healthComponent.CurrentHealth;

        [SerializeField] protected HealthComponent _healthComponent;
        [SerializeField] protected CombatComponent _combatComponent;
        [SerializeField] protected MovementComponent _movementComponent;

        private void Awake()
        {
            _healthComponent.OnHealthChanged += HandleHealthChanged;
            _healthComponent.OnDied += HandleDied;
            _movementComponent.OnMoved += HandleMoved;
            _combatComponent.OnFire += HandleFire;
        }

        private void OnDestroy()
        {
            _healthComponent.OnHealthChanged -= HandleHealthChanged;
            _healthComponent.OnDied -= HandleDied;
            _movementComponent.OnMoved -= HandleMoved;
            _combatComponent.OnFire -= HandleFire;
        }

        private void HandleFire(CombatData combatData) => OnFire?.Invoke(combatData);
        private void HandleMoved(Vector3 direction) => OnMoved?.Invoke(direction);
        private void HandleHealthChanged(int currentHealth) => OnHealthChanged?.Invoke(currentHealth);
        protected virtual void HandleDied() => OnDied?.Invoke();

        protected virtual void FixedUpdate()
        {
            _movementComponent.Move();
        }
    }
}
