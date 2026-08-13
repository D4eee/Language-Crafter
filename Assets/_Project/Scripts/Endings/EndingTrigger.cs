using UnityEngine;
using Yiyang.Interaction;

namespace Yiyang.Endings
{
    public sealed class EndingTrigger : InteractableBase
    {
        public bool triggerOnEnter;

        private void OnTriggerEnter(Collider other)
        {
            if (triggerOnEnter && other.CompareTag("Player")) EndingManager.Instance?.EvaluateAndLoadEnding();
        }

        protected override void OnInteract(GameObject interactor)
        {
            EndingManager.Instance?.EvaluateAndLoadEnding();
        }
    }
}
