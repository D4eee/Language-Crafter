using UnityEngine;
using Yiyang.Narration;

namespace Yiyang.Interaction
{
    public sealed class NarrationTrigger : InteractableBase
    {
        public NarrationSequenceData sequence;
        public bool triggerOnEnter;

        private void OnTriggerEnter(Collider other)
        {
            if (triggerOnEnter && other.CompareTag("Player")) Play();
        }

        protected override void OnInteract(GameObject interactor) => Play();

        private void Play()
        {
            if (sequence != null) NarrationManager.Instance?.Play(sequence);
        }
    }
}
