using UnityEngine;
using Yiyang.Narration;
using Yiyang.Story;

namespace Yiyang.Interaction
{
    public sealed class InspectableClue : InteractableBase
    {
        public ClueData clue;
        public NarrationSequenceData narration;

        protected override void OnInteract(GameObject interactor)
        {
            if (clue != null)
            {
                ClueManager.Instance?.CollectClue(clue);
                if (!string.IsNullOrWhiteSpace(clue.associatedFlag))
                    StoryFlagManager.Instance?.SetFlag(clue.associatedFlag);
            }
            if (narration != null) NarrationManager.Instance?.Play(narration);
        }
    }
}
