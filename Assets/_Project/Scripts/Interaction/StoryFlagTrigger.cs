using UnityEngine;
using Yiyang.Story;

namespace Yiyang.Interaction
{
    public sealed class StoryFlagTrigger : InteractableBase
    {
        public string[] setFlags;
        public bool triggerOnEnter;

        private void OnTriggerEnter(Collider other)
        {
            if (triggerOnEnter && other.CompareTag("Player")) SetFlags();
        }

        protected override void OnInteract(GameObject interactor) => SetFlags();

        private void SetFlags()
        {
            foreach (string flag in setFlags)
                StoryFlagManager.Instance?.SetFlag(flag);
        }
    }
}
