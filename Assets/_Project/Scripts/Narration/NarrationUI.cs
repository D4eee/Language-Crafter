using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Yiyang.Narration
{
    public sealed class NarrationUI : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public Text narrationText;

        private void Awake()
        {
            NarrationManager.Instance?.RegisterUI(this);
            SetVisible(false);
        }

        public void SetVisible(bool visible)
        {
            if (canvasGroup != null) canvasGroup.alpha = visible ? 1f : 0f;
        }

        public IEnumerator TypeLine(string text, float speed)
        {
            if (narrationText == null) yield break;
            narrationText.text = string.Empty;
            foreach (char c in text ?? string.Empty)
            {
                narrationText.text += c;
                if (Input.GetKey(KeyCode.Space))
                {
                    narrationText.text = text;
                    break;
                }
                yield return new WaitForSeconds(speed);
            }
        }
    }
}
