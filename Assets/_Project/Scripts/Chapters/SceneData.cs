using System.Collections.Generic;
using UnityEngine;
using Yiyang.Environment;

namespace Yiyang.Chapters
{
    [CreateAssetMenu(menuName = "Yiyang/Scene Data")]
    public sealed class SceneData : ScriptableObject
    {
        public string sceneID;
        public string sceneName;
        public string unitySceneName;
        public SceneType sceneType;
        [TextArea(2, 6)] public string description;
        public MoodProfile defaultMood;
        public List<string> possibleTransitions = new();
        public List<string> importantFlags = new();
        public string ambienceID;
    }
}
