using System;
using Game.Core;
using UnityEngine;

namespace Game.Bullet
{
    // +
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collider2D> OnTriggerEntered;
        public event Action<TeamType> OnTeamChanged;
        public event Action<Vector3> OnHit;
        public Vector3 Position => transform.position;
        public TeamType Team => _team;
        public int Damage => _damage;

        [SerializeField]
        private TeamType _team = TeamType.None;
        [SerializeField]
        private Vector2 _direction;
        
        [SerializeField]
        private int _damage;
        [SerializeField]
        private float _speed;
        
        public Bullet WithTeam(TeamType teamType)
        {
            _team = teamType;
            OnTeamChanged?.Invoke(_team);
            return this;
        }

        public Bullet WithDirection(Vector2 direction)
        {
            _direction = direction;
            return this;
        }

        public Bullet WithDamage(int damage)
        {
            _damage = damage;
            return this;
        }

        public Bullet WithSpeed(float speed)
        {
            _speed = speed;
            return this;
        }

        public Bullet WithPosition(Vector3 position)
        {
            transform.position = position;
            return this;
        }

        public Bullet WithRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
            return this;
        }

        public Bullet WithLayer(int layer)
        {
            gameObject.layer = layer;
            return this;
        }

        public Bullet Clear()
        {
            _team = TeamType.None;
            // Вызываю событие тут, а не WithTeam, так как метод может измениться и там может исчезнуть OnTeamChanged
            OnTeamChanged?.Invoke(TeamType.None);
            _direction = Vector2.zero;
            _damage = 0;
            _speed = 0;
            transform.position = Vector3.zero;
            return this;
        }

        public void Move(float deltaTime)
        {
            Vector3 moveStep = _direction * _speed * deltaTime;
            transform.position += moveStep;
        }

        public void Hit()
        {
            gameObject.SetActive(false);
            OnHit?.Invoke(transform.position);
        }
        private void OnTriggerEnter2D(Collider2D other) => this.OnTriggerEntered?.Invoke(this, other);
        
    }
}