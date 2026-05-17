using UnityEngine;

namespace CoreBreach
{
    public class PoolService : MonoBehaviour
    {
        [SerializeField] Projectile projectilePrefab;
        [SerializeField] int projectilePrewarm = 16;

        ObjectPool<Projectile> projectiles;

        void Awake()
        {
            projectiles = new ObjectPool<Projectile>(projectilePrefab, projectilePrewarm, transform);
        }

        public Projectile SpawnProjectile(Vector3 position, Vector2 direction, int damage)
        {
            var p = projectiles.Get(position, Quaternion.identity);
            p.Bind(projectiles);
            p.Launch(direction, damage);
            return p;
        }
    }
}
