using UnityEngine;
using Yiyang.Interaction;

namespace Yiyang.SceneManagement
{
    public sealed class SceneTransition : InteractableBase
    {
        public string targetSceneName;
        public string targetSpawnPointID = "Default";
        public bool triggerOnEnter;

        private void OnTriggerEnter(Collider other)
        {
            if (triggerOnEnter && other.CompareTag("Player")) Load();
        }

        protected override void OnInteract(GameObject interactor) => Load();
        private void Load() => SceneLoader.Instance?.LoadScene(targetSceneName, targetSpawnPointID);
    }
}
