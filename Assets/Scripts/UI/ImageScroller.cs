using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScroller : MonoBehaviour {
        [SerializeField] private RawImage img;
        [SerializeField][Range(-1, 1)] private float xSpeed, ySpeed;

        private void Update() {
                img.uvRect = new Rect(img.uvRect.position + new Vector2(xSpeed, ySpeed) * Time.deltaTime, img.uvRect.size);
        }
}
