using DG.Tweening;
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
        
        private void Awake()
        {
            _material = new Material(_viewConfig.MaterialPrefab);
            _renderer.material = _material;
        }
    }
}