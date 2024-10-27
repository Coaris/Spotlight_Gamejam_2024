using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour {
        [SerializeField] Transform sprite;

        [SerializeField] Transform openPosition;
        private Vector3 closePosition;
        public void OnCloseDoor() {
                //sprite.DOMove(Vector3.zero, 1).SetEase(Ease.InExpo);
                sprite.DOLocalMove(Vector3.zero, 0.3f).SetEase(Ease.InExpo);
        }
        public void OnOpenDoor() {
                sprite.DOMove(openPosition.position, 0.3f).SetEase(Ease.InExpo);
        }
}