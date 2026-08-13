using System.Collections.Generic;

namespace Yiyang.Dialogue
{
    [System.Serializable]
    public sealed class DialogueChoice
    {
        public string choiceText;
        public string nextNodeID;
        public List<string> setFlags = new();
        public List<string> requiredFlags = new();
        public List<string> blockedIfFlags = new();
        public int truthPoints;
        public int violencePoints;
        public int empathyPoints;
        public int escapePoints;
        public int silencePoints;
    }
}
