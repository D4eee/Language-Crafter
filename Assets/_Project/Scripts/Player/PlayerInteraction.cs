using System.Collections.Generic;
using UnityEngine;
using Yiyang.Interaction;
using Yiyang.UI;

namespace Yiyang.Player
{
    public sealed class PlayerInteraction : MonoBehaviour
    {
        private readonly List<IInteractable> nearby = new();

        private void Update()
        {
            IInteractable current = GetCurrent();
            InteractionPromptUI.Instance?.SetPrompt(current != null, current?.PromptText ?? string.Empty);
            if (current != null && Input.GetKeyDown(KeyCode.E))
            {
                current.Interact(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable) && !nearby.Contains(interactable))
            {
                nearby.Add(interactable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                nearby.Remove(interactable);
            }
        }

        private IInteractable GetCurrent()
        {
            nearby.RemoveAll(i => i == null || !i.CanInteract(gameObject));
            return nearby.Count > 0 ? nearby[nearby.Count - 1] : null;
        }
    }
}
