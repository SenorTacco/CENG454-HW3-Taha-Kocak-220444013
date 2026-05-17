using UnityEngine;

namespace CoreBreach
{
    public interface IWeapon
    {
        bool CanFire { get; }
        void Fire(Vector2 origin, Vector2 direction);
        void Tick(float deltaTime);
    }
}
