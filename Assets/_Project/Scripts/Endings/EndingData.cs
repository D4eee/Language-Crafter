using System.Collections.Generic;
using UnityEngine;

namespace Yiyang.Endings
{
    [CreateAssetMenu(menuName = "Yiyang/Ending")]
    public sealed class EndingData : ScriptableObject
    {
        public string endingID;
        public string endingName;
        [TextArea(2, 6)] public string endingDescription;
        public List<string> requiredFlags = new();
        public List<string> blockedFlags = new();
        public int minimumTruthScore;
        public int minimumViolenceScore;
        public int minimumEmpathyScore;
        public int minimumEscapeScore;
        public int minimumSilenceScore;
        public string targetEndingScene;
        public int priority;
    }
}
