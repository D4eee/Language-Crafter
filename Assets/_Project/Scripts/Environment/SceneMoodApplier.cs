using UnityEngine;

namespace Yiyang.Environment
{
    public sealed class SceneMoodApplier : MonoBehaviour
    {
        public MoodProfile mood;
        private void Start() => MoodManager.Instance?.ApplyMood(mood);
    }
}
