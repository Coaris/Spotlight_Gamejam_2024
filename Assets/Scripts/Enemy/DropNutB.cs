using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropNutB : EnemyBase, IExplode {

        private bool isDroping;
        [SerializeField] Collider2D explodeCheck;
        [SerializeField] float timer;

        private void Start() {
                //timer = Random.Range(1, 2);
                timer=Random.Range(0.7f, 2f);
                CheckFaceDirection(Random.Range(0, 2) == 0);
                anim.SetTrigger("Drop");
        }

        void Update() {
                if (isDead) {
                        rb.velocity = Vector2.zero;
                        return;
                }
                timer-=Time.deltaTime;
                if (timer <= 0) {
                        rb.gravityScale = 5f;
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
