using System.Collections;
using UnityEngine;

public class Rusher : EnemyBase, IPlayerCheck {
        [SerializeField] Transform patrolL;
        [SerializeField] Transform patrolR;
        [SerializeField] float moveSpeed;
        [SerializeField] float rushSpeed;

        private Vector2 move;

        private bool isAttacking;

        private void Start() {
                patrolL.SetParent(null);
                patrolR.SetParent(null);
                move.x = moveSpeed;
                move.y = rb.velocity.y;
        }
        private void Update() {
                if (isDead) {
                        rb.velocity = Vector2.zero;
                        return;
                } 
                base.Update();
                move.y = rb.velocity.y;
                if (isAttacking) {
                        //¹¥»÷
                        Rush();
                }
                else {
                        //Ñ²Âß
                        CheckPatrolPoint();
                        Patrol();
                }
        }
        private void Rush() {
                if (isFacingRight) {
                        move.x = rushSpeed;
                }
                else {
                        move.x = -rushSpeed;
                }
                rb.velocity = move;
        }



        public void OnPlayerDetected() {
                isAttacking = true;
                anim.SetBool("IsAttacking", true);
        }
        public void OnPlayerLost() {
                if (gameObject.activeInHierarchy) {
                        StartCoroutine(ResetPatrol());
                }
                
        }

        private IEnumerator ResetPatrol() {
                yield return new WaitForSeconds(1);
                if (isFacingRight) {
                        move.x = moveSpeed;
                }
                else {
                        move.x = -moveSpeed;
                }
                isAttacking = false;
                anim.SetBool("IsAttacking", false);
        }


        #region Ñ²Âß
        private void CheckPatrolPoint() {
                if (transform.position.x < patrolL.position.x) {
                        move.x = moveSpeed;
                }

                if (transform.position.x > patrolR.position.x) {
                        move.x = -moveSpeed;
                }
        }
        private void Patrol() {
                rb.velocity = move;
        }
        #endregion
}
