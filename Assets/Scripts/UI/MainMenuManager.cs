using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
        [SerializeField] private SceneConfig sceneConfig;

        [SerializeField] private GameObject mainMenu;

        public void OnQuit() {
                Application.Quit();
        }

        #region ²Ëµ¥³¡¾°ÇÐ»»
        public void OnNewGame() {
                PlayerStatusManager.Instance.SetStatusNewGame();
                SceneManager.LoadScene(sceneConfig.NewGameName);
        }

        public void OnLoadGame() {
                GameManager.Instance.LoadGame();
                GameManager.Instance.LoadGameReburn();
        }
        #endregion

        //#region ²Ëµ¥ÇÐ»»
        //public void OnOpenSettings() {
        //        mainMenu.SetActive(false);
        //}
        //public void OnOpenCredits() {
        //        mainMenu.SetActive(false);
        //}
        //public void OnBackToMain() {
        //        mainMenu.SetActive(true);
        //}
        //#endregion


}
