using System;
using UnityEngine;

namespace Game
{
    public class BulletAdapter: MonoBehaviour, IBulletAdapter
    {
        [SerializeField]
        private Bullet _bullet;

        public event Action<TeamType> OnTeamChanged;
        public event Action<Vector3> OnHit;

        private void Awake()
        {
            _bullet.OnTeamChanged += HandleTeamChanged;
            _bullet.OnHit += HandleHit;
        }
        
        private void OnDestroy()
        {
            _bullet.OnTeamChanged -= HandleTeamChanged;
            _bullet.OnHit -= HandleHit;
        }
        
        private void HandleTeamChanged(TeamType team)
        {
            OnTeamChanged?.Invoke(team);
        }

        private void HandleHit(Vector3 position)
        {
            OnHit?.Invoke(position);
        }
    }
}