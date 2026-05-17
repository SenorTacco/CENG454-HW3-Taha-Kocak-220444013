using System;
using System.Collections;
using UnityEngine;

namespace CoreBreach
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] WaveDefinition[] waves;
        [SerializeField] PoolService poolService;
        [SerializeField] GameDirector director;
        [SerializeField] EnemyRegistry registry;
        [SerializeField] Transform coreTransform;
        [SerializeField] float spawnRadius = 8f;
        [SerializeField] float startDelay = 1.5f;
        [SerializeField] float gapBetweenWaves = 2.5f;

        public event Action<int, int> OnWaveStarted;  // (waveIndex, totalWaves)
        public event Action OnAllWavesCleared;

        readonly IMovementStrategy chase = new ChaseStrategy();
        readonly IMovementStrategy zigzag = new ZigZagStrategy();

        void Start()
        {
            StartCoroutine(RunAllWaves());
        }

        IEnumerator RunAllWaves()
        {
            yield return new WaitForSeconds(startDelay);

            for (int i = 0; i < waves.Length; i++)
            {
                OnWaveStarted?.Invoke(i + 1, waves.Length);
                yield return SpawnWave(waves[i]);
                yield return new WaitUntil(() => registry == null || registry.LiveCount == 0);
                if (i < waves.Length - 1) yield return new WaitForSeconds(gapBetweenWaves);
            }

            OnAllWavesCleared?.Invoke();
            if (director != null) director.DeclareVictory();
        }

        IEnumerator SpawnWave(WaveDefinition def)
        {
            int zigCount = Mathf.RoundToInt(def.enemyCount * def.zigZagRatio);
            for (int i = 0; i < def.enemyCount; i++)
            {
                IMovementStrategy s = (i < zigCount) ? zigzag : chase;
                SpawnOne(s);
                yield return new WaitForSeconds(def.spawnInterval);
            }
        }

        void SpawnOne(IMovementStrategy s)
        {
            if (poolService == null || coreTransform == null) return;
            Vector2 dir = UnityEngine.Random.insideUnitCircle.normalized;
            if (dir == Vector2.zero) dir = Vector2.right;
            Vector2 pos = (Vector2)coreTransform.position + dir * spawnRadius;
            var enemy = poolService.SpawnEnemy(pos, coreTransform, s, registry);
            registry?.Register(enemy);
        }
    }
}
