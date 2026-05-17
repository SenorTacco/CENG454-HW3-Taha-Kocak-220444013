using UnityEngine;

namespace CoreBreach
{
    public class RapidFireDecorator : WeaponDecorator
    {
        readonly float multiplier;

        public RapidFireDecorator(IWeapon inner, float multiplier = 0.6f) : base(inner)
        {
            this.multiplier = Mathf.Clamp(multiplier, 0.1f, 1f);
        }

        public override float Cooldown => inner.Cooldown * multiplier;
    }
}
