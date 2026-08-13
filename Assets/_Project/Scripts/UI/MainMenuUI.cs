using UnityEngine;
using Yiyang.SaveLoad;
using Yiyang.SceneManagement;

namespace Yiyang.UI
{
    public sealed class MainMenuUI : MonoBehaviour
    {
        public string newGameScene = "Prototype_Hallway";
        public void NewGame() => SceneLoader.Instance?.LoadScene(newGameScene, "Default");
        public void Continue() => SaveLoadManager.Instance?.Load();
        public void Quit() => Application.Quit();
    }
}
