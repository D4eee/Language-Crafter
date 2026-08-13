using UnityEngine;
using Yiyang.Dialogue;

namespace Yiyang.Interaction
{
    public sealed class DialogueTrigger : InteractableBase
    {
        public DialogueSequenceData dialogue;
        protected override void OnInteract(GameObject interactor)
        {
            if (dialogue != null) DialogueManager.Instance?.Play(dialogue);
        }
    }
}
