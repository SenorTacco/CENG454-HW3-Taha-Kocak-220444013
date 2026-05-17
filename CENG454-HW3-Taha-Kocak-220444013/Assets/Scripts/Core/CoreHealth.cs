using System;
using UnityEngine;

namespace CoreBreach
{
    public class CoreHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] int maxHP = 20;
        int hp;

        public int HP => hp;
        public int MaxHP => maxHP;
        public bool IsDead => hp <= 0;

        // observer surface: any system can subscribe without us knowing them
        public event Action<int, int> OnDamaged;   // current, max
        public event Action OnDestroyed;

        void Awake()
        {
            hp = maxHP;
        }

        public void TakeDamage(int amount)
        {
            if (IsDead) return;
            hp -= amount;
            if (hp < 0) hp = 0;

            OnDamaged?.Invoke(hp, maxHP);
            if (hp == 0) OnDestroyed?.Invoke();
        }
    }
}
