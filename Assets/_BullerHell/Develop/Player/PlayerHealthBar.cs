using Boss;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image _healthBar;
        [SerializeField]
        private PlayerActions _player;

        private float _maxHealth;

        private void Awake()
        {
            _maxHealth = _player.Health;
        }


        private void OnEnable()
        {
            _player.OnHealthChange += HealthChange;
        }


        private void OnDisable()
        {
            _player.OnHealthChange -= HealthChange;
        }


        private void HealthChange()
        {
            _healthBar.fillAmount = _player.Health / _maxHealth;
        }
    }
}