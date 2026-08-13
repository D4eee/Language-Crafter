using UnityEngine;

namespace Yiyang.CameraSystem
{
    public sealed class CameraFollow2_5D : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new(0f, 2.2f, -9f);
        public float smoothing = 4.5f;
        public bool useOrthographic = true;
        public float orthographicSize = 4.6f;

        private Camera cam;

        private void Awake()
        {
            cam = GetComponentInChildren<Camera>();
            if (cam == null) cam = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            if (target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null) target = player.transform;
            }
            if (target == null) return;

            transform.position = Vector3.Lerp(transform.position, target.position + offset, 1f - Mathf.Exp(-smoothing * Time.deltaTime));
            transform.LookAt(target.position + Vector3.up * 1.1f);
            if (cam != null)
            {
                cam.orthographic = useOrthographic;
                if (useOrthographic) cam.orthographicSize = orthographicSize;
            }
        }

        public void ApplyZone(CameraZone zone)
        {
            if (zone == null) return;
            offset = zone.cameraOffset;
            smoothing = zone.followSmoothing;
            orthographicSize = zone.orthographicSize;
        }
    }
}
