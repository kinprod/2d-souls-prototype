using System;
using UnityEngine;

namespace Player.Animation
{
    public class AnimationEvents : MonoBehaviour
    {
        public event Action StartAttackAction;
        public event Action StopAttackAction;

        public event Action StartRollAction;
        public event Action StopRollAction;

        [SerializeField]
        private Collider2D _attack1Collider;
        [SerializeField]
        private Collider2D _attack2Collider1;
        [SerializeField]
        private Collider2D _attack2Collider2;

        public void StartAttack()
        {
            StartAttackAction?.Invoke();
        }

        public void StopAttack()
        {
            StopAttackAction?.Invoke();
        }
        
        public void EnableAttack1Collider()
        {
            _attack1Collider.enabled = true;
        }

        public void DisableAttack1Collider()
        {
            _attack1Collider.enabled = false;
        }

        public void EnableAttack2Collider1()
        {
            _attack2Collider1.enabled = true;
        }

        public void DisableAttack2Collider1()
        {
            _attack2Collider1.enabled = false;
        }

        public void EnableAttack2Collider2()
        {
            _attack2Collider2.enabled = true;
        }

        public void DisableAttack2Collider2()
        {
            _attack2Collider2.enabled = false;
        }

        public void StartRoll()
        {
            StartRollAction?.Invoke();
        }

        public void StopRoll()
        {
            StopRollAction?.Invoke();
        }
    }
}
