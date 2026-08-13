using UnityEngine;
using Yiyang.Environment;

namespace Yiyang.CameraSystem
{
    [RequireComponent(typeof(CameraZone), typeof(Collider))]
    public sealed class CameraZoneTrigger : MonoBehaviour
    {
        private CameraZone zone;
        private void Awake()
        {
            zone = GetComponent<CameraZone>();
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            FindFirstObjectByType<CameraFollow2_5D>()?.ApplyZone(zone);
            if (zone.moodOverride != null) MoodManager.Instance?.ApplyMood(zone.moodOverride);
        }
    }
}
