using System;
using DG.Tweening;
using Game.Scripts.Ship;
using UnityEngine;

namespace Game
{
    public class ShipView: MonoBehaviour
    {
        [SerializeField]
        private Renderer _renderer;

        [SerializeField]
        private Transform _viewTransform;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private ShipControllerViewConfig _viewConfig;

        [SerializeField]
        private ParticleSystem _fireVFX;

        [SerializeField]
        private AudioClip _fireSFX;

        [SerializeField]
        private AudioClip _damageSFX;
        
        private Material _material;
        private Tweener _damageAnimation;

        private IShipControllerAdapter _shipControllerAdapter;
        
        private Vector3 _currentDirection;
        
        private void Awake()
        {
            _material = new Material(_viewConfig.MaterialPrefab);
            _renderer.material = _material;

            _shipControllerAdapter = GetComponent<ShipControllerAdapter>();
            _shipControllerAdapter.OnHealthChanged += OnHealthChanged;
            _shipControllerAdapter.OnDead += OnDead;
            _shipControllerAdapter.OnMove += OnMove;
            _shipControllerAdapter.OnFire += OnFire;
        }

        private void OnDestroy()
        {
            _shipControllerAdapter.OnHealthChanged -= OnHealthChanged;
            _shipControllerAdapter.OnDead -= OnDead;
            _shipControllerAdapter.OnMove -= OnMove;
            _shipControllerAdapter.OnFire -= OnFire;
        }

        private void LateUpdate()
        {
            AnimateMovement(Time.deltaTime, _currentDirection);
        }

        private void OnFire(CombatData _)
        {
            if (_fireSFX)
                _audioSource.PlayOneShot(_fireSFX);
            if (_fireVFX)
                _fireVFX.Play();
        }

        private void OnMove(Vector3 direction)
        {
            _currentDirection = direction;
        }
        
        private void OnHealthChanged(int currentHealth, int _)
        {
            if (currentHealth > 0) 
                AnimateDamage();
        }

        private void OnDead()
        {
            ParticleSystem prefab = _viewConfig.DestroyEffectPrefab;
            Instantiate(prefab, _viewTransform.position, prefab.transform.rotation);
        }
        
        private void AnimateMovement(float deltaTime, Vector3 moveDirection)
        {
            Vector3 shipAngles = _viewTransform.localEulerAngles;
            shipAngles.x = _viewConfig.MoveRotationAngle * moveDirection.y;
            shipAngles.y = _viewConfig.MoveRotationAngle / 2 * moveDirection.x * -1f;
            
            Quaternion shipRotation = Quaternion.Euler(shipAngles);
            float t = _viewConfig.MoveSpeed * deltaTime;
            _viewTransform.localRotation = Quaternion.Lerp(_viewTransform.localRotation, shipRotation, t);
        }
        
        private void AnimateDamage()
        {
            if (_damageAnimation != null && _damageAnimation.IsActive())
                _damageAnimation.Kill();

            _damageAnimation = DOVirtual.Float(
                0f,
                1f,
                _viewConfig.HitDuration,
                progress => _material.SetFloat(_viewConfig.HitPropertyName,
                    _viewConfig.HitAnimationCurve.Evaluate(progress))
            ).SetLink(_renderer.gameObject);

            if (_damageSFX)
                _audioSource.PlayOneShot(_damageSFX);
        }
    }
}