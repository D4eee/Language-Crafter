using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yiyang.SceneManagement
{
    public sealed class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }
        public string LastSceneName { get; private set; }
        public string CurrentSpawnPointID { get; private set; } = "Default";

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void LoadScene(string targetSceneName, string targetSpawnPointID = "Default")
        {
            if (string.IsNullOrWhiteSpace(targetSceneName)) return;
            StartCoroutine(LoadRoutine(targetSceneName, targetSpawnPointID));
        }

        private IEnumerator LoadRoutine(string sceneName, string spawnPointID)
        {
            LastSceneName = SceneManager.GetActiveScene().name;
            CurrentSpawnPointID = string.IsNullOrWhiteSpace(spawnPointID) ? "Default" : spawnPointID;
            if (FadeTransitionUI.Instance != null) yield return FadeTransitionUI.Instance.FadeOut();
            yield return SceneManager.LoadSceneAsync(sceneName);
            if (FadeTransitionUI.Instance != null) yield return FadeTransitionUI.Instance.FadeIn();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneSpawnPoint[] points = FindObjectsByType<SceneSpawnPoint>(FindObjectsSortMode.None);
            SceneSpawnPoint target = null;
            foreach (SceneSpawnPoint point in points)
                if (point.spawnPointID == CurrentSpawnPointID) { target = point; break; }
            if (target == null && points.Length > 0) target = points[0];

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null && target != null)
            {
                player.transform.SetPositionAndRotation(target.transform.position, target.transform.rotation);
            }
            Yiyang.SaveLoad.SaveLoadManager.Instance?.AutoSave();
        }
    }
}
