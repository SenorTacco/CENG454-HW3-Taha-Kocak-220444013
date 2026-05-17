using UnityEngine;

namespace CoreBreach
{
    public class ZigZagStrategy : IMovementStrategy
    {
        readonly float frequency;
        readonly float amplitude;

        public ZigZagStrategy(float frequency = 3f, float amplitude = 0.8f)
        {
            this.frequency = frequency;
            this.amplitude = amplitude;
        }

        public Vector2 GetVelocity(Vector2 selfPos, Vector2 targetPos, float speed, float time)
        {
            Vector2 toward = targetPos - selfPos;
            if (toward.sqrMagnitude < 0.0001f) return Vector2.zero;

            Vector2 forward = toward.normalized;
            Vector2 side = new Vector2(-forward.y, forward.x);
            float wiggle = Mathf.Sin(time * frequency) * amplitude;
            return (forward + side * wiggle).normalized * speed;
        }
    }
}
