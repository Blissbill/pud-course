using UnityEngine;

namespace Game.Ship.Components
{
    [CreateAssetMenu(menuName = "Game/HealthConfig")]
    public sealed class HealthConfig : ScriptableObject
    {
        [field: SerializeField] 
        public int MaxHealth { get; private set; } = 5;
    }
}
