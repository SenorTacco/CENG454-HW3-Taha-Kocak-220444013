using UnityEngine;

namespace CoreBreach
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyAI : MonoBehaviour, IPoolable
    {
        [SerializeField] float speed = 2.2f;
        [SerializeField] int contactDamage = 1;
        [SerializeField] float damageCooldown = 0.6f;

        Rigidbody2D rb;
        EnemyHealth health;
        IMovementStrategy strategy;
        Transform target;
        ObjectPool<EnemyAI> pool;
        EnemyRegistry registry;

        float lifeTime;
        float damageTimer;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            health = GetComponent<EnemyHealth>();
        }

        // OnEnable/OnDisable is the key to ghost-subscriber prevention with pooling.
        void OnEnable()
        {
            if (health != null) health.OnDied += HandleDied;
        }

        void OnDisable()
        {
            if (health != null) health.OnDied -= HandleDied;
        }

        public void Bind(ObjectPool<EnemyAI> owner, Transform aimAt, IMovementStrategy ms, EnemyRegistry reg)
        {
            pool = owner;
            target = aimAt;
            strategy = ms;
            registry = reg;
        }

        public void OnSpawn()
        {
            lifeTime = 0f;
            damageTimer = 0f;
            health.ResetHealth();
        }

        public void OnDespawn()
        {
            target = null;
            strategy = null;
            registry = null;
            rb.linearVelocity = Vector2.zero;
        }

        void FixedUpdate()
        {
            if (strategy == null || target == null) return;
            rb.linearVelocity = strategy.GetVelocity(transform.position, target.position, speed, lifeTime);
            lifeTime += Time.fixedDeltaTime;
            if (damageTimer > 0f) damageTimer -= Time.fixedDeltaTime;
        }

        void OnCollisionEnter2D(Collision2D c) { TryHitCore(c.collider); }
        void OnCollisionStay2D(Collision2D c) { TryHitCore(c.collider); }

        void TryHitCore(Collider2D other)
        {
            if (damageTimer > 0f) return;
            if (other.TryGetComponent<CoreHealth>(out var core))
            {
                core.TakeDamage(contactDamage);
                damageTimer = damageCooldown;
            }
        }

        void HandleDied()
        {
            if (registry != null) registry.NotifyKilled(this);
            if (pool != null) pool.Release(this);
            else gameObject.SetActive(false);
        }
    }
}
