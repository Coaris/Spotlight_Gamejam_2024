using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
        private static GameManager instance;
        public static GameManager Instance { get { return instance; } }

        [SerializeField] public static string saveSceneName;
        public bool isReburning;


        AudioSource audioSource;
        public AudioClip jumpSound;
        public AudioClip shootSound;


        [System.Serializable]
        struct SaveData {
                public string sceneName;
                public int maxHP;
        }

        const string PLAYER_DATA_FILE_NAME = "PlayerData.sav";


        private void Awake() {
                if (instance == null) instance = this;
                else Destroy(gameObject);
                DontDestroyOnLoad(instance);
        }

        private void Start() {
                audioSource = GetComponent<AudioSource>();
        }
        public void PlayJumpSound() {
                audioSource.PlayOneShot(jumpSound);
        }
        public void PlayShootSound() {
                audioSource.PlayOneShot(shootSound);
        }


        //加载游戏或重生时，重新加载场景
        public void LoadGameReburn() {
                isReburning = true;
                SceneManager.LoadScene(saveSceneName);
        }


        #region Save & Load
        public void SaveGame() {
                SaveSystem.SaveGame(PLAYER_DATA_FILE_NAME, SaveGameData());
        }

        public void LoadGame() {
                var saveData = SaveSystem.LoadGame<SaveData>(PLAYER_DATA_FILE_NAME);
                LoadGameData(saveData);
        }
        #endregion

        #region Utilities
        SaveData SaveGameData() {
                var saveData = new SaveData();

                saveData.sceneName = SceneManager.GetActiveScene().name;
                saveData.maxHP = Player.GetMaxHP();

                return saveData;
        }

        void LoadGameData(SaveData saveData) {
                PlayerStatusManager.Instance.WriteStatusOnReburn(saveData.maxHP);
                saveSceneName = saveData.sceneName;
        }
        #endregion


}
