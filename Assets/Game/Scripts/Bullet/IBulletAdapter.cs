using System;

namespace Game
{
    public interface IBulletAdapter
    {
        event Action<TeamType> OnTeamChanged;
    }
}