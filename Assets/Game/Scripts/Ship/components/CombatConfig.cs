using UnityEngine;

namespace Game.Ship.Components
{
    [CreateAssetMenu(menuName = "Game/CombatConfig")]
    public sealed class CombatConfig : ScriptableObject
    {
        [field: SerializeField] 
        public float FireCooldown { get; private set; } = 0.25f;
        [field: SerializeField] 
        public float BulletSpeed { get; private set; } = 10f;
        [field: SerializeField] 
        public int BulletDamage { get; private set; } = 1;
    }
}
