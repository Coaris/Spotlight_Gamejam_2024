using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

        private Rigidbody2D rb;
        private PlayerMovement playerMovement;
        [SerializeField] float backForce = 1;
        [SerializeField] float backForceTime = 0.3f;

        private void Start() {
                rb = GetComponent<Rigidbody2D>();
                playerMovement = GetComponent<PlayerMovement>();
        }
        public void Attack() {
                if (playerMovement.IsSliding) return;

                //子弹发射

                //后坐力
                #region 后坐力
                playerMovement.IsAttacking = true;
                playerMovement.SetGravityScale(playerMovement.Data.gravityScale * 2);
                Vector2 attackDir = GetAttackDirection();
                playerMovement.CheckDirectionToFace(attackDir.x > 0);

                if (attackDir.x > 0) {
                        StartBackForceOnRight(false);
                }
                else {
                        StartBackForceOnRight(true);
                }

                if (playerMovement.IsOnGround) {
                        rb.velocity += Vector2.up * backForce;
                }
                else {
                        if (attackDir.y > 0.8f) {
                                rb.velocity += Vector2.down * backForce;
                        }
                        else if (attackDir.y < -0.8f) {
                                rb.velocity += Vector2.up * backForce;
                        }
                }
                #endregion
        }

        private Vector2 GetAttackDirection() {
                Vector2 dir = new Vector2();
                dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                dir = dir.normalized;
                return dir;
        }
        private void StartBackForceOnRight(bool isRight) {
                if (isRight) {
                        SetBackForceOnX(1);
                        StartCoroutine(ResetBackForceOnX(backForceTime));
                }
                else {
                        SetBackForceOnX(-1);
                        StartCoroutine(ResetBackForceOnX(backForceTime));
                }
        }

        private void SetBackForceOnX(float x) {
                playerMovement.SetBackForce(x);
        }

        private IEnumerator ResetBackForceOnX(float time) {
                yield return new WaitForSeconds(time);
                SetBackForceOnX(0);
                playerMovement.IsAttacking = false;
        }
}