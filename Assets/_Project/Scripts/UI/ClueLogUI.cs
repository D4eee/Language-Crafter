using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Yiyang.Interaction;

namespace Yiyang.UI
{
    public sealed class ClueLogUI : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public Text clueListText;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab)) Toggle();
        }

        public void Toggle()
        {
            bool visible = canvasGroup != null && canvasGroup.alpha < 0.5f;
            if (canvasGroup != null)
            {
                canvasGroup.alpha = visible ? 1f : 0f;
                canvasGroup.blocksRaycasts = visible;
            }
            if (visible) Refresh();
        }

        public void Refresh()
        {
            if (clueListText == null || ClueManager.Instance == null) return;
            StringBuilder sb = new StringBuilder("Collected Clues\n");
            foreach (string id in ClueManager.Instance.CollectedClueIDs) sb.AppendLine("- " + id);
            clueListText.text = sb.ToString();
        }
    }
}
