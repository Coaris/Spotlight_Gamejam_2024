using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

        private Rigidbody2D rb;
        private PlayerMovement playerMovement;

        [SerializeField] private Transform attackCheck;
        [SerializeField] private ATKChecker ssa;

        [SerializeField] float backForce = 1;
        [SerializeField] float backForceTime = 0.3f;
        [SerializeField] float attackCD = 2f;
        private float attackTimer;

        private void Start() {
                rb = GetComponent<Rigidbody2D>();
                playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update() {
                #region TIMERS
                attackTimer -= Time.deltaTime;
                #endregion
        }
        public void Attack() {
                //滑墙或冷却中不能攻击
                if (playerMovement.IsSliding || attackTimer >= 0) return;

                playerMovement.IsAttacking = true;
                playerMovement.SetGravityScale(playerMovement.Data.gravityScale * 2);
                rb.velocity = Vector3.zero;
                Vector2 attackDir = GetAttackDirection();

                //攻击
                ShootSpore(attackDir);

                //后坐力
                KnockBack(attackDir);
        }



        #region 攻击
        private void ShootSpore(Vector2 _attackDir) {
                float angle = Mathf.Atan2(_attackDir.x, _attackDir.y) * Mathf.Rad2Deg;
                attackCheck.localRotation = Quaternion.Euler(0, 0, -angle);

                attackTimer = attackCD;
                ssa.OnAnimStart();
        }
        #endregion


        #region 后坐力
        private void KnockBack(Vector2 attackDir) {
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
        #endregion
}