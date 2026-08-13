using UnityEngine;

namespace Yiyang.Environment
{
    [CreateAssetMenu(menuName = "Yiyang/Mood Profile")]
    public sealed class MoodProfile : ScriptableObject
    {
        public string moodID;
        public Color ambientColor = new(0.08f, 0.08f, 0.1f);
        public Color fogColor = new(0.05f, 0.06f, 0.07f);
        public float fogDensity = 0.03f;
        public Color mainLightColor = new(0.7f, 0.8f, 0.9f);
        public float mainLightIntensity = 0.8f;
        public float postExposure = -0.5f;
        public float contrast = 25f;
        public float saturation = -35f;
        [Range(0f, 1f)] public float vignetteIntensity = 0.55f;
        [Range(0f, 1f)] public float filmGrainIntensity = 0.35f;
        [Range(0f, 1f)] public float chromaticAberrationIntensity = 0.08f;
        public float bloomIntensity = 0.25f;
        public AudioClip ambienceClip;
        public AudioReverbPreset reverbPreset = AudioReverbPreset.Off;
    }
}
