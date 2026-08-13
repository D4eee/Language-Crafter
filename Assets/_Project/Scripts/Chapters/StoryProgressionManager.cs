using UnityEngine;
using Yiyang.Story;

namespace Yiyang.Chapters
{
    public sealed class StoryProgressionManager : MonoBehaviour
    {
        public static StoryProgressionManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void MarkSceneComplete(string unitySceneName)
        {
            StoryFlagManager.Instance?.SetFlag("completed_scene_" + unitySceneName);
        }
    }
}
