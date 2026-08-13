using UnityEngine;
using Yiyang.Environment;

namespace Yiyang.Narration
{
    [System.Serializable]
    public sealed class NarrationLine
    {
        [TextArea(2, 5)] public string text;
        public float delayBefore;
        public float displayDuration = 2f;
        public bool requireInput = true;
        public AudioClip optionalAudioClip;
        public MoodProfile optionalMoodOverride;
    }
}
