using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
        private PlayerController instance;
        public PlayerController Instance { get { return instance; } }

        private void Awake() {
                if (instance == null) instance = this;
                else Destroy(gameObject);
                DontDestroyOnLoad(gameObject);
        }
}
