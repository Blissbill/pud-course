
using Game.Ship;
using UnityEngine;

namespace Game.Player
{
    public sealed class PlayerUIAdapter : MonoBehaviour
    {
        [SerializeField]
        private PlayerShip  _player;
        [SerializeField]
        private PlayerUI _playerUI;

        private void Awake()
        {
            _player.OnHealthChanged += OnHealthChanged;
        }
        
        private void OnDestroy()
        {
            _player.OnHealthChanged -= OnHealthChanged;
        }
        
        private void OnHealthChanged(int currentHealth)
        {
            _playerUI.HealthChanged(currentHealth, _player.MaxHealth);
        }
    }
}