using System.Linq;
using UnityEngine;
using Yiyang.SceneManagement;
using Yiyang.Story;

namespace Yiyang.Endings
{
    public sealed class EndingManager : MonoBehaviour
    {
        public static EndingManager Instance { get; private set; }
        public EndingData[] endings;
        public string fallbackEndingScene = "FinalRoom_Template";

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public EndingData Evaluate()
        {
            EndingScoreTracker s = EndingScoreTracker.Instance;
            return endings?.Where(e => e != null && IsValid(e, s)).OrderByDescending(e => e.priority).FirstOrDefault();
        }

        public void EvaluateAndLoadEnding()
        {
            EndingData ending = Evaluate();
            SceneLoader.Instance?.LoadScene(ending != null ? ending.targetEndingScene : fallbackEndingScene);
        }

        private static bool IsValid(EndingData e, EndingScoreTracker s)
        {
            StoryFlagManager flags = StoryFlagManager.Instance;
            if (flags != null && (!flags.HasAllFlags(e.requiredFlags) || flags.HasAnyFlag(e.blockedFlags))) return false;
            if (s == null) return true;
            return s.TruthScore >= e.minimumTruthScore &&
                   s.ViolenceScore >= e.minimumViolenceScore &&
                   s.EmpathyScore >= e.minimumEmpathyScore &&
                   s.EscapeScore >= e.minimumEscapeScore &&
                   s.SilenceScore >= e.minimumSilenceScore;
        }
    }
}
