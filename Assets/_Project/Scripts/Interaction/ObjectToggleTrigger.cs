using UnityEngine;

namespace Yiyang.Interaction
{
    public sealed class ObjectToggleTrigger : InteractableBase
    {
        public GameObject[] enableObjects;
        public GameObject[] disableObjects;

        protected override void OnInteract(GameObject interactor)
        {
            foreach (GameObject go in enableObjects) if (go != null) go.SetActive(true);
            foreach (GameObject go in disableObjects) if (go != null) go.SetActive(false);
        }
    }
}
