using UnityEngine;
using UnityEngine.UI;

namespace Yiyang.UI
{
    public sealed class InteractionPromptUI : MonoBehaviour
    {
        public static InteractionPromptUI Instance { get; private set; }
        public CanvasGroup canvasGroup;
        public Text promptText;

        private void Awake()
        {
            Instance = this;
        }

        public void SetPrompt(bool visible, string text)
        {
            if (canvasGroup != null) canvasGroup.alpha = visible ? 1f : 0f;
            if (promptText != null) promptText.text = string.IsNullOrWhiteSpace(text) ? "Press E" : text;
        }
    }
}
