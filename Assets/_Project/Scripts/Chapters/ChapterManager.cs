using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yiyang.Story;

namespace Yiyang.Chapters
{
    public sealed class ChapterManager : MonoBehaviour
    {
        public static ChapterManager Instance { get; private set; }
        public List<ChapterData> chapters = new();
        public ChapterData CurrentChapter { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public ChapterData GetNextAvailableChapter()
        {
            return chapters.OrderBy(c => c.chapterOrder).FirstOrDefault(c => StoryFlagManager.Instance == null || StoryFlagManager.Instance.HasAllFlags(c.requiredFlags));
        }

        public void SetCurrentChapter(ChapterData chapter) => CurrentChapter = chapter;
    }
}
