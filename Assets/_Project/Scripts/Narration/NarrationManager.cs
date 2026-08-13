using System.Collections;
using UnityEngine;
using Yiyang.SceneManagement;
using Yiyang.Story;

namespace Yiyang.Narration
{
    public sealed class NarrationManager : MonoBehaviour
    {
        public static NarrationManager Instance { get; private set; }
        public float textSpeed = 0.035f;
        private NarrationUI ui;
        private Coroutine routine;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void RegisterUI(NarrationUI narrationUI) => ui = narrationUI;

        public void Play(NarrationSequenceData sequence)
        {
            if (sequence == null) return;
            if (routine != null) StopCoroutine(routine);
            routine = StartCoroutine(PlayRoutine(sequence));
        }

        private IEnumerator PlayRoutine(NarrationSequenceData sequence)
        {
            if (ui == null) ui = FindFirstObjectByType<NarrationUI>();
            ui?.SetVisible(true);
            foreach (NarrationLine line in sequence.lines)
            {
                if (line.optionalMoodOverride != null) Yiyang.Environment.MoodManager.Instance?.ApplyMood(line.optionalMoodOverride);
                if (line.delayBefore > 0f) yield return new WaitForSeconds(line.delayBefore);
                yield return ui.TypeLine(line.text, textSpeed);
                if (line.requireInput)
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E));
                else if (line.displayDuration > 0f)
                    yield return new WaitForSeconds(line.displayDuration);
            }
            ui?.SetVisible(false);
            FlagUtility.SetMany(sequence.setFlagsOnComplete);
            if (sequence.loadSceneOnComplete) SceneLoader.Instance?.LoadScene(sequence.targetSceneName);
        }
    }
}
