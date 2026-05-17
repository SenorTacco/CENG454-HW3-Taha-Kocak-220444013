using UnityEngine;

namespace CoreBreach
{
    [CreateAssetMenu(fileName = "Wave", menuName = "CoreBreach/Wave Definition")]
    public class WaveDefinition : ScriptableObject
    {
        public int enemyCount = 6;
        public float spawnInterval = 0.6f;
        [Range(0f, 1f)] public float zigZagRatio = 0.3f;
    }
}
