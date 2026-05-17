namespace CoreBreach
{
    public class BaseWeapon : IWeapon
    {
        public int Damage { get; }
        public float Cooldown { get; }
        public int ProjectileCount => 1;
        public float SpreadDegrees => 0f;

        float timer;

        public BaseWeapon(int damage, float cooldown)
        {
            Damage = damage;
            Cooldown = cooldown;
        }

        public bool CanFire => timer <= 0f;

        public void NoteFired()
        {
            timer = Cooldown;
        }

        public void Tick(float deltaTime)
        {
            if (timer > 0f) timer -= deltaTime;
        }
    }
}
