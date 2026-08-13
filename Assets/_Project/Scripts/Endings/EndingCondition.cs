using System.Collections.Generic;

namespace Yiyang.Endings
{
    [System.Serializable]
    public sealed class EndingCondition
    {
        public List<string> requiredFlags = new();
        public List<string> blockedFlags = new();
        public int minimumTruthScore;
        public int minimumViolenceScore;
        public int minimumEmpathyScore;
        public int minimumEscapeScore;
        public int minimumSilenceScore;
    }
}
