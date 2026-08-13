using System.Collections.Generic;
using UnityEngine;

namespace Yiyang.Interaction
{
    public sealed class ClueManager : MonoBehaviour
    {
        public static ClueManager Instance { get; private set; }
        private readonly HashSet<string> collected = new();
        public IReadOnlyCollection<string> CollectedClueIDs => collected;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void CollectClue(ClueData clue)
        {
            if (clue == null || string.IsNullOrWhiteSpace(clue.clueID)) return;
            if (collected.Add(clue.clueID)) Yiyang.SaveLoad.SaveLoadManager.Instance?.AutoSave();
        }

        public bool HasClue(string clueID) => collected.Contains(clueID);
    }
}
