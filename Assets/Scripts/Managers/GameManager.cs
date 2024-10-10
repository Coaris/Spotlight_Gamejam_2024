using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
        private static GameManager instance;
        public static GameManager Instance { get { return instance; } }

        private void Awake() {
                if (instance == null) instance = this;
                else Destroy(gameObject);
                DontDestroyOnLoad(instance);
        }
        
        private void LoadScene(string sceneName) {
                SceneManager.LoadScene(sceneName);
        }
}
