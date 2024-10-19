using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour {
        private Player player;

        private SafeGroundSaver safeGroundSaver;

        [SerializeField] private int damage;

        private void Start() {
                safeGroundSaver = GameObject.FindGameObjectWithTag("Player").GetComponent<SafeGroundSaver>();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
                if (collision.CompareTag("Player")) {
                        player = collision.gameObject.GetComponent<Player>();

                        player.Damage(damage);

                        safeGroundSaver.WarpPlayerToSafeGround();
                }
        }
}