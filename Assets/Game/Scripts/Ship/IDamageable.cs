using System;

namespace Game
{
    public interface IDamageable
    {
        event Action<int> OnHealthChanged;
        event Action OnDead;
        TeamType Team { get; }
        
        bool TakeDamage(int damage, TeamType sourceTeam);
    }
}