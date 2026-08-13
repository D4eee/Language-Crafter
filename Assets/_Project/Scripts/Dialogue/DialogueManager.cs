using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yiyang.Endings;
using Yiyang.SceneManagement;
using Yiyang.Story;

namespace Yiyang.Dialogue
{
    public sealed class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }
        private DialogueUI ui;
        private DialogueSequenceData activeSequence;
        private DialogueNode activeNode;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void RegisterUI(DialogueUI dialogueUI) => ui = dialogueUI;

        public void Play(DialogueSequenceData sequence)
        {
            activeSequence = sequence;
            ShowNode(sequence?.startingNodeID);
        }

        public void Choose(DialogueChoice choice)
        {
            if (choice == null) return;
            FlagUtility.SetMany(choice.setFlags);
            EndingScoreTracker.Instance?.Add(choice.truthPoints, choice.violencePoints, choice.empathyPoints, choice.escapePoints, choice.silencePoints);
            ShowNode(choice.nextNodeID);
        }

        public void Continue()
        {
            if (activeNode == null) return;
            if (activeNode.triggerEndingCheck) EndingManager.Instance?.EvaluateAndLoadEnding();
            else if (!string.IsNullOrWhiteSpace(activeNode.targetSceneName)) SceneLoader.Instance?.LoadScene(activeNode.targetSceneName);
            else ShowNode(activeNode.nextNodeID);
        }

        private void ShowNode(string nodeID)
        {
            if (activeSequence == null) return;
            activeNode = activeSequence.nodes.FirstOrDefault(n => n.nodeID == nodeID);
            if (activeNode == null)
            {
                ui?.SetVisible(false);
                return;
            }
            if (ui == null) ui = FindFirstObjectByType<DialogueUI>();
            IEnumerable<DialogueChoice> choices = activeNode.choices.Where(c => StoryFlagManager.Instance == null ||
                (StoryFlagManager.Instance.HasAllFlags(c.requiredFlags) && !StoryFlagManager.Instance.HasAnyFlag(c.blockedIfFlags)));
            ui?.Show(activeNode, choices.ToList());
        }
    }
}
