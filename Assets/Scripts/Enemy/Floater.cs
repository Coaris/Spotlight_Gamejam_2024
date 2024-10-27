using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : EnemyBase {

        [SerializeField] private float speed;
        [SerializeField] private bool isUping;
        [SerializeField] private bool isRighting;

        private Vector2 move;

        private void Start() {
                if (isRighting) {
                        move.x = speed;
                }
                else {
                        move.x = -speed;
                }

                if (isUping) {
                        move.y = speed;
                }
                else {
                        move.y = -speed;
                }

        }
        private void Update() {
                if (isDead) {
                        rb.velocity = Vector2.zero;
                        return;
                }

                base.Update();

                Patrol();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
                if (collision.CompareTag("Player") || collision.CompareTag("Ground") || collision.CompareTag("Enemy")) {
                        Vector2 touchPoint = collision.bounds.ClosestPoint(transform.position);
                        Bounce(touchPoint);
                }
        }

        private void Bounce(Vector2 _touchPoint) {
                Vector2 relativePoint = _touchPoint - (Vector2)transform.position;
                if (Mathf.Abs(relativePoint.x) >= Mathf.Abs(relativePoint.y)) {
                        if ((relativePoint.x > 0 && isRighting) || (relativePoint.x < 0 && !isRighting)) {
                                //左右反弹
                                move.x *= -1;
                                isRighting = !isRighting;
                        }
                }
                else {
                        if ((relativePoint.y > 0 && isUping) || (relativePoint.y < 0 && !isUping)) {
                                //上下反弹
                                move.y *= -1;
                                isUping = !isUping;
                        }
                }
        }
        private void Patrol() {
                rb.velocity = move;
        }
}
