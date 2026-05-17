using UnityEngine;

namespace CoreBreach
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 14f;
        [SerializeField] float lifetime = 2.5f;

        Vector2 dir;
        int damage;
        float life;

        public void Launch(Vector2 direction, int dmg)
        {
            dir = direction.normalized;
            damage = dmg;
            life = lifetime;
            float a = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, a);
        }

        void Update()
        {
            transform.position += (Vector3)(dir * speed * Time.deltaTime);
            life -= Time.deltaTime;
            if (life <= 0f) Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
