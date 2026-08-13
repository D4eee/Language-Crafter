using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yiyang.Endings;
using Yiyang.Story;

namespace Yiyang.DebugTools
{
    public sealed class DebugGamePanel : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public Text output;
        private bool visible;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1)) SetVisible(!visible);
            if (!visible || output == null) return;
            EndingScoreTracker s = EndingScoreTracker.Instance;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Scene: " + SceneManager.GetActiveScene().name);
            if (s != null) sb.AppendLine($"Scores T:{s.TruthScore} V:{s.ViolenceScore} E:{s.EmpathyScore} X:{s.EscapeScore} S:{s.SilenceScore}");
            sb.AppendLine("Flags:");
            if (StoryFlagManager.Instance != null)
                foreach (string flag in StoryFlagManager.Instance.Flags) sb.AppendLine("- " + flag);
            output.text = sb.ToString();
        }

        public void SetVisible(bool value)
        {
            visible = value;
            if (canvasGroup != null)
            {
                canvasGroup.alpha = visible ? 1f : 0f;
                canvasGroup.blocksRaycasts = visible;
            }
        }
    }
}
