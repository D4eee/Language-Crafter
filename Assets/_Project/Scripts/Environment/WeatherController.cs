using UnityEngine;

namespace Yiyang.Environment
{
    public sealed class WeatherController : MonoBehaviour
    {
        public ParticleSystem rainParticles;
        public GameObject fogVolume;

        public void SetRain(bool active)
        {
            if (rainParticles == null) return;
            if (active) rainParticles.Play();
            else rainParticles.Stop();
        }

        public void SetFogVolume(bool active)
        {
            if (fogVolume != null) fogVolume.SetActive(active);
        }
    }
}
