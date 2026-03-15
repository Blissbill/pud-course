using System;
using Modules.UI;
using UnityEngine;

namespace Game.UI
{
    public sealed class GameOverAdapter : MonoBehaviour
    {
        [SerializeField]
        private GameOverView _gameOverView;
        [SerializeField]
        private PlayerShip  _player;

        private void Awake()
        {
            _player.OnDied += _gameOverView.Show;
        }

        private void OnDestroy()
        {
            _player.OnDied -= _gameOverView.Show;
        }
    }
}