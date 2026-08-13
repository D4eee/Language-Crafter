using System.Collections.Generic;
using UnityEngine;

namespace Yiyang.Story
{
    [System.Serializable]
    public sealed class FlagCondition
    {
        public List<string> requiredFlags = new();
        public List<string> blockedIfFlags = new();

        public bool IsMet()
        {
            StoryFlagManager manager = StoryFlagManager.Instance;
            if (manager == null) return requiredFlags.Count == 0;
            return manager.HasAllFlags(requiredFlags) && !manager.HasAnyFlag(blockedIfFlags);
        }
    }
}
