using System;
using Unity.Mathematics;
using UnityEngine;


namespace Boss
{
    public class BossBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private GameObject _playerTarget;

        [Space]
        [SerializeField]
        private float _health = 100f;
        [SerializeField]
        private float _speed = 0.03f;
        [SerializeField]
        private float _damage = 10f;
        [SerializeField]
        private float _attackDistance = 1f;


        [Space]
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private AnimationEvents _events;

        [Space]
        [SerializeField]
        private SwordTrigger _swordTrigger1;
        [SerializeField]
        private SwordTrigger _swordTrigger2;

        public event Action OnHealthChanges;

        public float Health { get { return _health; } }

        private bool _canMove = true;


        private void Awake()
        {
            _swordTrigger1.Init(_damage);
            _swordTrigger2.Init(_damage);
        }


        private void OnEnable()
        {
            _events.StartAttackAction += StartAttack;
            _events.StopAttackAction += StopAttack;
        }


        private void OnDisable()
        {
            _events.StartAttackAction -= StartAttack;
            _events.StopAttackAction -= StopAttack;
        }


        private void FixedUpdate()
        {
            var direction = _playerTarget.transform.position - transform.position;
            
            if (Math.Abs(direction.x) > _attackDistance && _canMove)
            {
                Move();
            }
            else
            {
                _animator.SetBool($"Attack{UnityEngine.Random.Range(1, 3)}", true);
            }
        }


        private void StartAttack()
        {
            _canMove = false;
        }


        private void StopAttack()
        {
            _canMove = true;
            _animator.SetBool("Attack1", false);
            _animator.SetBool("Attack2", false);
        }


        void IDamageable.TakeDamage(float damage)
        {
            if (_health - damage > 0)
            {
                _health -= damage;
            }
            else
            {
                _health = 0;
                _animator.SetBool("Death", true);
            }
            OnHealthChanges?.Invoke();
        }


        private void Move()
        {
            var direction = _playerTarget.transform.position - transform.position;
            direction = new Vector3(direction.x, 0);
            transform.position += direction * _speed;

            if (direction.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (direction.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
