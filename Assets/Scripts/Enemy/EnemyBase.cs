using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {
        [SerializeField] protected Animator anim;
        [SerializeField] private int hp;
        [SerializeField] private SpriteRenderer spriteRenderer;

        [SerializeField] private Transform sprite;
        [SerializeField] protected Rigidbody2D rb;
        protected bool isFacingRight;


        protected bool isDead;

        protected void Update() {
                if (rb.velocity.x != 0) {
                        CheckFaceDirection(rb.velocity.x > 0);
                }
        }

        #region ¼ì²â³¯Ïò
        protected void CheckFaceDirection(bool _isMovingRight) {
                if (_isMovingRight != isFacingRight) {
                        Turn();
                }
        }
        private void Turn() {
                Vector3 scale = sprite.localScale;
                scale.x *= -1;
                sprite.localScale = scale;
                isFacingRight = !isFacingRight;
        }
        #endregion

        private void OnCollisionEnter2D(Collision2D collision) {
                if (collision.transform.CompareTag("Player")) {
                        collision.transform.GetComponent<Player>().Damage(1, transform.position.x - collision.transform.position.x);
                }
                //if (collision.CompareTag("Player")) {
                //        collision.GetComponent<Player>().Damage(1, transform.position.x - collision.transform.position.x);
                //}
        }


        public void OnHit(int _damage) {
                hp -= _damage;
                //ÊÜ»÷ÉÁË¸
                HitFlash();
                if (hp <= 0) {
                        //ËÀÍö
                        isDead = true;
                        Dead();
                }
        }

        private void Dead() {
                anim.SetTrigger("Dead");
        }

        private void HitFlash() {
                spriteRenderer.material.color = Color.red;
                StartCoroutine(ResetHitFlash());
        }
        private IEnumerator ResetHitFlash() {
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.material.color = Color.white;
        }
}
