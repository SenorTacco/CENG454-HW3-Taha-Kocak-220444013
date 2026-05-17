using UnityEngine;

namespace CoreBreach
{
    public class AudioCueRouter : MonoBehaviour
    {
        [SerializeField] CoreHealth core;
        [SerializeField] AudioSource source;
        [SerializeField] AudioClip hitClip;
        [SerializeField] AudioClip destroyedClip;

        void OnEnable()
        {
            if (core == null) return;
            core.OnDamaged += HandleHit;
            core.OnDestroyed += HandleDestroyed;
        }

        void OnDisable()
        {
            if (core == null) return;
            core.OnDamaged -= HandleHit;
            core.OnDestroyed -= HandleDestroyed;
        }

        void HandleHit(int current, int max)
        {
            if (source != null && hitClip != null) source.PlayOneShot(hitClip);
        }

        void HandleDestroyed()
        {
            if (source != null && destroyedClip != null) source.PlayOneShot(destroyedClip);
        }
    }
}
