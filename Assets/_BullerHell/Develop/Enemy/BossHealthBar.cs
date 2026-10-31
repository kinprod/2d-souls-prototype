using System;
using UnityEngine;
using UnityEngine.UI;


namespace Boss
{
    public class BossHealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image _healthBar;
        [SerializeField]
        private BossBehaviour _bossBehaviour;

        private float _maxHealth;

        private void Awake()
        {
            _maxHealth = _bossBehaviour.Health;
        }


        private void OnEnable()
        {
            _bossBehaviour.OnHealthChanges += HealthChange;
        }


        private void OnDisable()
        {
            _bossBehaviour.OnHealthChanges -= HealthChange;
        }


        private void HealthChange()
        {
            _healthBar.fillAmount = _bossBehaviour.Health / _maxHealth;
        }
    }
}