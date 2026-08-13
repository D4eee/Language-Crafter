using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Yiyang.Chapters
{
    [CreateAssetMenu(menuName = "Yiyang/Scene Registry")]
    public sealed class SceneRegistry : ScriptableObject
    {
        public List<SceneData> scenes = new();
        public SceneData FindByUnitySceneName(string unitySceneName) => scenes.FirstOrDefault(s => s != null && s.unitySceneName == unitySceneName);
    }
}
