using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yiyang.Endings;
using Yiyang.SceneManagement;
using Yiyang.Story;

namespace Yiyang.SaveLoad
{
    public sealed class SaveLoadManager : MonoBehaviour
    {
        public static SaveLoadManager Instance { get; private set; }
        public string FilePath => Path.Combine(Application.persistentDataPath, "yiyang_save.json");

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void AutoSave() => Save();

        public void Save()
        {
            SaveData data = new SaveData
            {
                currentSceneName = SceneManager.GetActiveScene().name,
                currentSpawnPoint = SceneLoader.Instance != null ? SceneLoader.Instance.CurrentSpawnPointID : "Default",
                storyFlags = StoryFlagManager.Instance?.Flags.ToList() ?? new(),
                collectedClues = Yiyang.Interaction.ClueManager.Instance?.CollectedClueIDs.ToList() ?? new()
            };
            EndingScoreTracker scores = EndingScoreTracker.Instance;
            if (scores != null)
            {
                data.truthScore = scores.TruthScore;
                data.violenceScore = scores.ViolenceScore;
                data.empathyScore = scores.EmpathyScore;
                data.escapeScore = scores.EscapeScore;
                data.silenceScore = scores.SilenceScore;
            }
            File.WriteAllText(FilePath, JsonUtility.ToJson(data, true));
        }

        public bool Load()
        {
            if (!File.Exists(FilePath)) return false;
            SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(FilePath));
            StoryFlagManager.Instance?.ReplaceFlags(data.storyFlags);
            EndingScoreTracker.Instance?.SetScores(data.truthScore, data.violenceScore, data.empathyScore, data.escapeScore, data.silenceScore);
            SceneLoader.Instance?.LoadScene(data.currentSceneName, data.currentSpawnPoint);
            return true;
        }
    }
}
