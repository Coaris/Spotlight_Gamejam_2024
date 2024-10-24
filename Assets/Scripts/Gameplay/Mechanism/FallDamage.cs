using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour {

        [SerializeField] private int damage;

        private void OnTriggerEnter2D(Collider2D collision) {
                if (collision.CompareTag("Player")) {
                        collision.gameObject.GetComponent<Player>().Damage(damage, true);
                }
        }
}