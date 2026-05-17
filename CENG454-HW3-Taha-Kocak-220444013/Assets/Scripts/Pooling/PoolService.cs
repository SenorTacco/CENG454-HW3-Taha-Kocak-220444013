using UnityEngine;

namespace CoreBreach
{
    public class PoolService : MonoBehaviour
    {
        [SerializeField] Projectile projectilePrefab;
        [SerializeField] EnemyAI enemyPrefab;
        [SerializeField] int projectilePrewarm = 16;
        [SerializeField] int enemyPrewarm = 8;

        ObjectPool<Projectile> projectiles;
        ObjectPool<EnemyAI> enemies;

        void Awake()
        {
            projectiles = new ObjectPool<Projectile>(projectilePrefab, projectilePrewarm, transform);
            enemies = new ObjectPool<EnemyAI>(enemyPrefab, enemyPrewarm, transform);
        }

        public Projectile SpawnProjectile(Vector3 position, Vector2 direction, int damage)
        {
            var p = projectiles.Get(position, Quaternion.identity);
            p.Bind(projectiles);
            p.Launch(direction, damage);
            return p;
        }

        public EnemyAI SpawnEnemy(Vector3 position, Transform target, IMovementStrategy strategy, EnemyRegistry registry)
        {
            var e = enemies.Get(position, Quaternion.identity);
            e.Bind(enemies, target, strategy, registry);
            return e;
        }
    }
}
