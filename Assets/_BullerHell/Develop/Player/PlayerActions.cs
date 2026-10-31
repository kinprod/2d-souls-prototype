using Player.Animation;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerActions : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private Rigidbody2D _rigidbody2D;
        [SerializeField]
        private Animator _animator;

        [Space]
        [SerializeField]
        private float _health = 10f;
        [SerializeField, Min(0)]
        private float _speed = 0.08f;
        [SerializeField, Min(0)]
        private float _damage = 1f;
        [SerializeField, Min(0)]
        private float _rollSpeed = 0.1f;


        [Space]
        [SerializeField]
        private AnimationEvents _animationEvents;
        [SerializeField]
        private SwordTrigger _swordTrigger1;
        [SerializeField]
        private SwordTrigger _swordTrigger2;
        [SerializeField]
        private SwordTrigger _swordTrigger3;

        private InputActions _actions;

        private InputAction _move;
        private InputAction _roll;
        private InputAction _attack1;
        private InputAction _attack2;

        private bool _isCanMove = true;
        private bool _inRoll = false;

        private Vector2 _rollDirection;

        public event Action OnHealthChange;

        public float Health { get { return _health; } }


        private void Awake()
        {
            _actions = new InputActions();
            _actions.Enable();
            _move = _actions.Gameplay.Move;
            _roll = _actions.Gameplay.Roll;
            _attack1 = _actions.Gameplay.Attack1;
            _attack2 = _actions.Gameplay.Attack2;

            _swordTrigger1.Init(_damage);
            _swordTrigger2.Init(_damage);
            _swordTrigger3.Init(_damage);
        }


        private void OnEnable()
        {
            _attack1.performed += Attack1;
            _attack2.performed += Attack2;
            _roll.performed += Roll;
            _animationEvents.StartAttackAction += CannotMove;
            _animationEvents.StopAttackAction += CanMove;
            _animationEvents.StartRollAction += StartRoll;
            _animationEvents.StopRollAction += StopRoll;
        }


        private void OnDisable()
        {
            _attack1.performed -= Attack1;
            _attack2.performed -= Attack2;
            _roll.performed -= Roll;
            _animationEvents.StartAttackAction -= CannotMove;
            _animationEvents.StopAttackAction -= CanMove;
            _animationEvents.StartRollAction -= StartRoll;
            _animationEvents.StopRollAction -= StopRoll;
        }


        private void FixedUpdate()
        {
            if (_isCanMove)
            {
                Move();
            }
            else if (_inRoll)
            {
                RollAction();
            }
        }


        private void RollAction()
        {
            _rigidbody2D.position += _rollDirection * _rollSpeed;

            if (_rollDirection.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (_rollDirection.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }


        private void Roll(InputAction.CallbackContext context)
        {
            _rollDirection = _move.ReadValue<Vector2>();
            if (_rollDirection.magnitude != 0)
            {
                StartCoroutine(RollCoroutine());
            }
        }


        private IEnumerator RollCoroutine()
        {
            _animator.SetBool("Roll", true);
            yield return new WaitForEndOfFrame();
            _animator.SetBool("Roll", false);
        }


        private void CannotMove() { _isCanMove = false; }


        private void CanMove() { _isCanMove = true; }

        
        private void StartRoll()
        {
            _isCanMove = false;
            _inRoll = true;
        }

        private void StopRoll()
        {
            _isCanMove = true;
            _inRoll = false;
        }


        private void Attack1(InputAction.CallbackContext context)
        {
            StartCoroutine(AttackCoroutine("Attack1"));
        }


        private void Attack2(InputAction.CallbackContext context)
        {
            StartCoroutine(AttackCoroutine("Attack2"));
        }


        private IEnumerator AttackCoroutine(string animationName)
        {
            _animator.SetBool(animationName, true);
            yield return new WaitForEndOfFrame();
            _animator.SetBool(animationName, false);
        }


        private void Move()
        {
            var direction = _move.ReadValue<Vector2>();
            Debug.Log(direction);

            _rigidbody2D.position += direction * _speed;

            if (direction.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (direction.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (direction.magnitude != 0)
            {
                _animator.SetBool("Move", true);
            }
            else
            {
                _animator.SetBool("Move", false);
            }
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
            }
            OnHealthChange?.Invoke();
            Debug.Log("Damage");
        }
    }
}