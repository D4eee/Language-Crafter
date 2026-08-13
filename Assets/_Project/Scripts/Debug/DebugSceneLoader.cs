using UnityEngine;
using Yiyang.SceneManagement;

namespace Yiyang.DebugTools
{
    public sealed class DebugSceneLoader : MonoBehaviour
    {
        public void LoadScene(string sceneName) => SceneLoader.Instance?.LoadScene(sceneName);
    }
}
