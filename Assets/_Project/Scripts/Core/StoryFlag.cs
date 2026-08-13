using UnityEngine;

namespace Yiyang.Story
{
    [CreateAssetMenu(menuName = "Yiyang/Story Flag")]
    public sealed class StoryFlag : ScriptableObject
    {
        public string flagID;
        public string description;
    }
}
