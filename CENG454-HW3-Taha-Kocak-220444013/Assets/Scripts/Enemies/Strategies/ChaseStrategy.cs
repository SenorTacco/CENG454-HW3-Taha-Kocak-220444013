using UnityEngine;

namespace CoreBreach
{
    public class ChaseStrategy : IMovementStrategy
    {
        public Vector2 GetVelocity(Vector2 selfPos, Vector2 targetPos, float speed, float time)
        {
            Vector2 dir = targetPos - selfPos;
            if (dir.sqrMagnitude < 0.0001f) return Vector2.zero;
            return dir.normalized * speed;
        }
    }
}
