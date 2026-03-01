using System;
using UnityEngine;

namespace Game.Components
{
    public class HealthComponent: MonoBehaviour, IDamageable
    {
        public event Action<int> OnHealthChanged;
        public event Action OnDead;
        
        public TeamType Team { get; private set; }
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        

        public void Init(ShipControllerConfig config, TeamType team)
        {
            MaxHealth = config.Health;
            CurrentHealth = config.Health;
            Team = team;
            
            OnHealthChanged?.Invoke(CurrentHealth);
        }

        public bool TakeDamage(int damage, TeamType sourceTeam)
        {
            if (sourceTeam == Team || damage <= 0 || CurrentHealth <= 0) 
                return false;
            
            CurrentHealth = Math.Clamp(CurrentHealth - damage, 0, MaxHealth);
            OnHealthChanged?.Invoke(CurrentHealth);
            
            if (CurrentHealth == 0) 
                OnDead?.Invoke();

            return true;
        }
    }
}