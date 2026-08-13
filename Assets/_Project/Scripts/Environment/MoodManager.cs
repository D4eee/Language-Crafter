using UnityEngine;

namespace Yiyang.Environment
{
    public sealed class MoodManager : MonoBehaviour
    {
        public static MoodManager Instance { get; private set; }
        public MoodProfile currentMood;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void ApplyMood(MoodProfile mood)
        {
            if (mood == null) return;
            currentMood = mood;
            RenderSettings.ambientLight = mood.ambientColor;
            RenderSettings.fog = true;
            RenderSettings.fogColor = mood.fogColor;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogDensity = mood.fogDensity;
            Light main = FindFirstObjectByType<Light>();
            if (main != null)
            {
                main.color = mood.mainLightColor;
                main.intensity = mood.mainLightIntensity;
            }
            AmbientSoundManager.Instance?.Play(mood.ambienceClip);
        }
    }
}
