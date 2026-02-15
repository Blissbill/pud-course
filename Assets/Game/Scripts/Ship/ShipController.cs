using System;
using Game.Components;
using UnityEngine;

namespace Game.Scripts.Ship
{
    public class ShipController: MonoBehaviour
    {
        [SerializeField] 
        private HealthComponent _healthComponent;
        [SerializeField]
        private ShipControllerConfig _config;

        [SerializeField] private TeamType _team = TeamType.None;

        public void Awake()
        {
            _healthComponent.Initialize(_config, _team);
        }
    }
}