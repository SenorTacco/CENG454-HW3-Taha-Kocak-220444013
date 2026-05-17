namespace CoreBreach
{
    public interface IWeapon
    {
        int Damage { get; }
        float Cooldown { get; }
        int ProjectileCount { get; }
        float SpreadDegrees { get; }

        bool CanFire { get; }
        void NoteFired();
        void Tick(float deltaTime);
    }
}
