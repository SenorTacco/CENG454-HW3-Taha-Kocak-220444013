using System;
using UnityEngine;

namespace CoreBreach
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] int maxHP = 3;
        int hp;

        public event Action OnDied;

        void Awake()
        {
            hp = maxHP;
        }

        public void ResetHealth()
        {
            hp = maxHP;
        }

        public void TakeDamage(int amount)
        {
            if (hp <= 0) return;
            hp -= amount;
            if (hp <= 0)
            {
                hp = 0;
                OnDied?.Invoke();
            }
        }
    }
}
