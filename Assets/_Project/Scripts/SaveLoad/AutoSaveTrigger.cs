using UnityEngine;

namespace Yiyang.SaveLoad
{
    public sealed class AutoSaveTrigger : MonoBehaviour
    {
        public bool saveOnStart = true;
        private void Start()
        {
            if (saveOnStart) SaveLoadManager.Instance?.AutoSave();
        }
    }
}
