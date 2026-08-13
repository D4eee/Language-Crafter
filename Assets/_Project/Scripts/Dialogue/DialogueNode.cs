using System.Collections.Generic;
using UnityEngine;

namespace Yiyang.Dialogue
{
    [System.Serializable]
    public sealed class DialogueNode
    {
        public string nodeID;
        public string speakerName;
        [TextArea(2, 5)] public string line;
        public List<DialogueChoice> choices = new();
        public string nextNodeID;
        public bool triggerEndingCheck;
        public string targetSceneName;
    }
}
