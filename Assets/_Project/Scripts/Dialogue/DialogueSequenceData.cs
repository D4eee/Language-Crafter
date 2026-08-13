using System.Collections.Generic;
using UnityEngine;

namespace Yiyang.Dialogue
{
    [CreateAssetMenu(menuName = "Yiyang/Dialogue Sequence")]
    public sealed class DialogueSequenceData : ScriptableObject
    {
        public string sequenceID;
        public string startingNodeID = "start";
        public List<DialogueNode> nodes = new();
    }
}
