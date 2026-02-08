using UnityEngine;

namespace Game
{
    public class BulletFactory: MonoBehaviour
    {
        [SerializeField]
        private BulletData _prefab;

        [SerializeField]
        private Transform _container;

        public BulletData CreateBullet()
        {
            return Instantiate(_prefab, _container);
        }
    }
}