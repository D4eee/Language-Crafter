using UnityEngine;

namespace Yiyang.Environment
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class AmbientSoundManager : MonoBehaviour
    {
        public static AmbientSoundManager Instance { get; private set; }
        private AudioSource source;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            source = GetComponent<AudioSource>();
            source.loop = true;
            source.playOnAwake = false;
        }

        public void Play(AudioClip clip)
        {
            if (clip == null || source == null || source.clip == clip) return;
            source.clip = clip;
            source.Play();
        }
    }
}
