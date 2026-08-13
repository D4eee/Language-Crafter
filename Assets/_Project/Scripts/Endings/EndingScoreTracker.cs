using UnityEngine;

namespace Yiyang.Endings
{
    public sealed class EndingScoreTracker : MonoBehaviour
    {
        public static EndingScoreTracker Instance { get; private set; }
        public int TruthScore { get; private set; }
        public int ViolenceScore { get; private set; }
        public int EmpathyScore { get; private set; }
        public int EscapeScore { get; private set; }
        public int SilenceScore { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Add(int truth, int violence, int empathy, int escape, int silence)
        {
            TruthScore += truth;
            ViolenceScore += violence;
            EmpathyScore += empathy;
            EscapeScore += escape;
            SilenceScore += silence;
        }

        public void SetScores(int truth, int violence, int empathy, int escape, int silence)
        {
            TruthScore = truth;
            ViolenceScore = violence;
            EmpathyScore = empathy;
            EscapeScore = escape;
            SilenceScore = silence;
        }
    }
}
