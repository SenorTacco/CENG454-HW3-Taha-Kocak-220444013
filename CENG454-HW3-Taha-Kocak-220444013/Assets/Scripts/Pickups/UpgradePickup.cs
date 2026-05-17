using UnityEngine;

namespace CoreBreach
{
    [RequireComponent(typeof(Collider2D))]
    public class UpgradePickup : MonoBehaviour
    {
        [SerializeField] UpgradeKind kind;

        void Awake()
        {
            var col = GetComponent<Collider2D>();
            if (col != null) col.isTrigger = true;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerController>(out var player))
            {
                player.ApplyUpgrade(kind);
                Destroy(gameObject);
            }
        }
    }
}
