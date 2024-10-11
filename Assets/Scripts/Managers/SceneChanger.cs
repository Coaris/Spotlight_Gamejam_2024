using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
        [SerializeField] private SceneConfig sceneConfig;

        public void OnLoadMainMenu() {
                SceneManager.LoadScene(sceneConfig.MainMenu.name);
        }
        public void OnLoadSettings() {
                SceneManager.LoadScene(sceneConfig.Settings.name);
        }
        public void OnLoadCredits() {
                SceneManager.LoadScene(sceneConfig.Credits.name);
        }
        public void OnLoadNewGame() {
                SceneManager.LoadScene(sceneConfig.NewGame.name);
        }
        public void OnQuit() {
                Application.Quit();
        }
}
