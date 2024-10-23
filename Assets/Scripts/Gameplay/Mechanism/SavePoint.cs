using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {

        private void Start() {
                if (GameManager.Instance.isReburning) {
                        PlayerController.Instance.transform.position = transform.position;
                        GameManager.Instance.isReburning = false;
                }
        }

        [SerializeField] private SpriteRenderer indicator;

        private void OnTriggerEnter2D(Collider2D collision) {
                if (collision.CompareTag("Player")) {
                        indicator.enabled = true;
                        collision.gameObject.GetComponent<PlayerController>().interactType = InteractType.SavePoint;
                }
        }
        private void OnTriggerExit2D(Collider2D collision) {
                if (collision.CompareTag("Player")) {
                        indicator.enabled = false;
                        collision.gameObject.GetComponent<PlayerController>().interactType = InteractType.None;
                }
        }
}