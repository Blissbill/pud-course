using System;
using UnityEngine;

namespace Game
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _blueVFX;
        [SerializeField]
        private GameObject _redVFX;

        private IBulletAdapter _bulletAdapter;
        
        public void Awake()
        {
            // SerializeField не работает с интерфейсами
            _bulletAdapter = GetComponent<BulletAdapter>();
            _bulletAdapter.OnTeamChanged += this.OnTeamChanged;
        }

        private void OnTeamChanged(TeamType teamType)
        {
            if (teamType == TeamType.None)
            {
                _blueVFX.SetActive(false);
                _redVFX.SetActive(false);
            } else if (teamType == TeamType.Player)
            {
                _blueVFX.SetActive(true);
                _redVFX.SetActive(false);
            }
            else
            {
                _blueVFX.SetActive(false);
                _redVFX.SetActive(true);
            }
        }

        public void OnDestroy()
        {
            _bulletAdapter.OnTeamChanged -= this.OnTeamChanged;
        }
    }
}