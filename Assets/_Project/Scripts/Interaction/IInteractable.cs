using UnityEngine;

namespace Yiyang.Interaction
{
    public interface IInteractable
    {
        string PromptText { get; }
        bool CanInteract(GameObject interactor);
        void Interact(GameObject interactor);
    }
}
