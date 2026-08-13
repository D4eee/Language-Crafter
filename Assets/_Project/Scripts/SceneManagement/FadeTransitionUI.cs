using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Yiyang.SceneManagement
{
    public class FadeTransitionUI : MonoBehaviour
    {
        public static FadeTransitionUI Instance { get; private set; }
        public Image fadeImage;
        public float fadeDuration = 0.35f;

        private void Awake()
        {
            Instance = this;
        }

        public IEnumerator FadeOut() => Fade(1f);
        public IEnumerator FadeIn() => Fade(0f);

        private IEnumerator Fade(float target)
        {
            if (fadeImage == null) yield break;
            Color start = fadeImage.color;
            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.unscaledDeltaTime;
                float a = Mathf.Lerp(start.a, target, t / fadeDuration);
                fadeImage.color = new Color(start.r, start.g, start.b, a);
                yield return null;
            }
            fadeImage.color = new Color(start.r, start.g, start.b, target);
        }
    }
}
