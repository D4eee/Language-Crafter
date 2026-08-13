using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Yiyang.Story
{
    public sealed class StoryFlagManager : MonoBehaviour
    {
        public static StoryFlagManager Instance { get; private set; }
        private readonly HashSet<string> flags = new();
        public IReadOnlyCollection<string> Flags => flags;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SetFlag(string flag)
        {
            if (string.IsNullOrWhiteSpace(flag)) return;
            if (flags.Add(flag)) Yiyang.SaveLoad.SaveLoadManager.Instance?.AutoSave();
        }

        public void RemoveFlag(string flag)
        {
            if (string.IsNullOrWhiteSpace(flag)) return;
            flags.Remove(flag);
        }

        public bool HasFlag(string flag) => !string.IsNullOrWhiteSpace(flag) && flags.Contains(flag);
        public bool HasAllFlags(IEnumerable<string> required) => required == null || required.All(HasFlag);
        public bool HasAnyFlag(IEnumerable<string> options) => options != null && options.Any(HasFlag);
        public void ReplaceFlags(IEnumerable<string> savedFlags)
        {
            flags.Clear();
            if (savedFlags == null) return;
            foreach (string flag in savedFlags) SetFlag(flag);
        }
    }
}
