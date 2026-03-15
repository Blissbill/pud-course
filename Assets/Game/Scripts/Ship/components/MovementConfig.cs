using UnityEngine;

namespace Game.Ship.Components
{
    [CreateAssetMenu(menuName = "Game/MovementConfig")]
    public sealed class MovementConfig : ScriptableObject
    {
        [field: SerializeField] 
        public float MoveSpeed { get; private set; } = 5f;
    }
}
