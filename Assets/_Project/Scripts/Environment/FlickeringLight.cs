using UnityEngine;

namespace Yiyang.Environment
{
    [RequireComponent(typeof(Light))]
    public sealed class FlickeringLight : MonoBehaviour
    {
        public float baseIntensity = 1f;
        public float flickerAmount = 0.35f;
        public float flickerSpeed = 7f;
        private Light lightSource;

        private void Awake() => lightSource = GetComponent<Light>();
        private void Update()
        {
            float n = Mathf.PerlinNoise(Time.time * flickerSpeed, transform.position.x);
            lightSource.intensity = baseIntensity + (n - 0.5f) * flickerAmount;
        }
    }
}
