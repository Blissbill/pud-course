using Modules.UI;
using Modules.Utils;
using UnityEngine;

namespace Game.Player
{
    public class PlayerUI: MonoBehaviour
    {
        [SerializeField]
        private CameraShaker _cameraShaker;
        [SerializeField]
        private HealthView _healthView;
        
        public void HealthChanged(int currentHealth, int maxHealth)
        {
            _healthView.SetHealth(currentHealth, maxHealth);
            _cameraShaker.Shake();
        }
    }
}