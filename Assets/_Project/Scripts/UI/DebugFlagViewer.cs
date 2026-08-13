using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Yiyang.Story;

namespace Yiyang.UI
{
    public sealed class DebugFlagViewer : MonoBehaviour
    {
        public Text output;
        private void Update()
        {
            if (output == null || StoryFlagManager.Instance == null) return;
            StringBuilder sb = new StringBuilder("Flags\n");
            foreach (string flag in StoryFlagManager.Instance.Flags) sb.AppendLine(flag);
            output.text = sb.ToString();
        }
    }
}
