using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeGroundSaver : MonoBehaviour {

        [SerializeField] private LayerMask whatIsCheckPoint;

        public Vector2 safeGroundLoaction { get; private set; } = Vector2.zero;

        private Collider2D coll;
        private float safeSpotYOffest;

        private void Start() {
                safeGroundLoaction = transform.position;
                coll = GetComponent<Collider2D>();

                safeSpotYOffest = (coll.bounds.size.y / 2);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
                if ((whatIsCheckPoint.value & (1 << collision.gameObject.layer)) > 0) {
                        safeGroundLoaction = new Vector2(collision.bounds.center.x, collision.bounds.min.y + safeSpotYOffest);
                }
        }

        public void WarpPlayerToSafeGround() {
                StartCoroutine(BackToSafeGround());
        }

        private IEnumerator BackToSafeGround() {
                GameMenuManager.Instance.mapLoadFader.FadeOut();
                yield return new WaitForSeconds(0.3f);
                transform.position = safeGroundLoaction;
                GameMenuManager.Instance.mapLoadFader.FadeIn();
        }
}
