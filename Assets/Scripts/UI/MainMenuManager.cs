using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
        [SerializeField] private SceneConfig sceneConfig;

        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject settings;
        [SerializeField] private GameObject credits;

        public void OnQuit() {
                Application.Quit();
        }

        #region ²Ëµ¥³¡¾°ÇÐ»»
        public void OnNewGame() {
                PlayerStatusManager.Instance.SetStatusNewGame();
                SceneManager.LoadScene(sceneConfig.NewGame.name);
        }

        public void OnLoadGame() {
                GameManager.Instance.LoadGame();
                GameManager.Instance.LoadGameReburn();
        }
        #endregion

        #region ²Ëµ¥ÇÐ»»
        public void OnOpenSettings() {
                mainMenu.SetActive(false);
                settings.SetActive(true);
        }
        public void OnOpenCredits() {
                mainMenu.SetActive(false);
                credits.SetActive(true);
        }
        public void OnBackToMain() {
                settings.SetActive(false);
                credits.SetActive(false);
                mainMenu.SetActive(true);
        }
        #endregion


}
