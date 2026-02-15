using System;
using UnityEngine;

namespace Game
{
    public interface IBulletAdapter
    {
        event Action<TeamType> OnTeamChanged;
        event Action<Vector3> OnHit;
    }
}