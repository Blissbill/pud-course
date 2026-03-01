using System;
using UnityEngine;

namespace Game.Scripts.Ship
{
    public interface IShipControllerAdapter
    {
        public event Action<int, int> OnHealthChanged;
        public event Action OnDead;
        public event Action<Vector3> OnMove;
        public event Action<CombatData> OnFire;
    }
}