using Game.Core;
using UnityEngine;

namespace Game.Bullet
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _blueVFX;
        [SerializeField]
        private GameObject _redVFX;
        [SerializeField] 
        private GameObject _explosionVFX;
        
        public void ChangeTeam(TeamType teamType)
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

        public void Hit(Vector3 position)
        {
            // В ParticleSystem включил уничтожение по окончанию анимации
            Instantiate(_explosionVFX, position, _explosionVFX.transform.rotation);
        }
 

    }
}