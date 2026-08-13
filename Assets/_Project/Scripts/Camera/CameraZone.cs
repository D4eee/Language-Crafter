using UnityEngine;
using Yiyang.Environment;

namespace Yiyang.CameraSystem
{
    public sealed class CameraZone : MonoBehaviour
    {
        public Vector3 cameraOffset = new(0f, 2.2f, -9f);
        public float orthographicSize = 4.6f;
        public float followSmoothing = 4.5f;
        public float backgroundDepthFeeling = 1f;
        [Range(0f, 1f)] public float vignetteIntensity = 0.45f;
        public MoodProfile moodOverride;
    }
}
