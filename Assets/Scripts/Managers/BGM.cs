using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {

        private static BGM instance;
        public static BGM Instance { get { return instance; } }


        AudioSource audioSource;
        public AudioClip normalBGM;
        public AudioClip bossBGM;
        private void Awake() {
                if (instance == null) instance = this;
                else Destroy(gameObject);
                DontDestroyOnLoad(instance);
        }


        void Start() {
                audioSource = GetComponent<AudioSource>();
        }

        public void ChangeToBossBGM() {
                
                audioSource.clip = bossBGM;
                audioSource.volume = 0.5f;
                audioSource.Play();
        }
        public void ChangeToNormalBGM() {

                audioSource.clip = normalBGM;
                audioSource.volume = 1;
                audioSource.Play();
        }
}
