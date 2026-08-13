using UnityEngine;

namespace Yiyang.Environment
{
    public sealed class ParallaxLayer : MonoBehaviour
    {
        public float parallaxStrength = 0.15f;
        private Transform cam;
        private Vector3 startPos;
        private Vector3 camStart;

        private void Start()
        {
            cam = Camera.main != null ? Camera.main.transform : null;
            startPos = transform.position;
            camStart = cam != null ? cam.position : Vector3.zero;
        }

        private void LateUpdate()
        {
            if (cam == null) return;
            Vector3 delta = cam.position - camStart;
            transform.position = startPos + new Vector3(delta.x * parallaxStrength, delta.y * parallaxStrength, 0f);
        }
    }
}
