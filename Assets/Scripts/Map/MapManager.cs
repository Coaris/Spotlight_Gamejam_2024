using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
        public static MapManager Instance { get; private set; }

        [HideInInspector] public string portalFrom;

        private void Awake() {
                if (Instance != null && Instance != this) {
                        Destroy(gameObject);
                }
                else {
                        Instance = this;
                }
                DontDestroyOnLoad(gameObject);
        }

}
