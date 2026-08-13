using System.Collections.Generic;

namespace Yiyang.SaveLoad
{
    [System.Serializable]
    public sealed class SaveData
    {
        public string currentSceneName;
        public string currentSpawnPoint;
        public List<string> storyFlags = new();
        public List<string> completedChapters = new();
        public List<string> collectedClues = new();
        public int truthScore;
        public int violenceScore;
        public int empathyScore;
        public int escapeScore;
        public int silenceScore;
    }
}
