namespace CoreBreach
{
    public abstract class WeaponDecorator : IWeapon
    {
        protected readonly IWeapon inner;

        protected WeaponDecorator(IWeapon inner)
        {
            this.inner = inner;
        }

        public virtual int Damage => inner.Damage;
        public virtual float Cooldown => inner.Cooldown;
        public virtual int ProjectileCount => inner.ProjectileCount;
        public virtual float SpreadDegrees => inner.SpreadDegrees;

        public virtual bool CanFire => inner.CanFire;
        public virtual void NoteFired() => inner.NoteFired();
        public virtual void Tick(float deltaTime) => inner.Tick(deltaTime);
    }
}
