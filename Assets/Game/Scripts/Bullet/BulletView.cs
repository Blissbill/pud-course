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
        [SerializeField]
        private BulletViewConfig _configView;

        private IBulletAdapter _bulletAdapter;
        
        private void Awake()
        {
            // SerializeField не работает с интерфейсами
            _bulletAdapter = GetComponent<BulletAdapter>();
            _bulletAdapter.OnTeamChanged += this.OnTeamChanged;
            _bulletAdapter.OnHit += OnHit;
        }
        
        private void OnDestroy()
        {
            _bulletAdapter.OnTeamChanged -= this.OnTeamChanged;
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

        private void OnHit(Vector3 position)
        {
            GameObject prefab = _configView.ExplosionVFX;
            Instantiate(prefab, position, prefab.transform.rotation);
        }
 

    }
}