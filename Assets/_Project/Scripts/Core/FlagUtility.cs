using System.Collections.Generic;

namespace Yiyang.Story
{
    public static class FlagUtility
    {
        public static bool HasAll(List<string> flags) => StoryFlagManager.Instance == null || StoryFlagManager.Instance.HasAllFlags(flags);
        public static bool HasAny(List<string> flags) => StoryFlagManager.Instance != null && StoryFlagManager.Instance.HasAnyFlag(flags);
        public static void SetMany(IEnumerable<string> flags)
        {
            if (flags == null) return;
            foreach (string flag in flags) StoryFlagManager.Instance?.SetFlag(flag);
        }
    }
}
