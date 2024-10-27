using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour {
        [SerializeField] Transform root;

        private void OnTriggerEnter2D(Collider2D collision) {
                if (collision.CompareTag("Player")&&CompareTag("PlayerCheck")) {
                        root.GetComponent<IPlayerCheck>().OnPlayerDetected();
                }
        }
        private void OnTriggerExit2D(Collider2D collision) {
                if (collision.CompareTag("Player")&&CompareTag("PlayerCheck")) {
                        root.GetComponent<IPlayerCheck>().OnPlayerLost();
                }
        }
}