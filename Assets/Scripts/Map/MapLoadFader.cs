using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MapLoadFader : MonoBehaviour {
        private Image fadeImage;
        [SerializeField] float fadeTime;


        private void Start() {
                fadeImage = GetComponent<Image>();
        }

        public void FadeIn() {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.g, 1);
                fadeImage.DOFade(0, fadeTime);
        }
        public void FadeOut() {
                fadeImage.enabled = true;
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.g, 0);
                fadeImage.DOFade(1, fadeTime);
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.g, 0);
        }
}
