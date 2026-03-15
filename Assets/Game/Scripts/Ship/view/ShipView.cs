using DG.Tweening;
using Game.Ship.Components;
using UnityEngine;

namespace Game.Ship
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
        
        private Vector3 _currentDirection;

        private void Awake()
        {
            _material = new Material(_viewConfig.MaterialPrefab);
            _renderer.material = _material;
        }

        private void OnEnable()
        {
            _renderer.enabled = true;
        }

        private void LateUpdate()
        {
            AnimateMovement(Time.deltaTime, _currentDirection);
        }

        public void Fire(CombatData _)
        {
            if (_fireSFX)
                _audioSource.PlayOneShot(_fireSFX);
            if (_fireVFX)
                _fireVFX.Play();
        }

        public void Move(Vector3 direction)
        {
            _currentDirection = direction;
        }
        
        public void HealthChanged(int currentHealth)
        {
            if (currentHealth > 0) 
                AnimateDamage();
        }

        public void Death()
        {
            _renderer.enabled = false;
            ParticleSystem prefab = _viewConfig.DestroyEffectPrefab;
            // В ParticleSystem включил уничтожение по окончанию анимации
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