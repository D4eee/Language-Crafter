using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Yiyang.Dialogue
{
    public sealed class DialogueUI : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public Text speakerText;
        public Text lineText;
        public Transform choicesRoot;
        public Button choiceButtonPrefab;
        public Button continueButton;

        private void Awake()
        {
            DialogueManager.Instance?.RegisterUI(this);
            if (continueButton != null) continueButton.onClick.AddListener(() => DialogueManager.Instance?.Continue());
            SetVisible(false);
        }

        public void SetVisible(bool visible)
        {
            if (canvasGroup != null) canvasGroup.alpha = visible ? 1f : 0f;
            if (canvasGroup != null) canvasGroup.blocksRaycasts = visible;
        }

        public void Show(DialogueNode node, List<DialogueChoice> choices)
        {
            SetVisible(true);
            if (speakerText != null) speakerText.text = node.speakerName;
            if (lineText != null) lineText.text = node.line;
            if (choicesRoot != null)
                foreach (Transform child in choicesRoot) Destroy(child.gameObject);
            bool hasChoices = choices != null && choices.Count > 0;
            if (continueButton != null) continueButton.gameObject.SetActive(!hasChoices);
            if (!hasChoices || choiceButtonPrefab == null || choicesRoot == null) return;
            foreach (DialogueChoice choice in choices)
            {
                Button b = Instantiate(choiceButtonPrefab, choicesRoot);
                b.gameObject.SetActive(true);
                Text t = b.GetComponentInChildren<Text>();
                if (t != null) t.text = choice.choiceText;
                b.onClick.AddListener(() => DialogueManager.Instance?.Choose(choice));
            }
        }
    }
}
