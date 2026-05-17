using System;
using System.Collections.Generic;
using UnityEngine;

namespace CoreBreach
{
    public class EnemyRegistry : MonoBehaviour
    {
        readonly HashSet<EnemyAI> live = new HashSet<EnemyAI>();

        public event Action<EnemyAI> OnEnemyKilled;
        public int LiveCount => live.Count;

        public void Register(EnemyAI e)
        {
            if (e == null) return;
            live.Add(e);
        }

        public void NotifyKilled(EnemyAI e)
        {
            if (e == null) return;
            if (live.Remove(e)) OnEnemyKilled?.Invoke(e);
        }

        public void Clear()
        {
            live.Clear();
        }
    }
}
