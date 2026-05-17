using UnityEngine;

namespace CoreBreach
{
    public class Projectile : MonoBehaviour, IPoolable
    {
        [SerializeField] float speed = 14f;
        [SerializeField] float lifetime = 2.5f;

        Vector2 dir;
        int damage;
        float life;
        ObjectPool<Projectile> pool;

        public void Bind(ObjectPool<Projectile> owner)
        {
            pool = owner;
        }

        public void Launch(Vector2 direction, int dmg)
        {
            dir = direction.normalized;
            damage = dmg;
            float a = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, a);
        }

        public void OnSpawn()
        {
            life = lifetime;
        }

        public void OnDespawn()
        {
            dir = Vector2.zero;
            damage = 0;
            life = 0f;
        }

        void Update()
        {
            transform.position += (Vector3)(dir * speed * Time.deltaTime);
            life -= Time.deltaTime;
            if (life <= 0f) ReturnToPool();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // ignore the core so the player can't shoot its own base
            if (other.CompareTag("Core")) return;
            if (other.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage(damage);
                ReturnToPool();
            }
        }

        void ReturnToPool()
        {
            if (pool != null) pool.Release(this);
            else gameObject.SetActive(false);
        }
    }
}
