using TMPro;
using UnityEngine;

namespace CoreBreach
{
    public class HUDPresenter : MonoBehaviour
    {
        [SerializeField] CoreHealth core;
        [SerializeField] GameDirector director;
        [SerializeField] TMP_Text coreText;
        [SerializeField] GameObject gameOverPanel;
        [SerializeField] TMP_Text gameOverText;

        void OnEnable()
        {
            if (core != null)
            {
                core.OnDamaged += HandleDamaged;
                core.OnDestroyed += HandleDestroyed;
                HandleDamaged(core.HP, core.MaxHP);
            }
            if (director != null) director.OnStateChanged += HandleState;
            if (gameOverPanel != null) gameOverPanel.SetActive(false);
        }

        void OnDisable()
        {
            if (core != null)
            {
                core.OnDamaged -= HandleDamaged;
                core.OnDestroyed -= HandleDestroyed;
            }
            if (director != null) director.OnStateChanged -= HandleState;
        }

        void HandleDamaged(int current, int max)
        {
            if (coreText != null) coreText.text = $"Core  {current}/{max}";
        }

        void HandleDestroyed()
        {
            // visual handled in HandleState; this is for cases where we want a hit flash
        }

        void HandleState(GameState s)
        {
            if (s == GameState.Playing) return;
            if (gameOverPanel != null) gameOverPanel.SetActive(true);
            if (gameOverText != null) gameOverText.text = (s == GameState.Won) ? "YOU SURVIVED" : "CORE LOST";
        }
    }
}
