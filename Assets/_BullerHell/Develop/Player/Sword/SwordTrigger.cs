using System.Collections.Generic;
using UnityEngine;

public class SwordTrigger : MonoBehaviour
{
    private float _damage = 0;

    public void Init(float damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Player")
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(_damage);
            }
        }
    }
}
