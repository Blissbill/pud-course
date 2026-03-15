using UnityEngine;

namespace Game.Bullet
{
    public sealed class BulletFactory : MonoBehaviour
    {
        [SerializeField]
        private Bullet _prefab;

        [SerializeField]
        private Transform _container;

        public Bullet CreateBullet()
        {
            return Instantiate(_prefab, _container);
        }
    }
}