using System;
using UnityEngine;

namespace Game
{
    public class BulletAdapter: MonoBehaviour, IBulletAdapter
    {
        [SerializeField]
        private Bullet _bullet;

        public event Action<TeamType> OnTeamChanged;

        public void Awake()
        {
            _bullet.OnTeamChanged += HandleTeamChanged;
        }
        
        private void HandleTeamChanged(TeamType team)
        {
            OnTeamChanged?.Invoke(team);
        }
        
        private void OnDestroy()
        {
            _bullet.OnTeamChanged -= HandleTeamChanged;
        }
    }
}