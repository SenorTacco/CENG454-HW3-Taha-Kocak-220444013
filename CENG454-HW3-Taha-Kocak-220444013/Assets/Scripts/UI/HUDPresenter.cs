using TMPro;
using UnityEngine;

namespace CoreBreach
{
    public class HUDPresenter : MonoBehaviour
    {
        [SerializeField] CoreHealth core;
        [SerializeField] GameDirector director;
        [SerializeField] WaveSpawner waveSpawner;
        [SerializeField] EnemyRegistry registry;

        [SerializeField] TMP_Text coreText;
        [SerializeField] TMP_Text waveText;
        [SerializeField] TMP_Text killsText;
        [SerializeField] GameObject gameOverPanel;
        [SerializeField] TMP_Text gameOverText;

        int kills;

        void OnEnable()
        {
            if (core != null)
            {
                core.OnDamaged += HandleDamaged;
                core.OnDestroyed += HandleDestroyed;
                HandleDamaged(core.HP, core.MaxHP);
            }
            if (director != null) director.OnStateChanged += HandleState;
            if (waveSpawner != null) waveSpawner.OnWaveStarted += HandleWaveStarted;
            if (registry != null) registry.OnEnemyKilled += HandleEnemyKilled;

            if (gameOverPanel != null) gameOverPanel.SetActive(false);
            if (killsText != null) killsText.text = "Kills  0";
        }

        void OnDisable()
        {
            if (core != null)
            {
                core.OnDamaged -= HandleDamaged;
                core.OnDestroyed -= HandleDestroyed;
            }
            if (director != null) director.OnStateChanged -= HandleState;
            if (waveSpawner != null) waveSpawner.OnWaveStarted -= HandleWaveStarted;
            if (registry != null) registry.OnEnemyKilled -= HandleEnemyKilled;
        }

        void HandleDamaged(int current, int max)
        {
            if (coreText != null) coreText.text = $"Core  {current}/{max}";
        }

        void HandleDestroyed()
        {
            // visual handled by HandleState
        }

        void HandleState(GameState s)
        {
            if (s == GameState.Playing) return;
            if (gameOverPanel != null) gameOverPanel.SetActive(true);
            if (gameOverText != null)
            {
                gameOverText.text = (s == GameState.Won) ? "YOU SURVIVED\nPress R to play again"
                                                        : "CORE LOST\nPress R to retry";
            }
        }

        void HandleWaveStarted(int index, int total)
        {
            if (waveText != null) waveText.text = $"Wave  {index}/{total}";
        }

        void HandleEnemyKilled(EnemyAI _)
        {
            kills++;
            if (killsText != null) killsText.text = $"Kills  {kills}";
        }
    }
}
