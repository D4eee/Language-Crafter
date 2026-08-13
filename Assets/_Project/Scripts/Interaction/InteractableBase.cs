using UnityEngine;

namespace Yiyang.Interaction
{
    [RequireComponent(typeof(Collider))]
    public class InteractableBase : MonoBehaviour, IInteractable
    {
        [SerializeField] private string promptText = "Press E";
        [SerializeField] private bool oneShot;
        private bool used;

        public string PromptText => promptText;

        protected virtual void Reset()
        {
            Collider c = GetComponent<Collider>();
            c.isTrigger = true;
        }

        public virtual bool CanInteract(GameObject interactor) => !used || !oneShot;

        public void Interact(GameObject interactor)
        {
            if (!CanInteract(interactor)) return;
            used = true;
            OnInteract(interactor);
        }

        protected virtual void OnInteract(GameObject interactor) { }
    }
}
