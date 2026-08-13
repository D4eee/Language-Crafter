using System.Collections.Generic;
using UnityEngine;
using Yiyang.Environment;

namespace Yiyang.Chapters
{
    [CreateAssetMenu(menuName = "Yiyang/Chapter Data")]
    public sealed class ChapterData : ScriptableObject
    {
        public string chapterID;
        public string chapterName;
        [TextArea(2, 6)] public string description;
        public List<SceneData> sceneList = new();
        public SceneData startingScene;
        public List<string> requiredFlags = new();
        public MoodProfile chapterMood;
        public int chapterOrder;
    }
}
