using UnityEngine;

namespace Yiyang.Interaction
{
    [CreateAssetMenu(menuName = "Yiyang/Clue")]
    public sealed class ClueData : ScriptableObject
    {
        public string clueID;
        public string title;
        [TextArea(3, 8)] public string description;
        public Sprite optionalImage;
        public string associatedFlag;
    }
}
