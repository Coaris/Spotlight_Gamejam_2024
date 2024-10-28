using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropNutB : EnemyBase, IExplode {

        private bool isDroping;
        [SerializeField] Collider2D explodeCheck;

        private void Start() {
                CheckFaceDirection(Random.Range(0, 2) == 0);
                anim.SetTrigger("Drop");
        }

        void Update() {
                if (isDead) {
                        rb.velocity = Vector2.zero;
                        return;
                }
        }

        private void OnCollisionEnter2D(Collision2D collision) {
                if (collision.transform.CompareTag("Ground")) {
                        anim.SetTrigger("Dead");
                }
        }
        public void OnExplode() {
                explodeCheck.enabled = true;
        }
}
