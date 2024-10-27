using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeCheck : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D collision) {
                if (collision.CompareTag("Player") && CompareTag("ExplodeCheck")) {
                        collision.transform.GetComponent<Player>().Damage(1, transform.position.x - collision.transform.position.x);
                        gameObject.SetActive(false);
                }
        }
}
