using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {

        [SerializeField] private Sprite s1;
        [SerializeField] private Sprite s2;
        private void Start() {
                if (GameManager.Instance.isReburning) {
                        PlayerController.Instance.transform.position = transform.position;
                        GameManager.Instance.isReburning = false;
                }
        }

        [SerializeField] private SpriteRenderer indicator;


        public void ChangeSprite() {
                GetComponent<SpriteRenderer>().sprite = s2;
        }
        private void OnTriggerEnter2D(Collider2D collision) {
                if (collision.CompareTag("Player")) {
                        indicator.enabled = true;
                        collision.gameObject.GetComponent<PlayerController>().interactType = InteractType.SavePoint;
                        collision.gameObject.GetComponent<PlayerController>().currentSavePoint = this;
                }
        }
        private void OnTriggerExit2D(Collider2D collision) {
                if (collision.CompareTag("Player")) {
                        indicator.enabled = false;
                        collision.gameObject.GetComponent<PlayerController>().interactType = InteractType.None;
                        collision.gameObject.GetComponent<PlayerController>().currentSavePoint = null;
                }
        }
}