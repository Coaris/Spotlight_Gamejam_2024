using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {
        [SerializeField] private Animator anim;
        [SerializeField] private int hp;



        private void OnTriggerEnter2D(Collider2D collision) {
                if (collision.CompareTag("Player")) {
                        Debug.Log("撞到玩家了");
                        collision.GetComponent<Player>().Damage(1, transform.position.x - collision.transform.position.x);
                }
        }


        public void OnHit(int _damage) {
                hp -= _damage;
                if (hp <= 0) {
                        //死亡
                        Debug.Log("死了");
                }
                else {
                        //受伤
                        Debug.Log("受伤");
                }
        }

        private void Dead() {

        }
}
