using System;
using UnityEngine;

namespace Boss
{
    public class AnimationEvents : MonoBehaviour
    {
        public event Action StartAttackAction;
        public event Action StopAttackAction;

        [SerializeField]
        private Collider2D _attack1Collider;
        [SerializeField]
        private Collider2D _attack2Collider;


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


        public void EnableAttack2Collider()
        {
            _attack2Collider.enabled = true;
        }


        public void DisableAttack2Collider()
        {
            _attack2Collider.enabled = false;
        }
    }
}