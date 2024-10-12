using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
        [SerializeField] private SceneConfig sceneConfig;

        public void OnQuit() {
                Application.Quit();
        }

        #region ²Ëµ¥³¡¾°ÇÐ»»
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
        #endregion 

        
}
