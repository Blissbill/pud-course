using System;
using Game.Core;

namespace Game.Ship.Components
{
    public interface IDamageable
    {
        event Action<int> OnHealthChanged;
        event Action OnDied;
        TeamType Team { get; }
        
        void TakeDamage(int damage);
    }
}