using UnityEngine;

namespace Yiyang.UI
{
    public sealed class PauseMenuUI : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        private bool paused;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) SetPaused(!paused);
        }

        public void SetPaused(bool value)
        {
            paused = value;
            Time.timeScale = paused ? 0f : 1f;
            if (canvasGroup != null)
            {
                canvasGroup.alpha = paused ? 1f : 0f;
                canvasGroup.blocksRaycasts = paused;
            }
        }
    }
}
