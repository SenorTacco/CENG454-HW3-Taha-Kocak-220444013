using System;
using UnityEngine;

namespace CoreBreach
{
    public enum GameState { Playing, Won, Lost }

    public class GameDirector : MonoBehaviour
    {
        [SerializeField] CoreHealth core;

        public GameState State { get; private set; } = GameState.Playing;
        public event Action<GameState> OnStateChanged;

        void OnEnable()
        {
            if (core != null) core.OnDestroyed += HandleCoreLost;
        }

        void OnDisable()
        {
            if (core != null) core.OnDestroyed -= HandleCoreLost;
        }

        void HandleCoreLost()
        {
            SetState(GameState.Lost);
        }

        public void DeclareVictory()
        {
            if (State == GameState.Playing) SetState(GameState.Won);
        }

        void SetState(GameState s)
        {
            if (State == s) return;
            State = s;
            OnStateChanged?.Invoke(s);
            Time.timeScale = (s == GameState.Playing) ? 1f : 0f;
        }
    }
}
