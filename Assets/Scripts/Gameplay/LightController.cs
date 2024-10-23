using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

        private PlayerController playerController;
        private Rigidbody2D rb;

        private Vector2 mouseOnScreen;
        private Vector3 mouseInMap;

        [SerializeField] private float distanceThreshold;
        private bool isMoving;

        private Vector2 moveDir;
        [SerializeField] private float maxSpeed = 5;
        private Vector2 force;


        private void Start() {
                playerController = FindAnyObjectByType<PlayerController>();
                playerController.BindLight(this);
                rb = GetComponent<Rigidbody2D>();
                GetMouseInMap(Input.mousePosition);
        }

        private void Update() {
                GetMouseInMap(mouseOnScreen);
                CheckDistance(mouseInMap);

        }
        private void FixedUpdate() {
                MoveTo(mouseInMap);
        }

        public void GetMouseOnScreen(Vector2 _mousePosition) {
                mouseOnScreen = _mousePosition;
        }
        private void GetMouseInMap(Vector2 _mouseOnScreen) {
                mouseInMap = Camera.main.ScreenToWorldPoint(mouseOnScreen);
                mouseInMap.z = 0;
        }

        private void CheckDistance(Vector3 _mouseOnScreen) {
                if (Vector2.Distance(_mouseOnScreen, transform.position) <= distanceThreshold) {
                        isMoving = false;
                        StopMove();
                }
                else {
                        isMoving = true;
                }
        }
        private void StopMove() {
                rb.velocity = Vector2.zero;
                Vector3 lightScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
                Cursor.SetCursor(null, new Vector2(lightScreenPosition.x, lightScreenPosition.y), CursorMode.Auto);
        }

        private void MoveTo(Vector3 _target) {
                if (!isMoving) return;
                moveDir = (mouseInMap - transform.position).normalized;
                rb.velocity = moveDir * maxSpeed;
        }


}
