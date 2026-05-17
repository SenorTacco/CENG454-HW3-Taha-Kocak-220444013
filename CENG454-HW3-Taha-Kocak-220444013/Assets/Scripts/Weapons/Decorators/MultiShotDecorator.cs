namespace CoreBreach
{
    public class MultiShotDecorator : WeaponDecorator
    {
        readonly int extraProjectiles;
        readonly float extraSpread;

        public MultiShotDecorator(IWeapon inner, int extraProjectiles = 2, float extraSpreadDeg = 15f) : base(inner)
        {
            this.extraProjectiles = extraProjectiles;
            this.extraSpread = extraSpreadDeg;
        }

        public override int ProjectileCount => inner.ProjectileCount + extraProjectiles;
        public override float SpreadDegrees => inner.SpreadDegrees + extraSpread;
    }
}
