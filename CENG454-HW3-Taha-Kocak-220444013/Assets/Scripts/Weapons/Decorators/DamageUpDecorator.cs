namespace CoreBreach
{
    public class DamageUpDecorator : WeaponDecorator
    {
        readonly int bonus;

        public DamageUpDecorator(IWeapon inner, int bonus = 1) : base(inner)
        {
            this.bonus = bonus;
        }

        public override int Damage => inner.Damage + bonus;
    }
}
