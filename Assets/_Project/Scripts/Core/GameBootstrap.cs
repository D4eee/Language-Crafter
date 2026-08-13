using UnityEngine;
using Yiyang.Chapters;
using Yiyang.Endings;
using Yiyang.Environment;
using Yiyang.Narration;
using Yiyang.Dialogue;
using Yiyang.SaveLoad;
using Yiyang.SceneManagement;
using Yiyang.Story;
using Yiyang.UI;
using Yiyang.Interaction;

namespace Yiyang.Core
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private string firstPlayableScene = "Prototype_Hallway";
        [SerializeField] private bool loadFirstSceneOnStart = true;

        private void Awake()
        {
            Ensure<SceneLoader>("SceneLoader");
            Ensure<StoryFlagManager>("StoryFlagManager");
            Ensure<ClueManager>("ClueManager");
            Ensure<EndingScoreTracker>("EndingScoreTracker");
            Ensure<EndingManager>("EndingManager");
            Ensure<SaveLoadManager>("SaveLoadManager");
            Ensure<ChapterManager>("ChapterManager");
            Ensure<StoryProgressionManager>("StoryProgressionManager");
            Ensure<MoodManager>("MoodManager");
            Ensure<AmbientSoundManager>("AmbientSoundManager");
            Ensure<UIManager>("UIManager");
            Ensure<NarrationManager>("NarrationManager");
            Ensure<DialogueManager>("DialogueManager");
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (loadFirstSceneOnStart && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Boot")
            {
                SceneLoader.Instance.LoadScene(firstPlayableScene, "Default");
            }
        }

        private static T Ensure<T>(string objectName) where T : Component
        {
            T existing = FindFirstObjectByType<T>();
            if (existing != null) return existing;
            GameObject created = new GameObject(objectName);
            DontDestroyOnLoad(created);
            return created.AddComponent<T>();
        }
    }
}
