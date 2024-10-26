using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSporeAnim : MonoBehaviour {
        [SerializeField] private Animator anim;
        [SerializeField] private PlayerAttack playerAttack;
        [SerializeField] private Collider2D coll;


        public void OnAnimStart() {
                anim.SetTrigger("Attack");

                coll.enabled = true;
        }
        public void OnAnimEnd() {
                anim.SetTrigger("AttackEnd");

                coll.enabled = false;

                playerAttack.ShootSporeEnd();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
                if (collision != null) {
                        //if (collision.CompareTag("Player")) {
                                
                        //}
                        //Debug.Log("000");
                }
        }
}
