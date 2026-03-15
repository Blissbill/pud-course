using System;
using Game.Core;
using UnityEngine;

namespace Game.Ship.Components
{
    public sealed class HealthComponent : MonoBehaviour, IDamageable
    {
        public event Action<int> OnHealthChanged;
        public event Action OnDied;

        public TeamType Team { get; private set; }
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }

        [SerializeField] 
        private HealthConfig _config;
        [SerializeField] 
        private TeamType _team;

        private void Awake()
        {
            MaxHealth = _config.MaxHealth;
            CurrentHealth = _config.MaxHealth;
            Team = _team;
        }

        private void OnEnable()
        {
            CurrentHealth = MaxHealth;
            OnHealthChanged?.Invoke(CurrentHealth);
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0 || CurrentHealth <= 0)
                return;

            CurrentHealth = Math.Clamp(CurrentHealth - damage, 0, MaxHealth);
            OnHealthChanged?.Invoke(CurrentHealth);

            if (CurrentHealth == 0)
                OnDied?.Invoke();
        }
    }
}
