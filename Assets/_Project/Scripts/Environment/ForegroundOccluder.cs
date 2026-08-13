using UnityEngine;

namespace Yiyang.Environment
{
    public sealed class ForegroundOccluder : MonoBehaviour
    {
        public float alpha = 0.65f;
        private void Start()
        {
            Renderer r = GetComponent<Renderer>();
            if (r != null && r.material != null)
            {
                Color c = r.material.color;
                r.material.color = new Color(c.r, c.g, c.b, alpha);
            }
        }
    }
}
