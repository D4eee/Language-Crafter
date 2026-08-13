using System.Collections.Generic;
using UnityEngine;

namespace Yiyang.Narration
{
    [CreateAssetMenu(menuName = "Yiyang/Narration Sequence")]
    public sealed class NarrationSequenceData : ScriptableObject
    {
        public string sequenceID;
        public List<NarrationLine> lines = new();
        public bool autoPlay;
        public bool canSkip = true;
        public List<string> setFlagsOnComplete = new();
        public bool loadSceneOnComplete;
        public string targetSceneName;
    }
}
