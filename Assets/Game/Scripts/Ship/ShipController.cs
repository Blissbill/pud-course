using System;
using Game.Components;
using UnityEngine;

namespace Game.Scripts.Ship
{
    public abstract class ShipController: MonoBehaviour
    {   
        public event Action<int> OnHealthChanged;
        public event Action OnDead;
        public event Action<Vector3> OnMove;
        public event Action<CombatData> OnFire;
        public int MaxHealth => _healthComponent.MaxHealth;
        public int CurrentHealth => _healthComponent.CurrentHealth;
        
        [SerializeField] 
        protected HealthComponent _healthComponent;
        [SerializeField]
        protected CombatComponent _combatComponent;
        [SerializeField]
        protected MovementComponent _movementComponent;
        [SerializeField]
        protected ShipControllerConfig _config;
        [SerializeField] 
        protected TeamType _team = TeamType.None;
        

        private void Awake()
        {
            _healthComponent.Init(_config, _team);
            _combatComponent.Init(_config);
            _movementComponent.Init(_config, Vector3.zero);
            _healthComponent.OnHealthChanged += HandleHealthChanged;
            _healthComponent.OnDead += HandleDeath;
            _movementComponent.OnMove += HandleMove;
            _combatComponent.OnFire += HandleFire;
        }

        private void OnDestroy()
        {
            _healthComponent.OnHealthChanged -= HandleHealthChanged;
            _healthComponent.OnDead -= HandleDeath;
            _movementComponent.OnMove -= HandleMove;
            _combatComponent.OnFire -= HandleFire;
        }

        private void HandleFire(CombatData combatData)
        {
            OnFire?.Invoke(combatData);
        }
        
        private void HandleMove(Vector3 direction)
        {
            OnMove?.Invoke(direction);
        }
        
        private void HandleHealthChanged(int currentHealth)
        {
            OnHealthChanged?.Invoke(currentHealth);
        }

        private void HandleDeath()
        {
            OnDead?.Invoke();
        }

        protected virtual void FixedUpdate()
        {
            _movementComponent.Move();
        }
    }
}