using UnityEngine;

namespace CoreBreach
{
    public interface IMovementStrategy
    {
        Vector2 GetVelocity(Vector2 selfPos, Vector2 targetPos, float speed, float time);
    }
}
