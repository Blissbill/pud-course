using Game.Core;
using UnityEngine;

namespace Game.Bullet
{
    public sealed class BulletAdapter : MonoBehaviour
    {
        [SerializeField]
        private Bullet _bullet;

        [SerializeField]
        private BulletView _bulletView;

        private void Awake()
        {
            _bullet.OnTeamChanged += OnTeamChanged;
            _bullet.OnHit += OnHit;
        }

        private void OnDestroy()
        {
            _bullet.OnTeamChanged -= OnTeamChanged;
            _bullet.OnHit -= OnHit;
        }
        
        private void OnTeamChanged(TeamType team)
        {
            _bulletView.ChangeTeam(team);
        }

        private void OnHit(Vector3 position)
        {
            _bulletView.Hit(position);
        }
    }
}