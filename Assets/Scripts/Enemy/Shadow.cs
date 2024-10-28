using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour {

        [SerializeField] private List<Collider2D> colliders;

        private void OnTriggerEnter2D(Collider2D collision) {

                if (collision.CompareTag("Player")) {
                        CloseColliders();
                        collision.transform.GetComponent<Player>().Damage(1, transform.position.x - collision.transform.position.x);
                }
        }
        public void OpenColliders() {
                foreach (var collider in colliders) {
                        collider.enabled = true;
                }
        }
        public void CloseColliders() {
                foreach (var collider in colliders) {
                        collider.enabled = false;
                }
        }
}