using System;
using Game.Scripts.Ship;
using Modules.UI;
using Modules.Utils;
using UnityEngine;

namespace Game.Player
{
    public class PlayerShipPresentation: MonoBehaviour
    {
        [SerializeField]
        private CameraShaker _cameraShaker;

        [Header("UI")]
        [SerializeField]
        private GameOverView _gameOverView;

        [SerializeField]
        private HealthView _healthView;

        private IShipControllerAdapter _shipControllerAdapter;

        private void Awake()
        {
            _shipControllerAdapter = GetComponent<ShipControllerAdapter>();
            _shipControllerAdapter.OnHealthChanged += OnHealthChanged;
            _shipControllerAdapter.OnDead += _gameOverView.Show;
        }

        private void OnDestroy()
        {
            _shipControllerAdapter.OnHealthChanged += OnHealthChanged;
            _shipControllerAdapter.OnDead -= _gameOverView.Show;
        }
        
        private void OnHealthChanged(int currentHealth, int maxHealth)
        {
            _healthView.SetHealth(currentHealth, maxHealth);
            _cameraShaker.Shake();
        }
    }
}