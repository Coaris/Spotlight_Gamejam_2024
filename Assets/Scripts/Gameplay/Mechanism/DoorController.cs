using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
        [SerializeField] List<Door> doors;

        private void Start() {
                if (doors != null) {
                        OpenAllDoors();
                }
        }
        private void OnTriggerEnter2D(Collider2D collision) {
                if (collision.CompareTag("Player")) {
                        foreach (Door door in doors) {
                                door.OnCloseDoor();
                        }
                }
        }

        public void OpenAllDoors() {
                foreach (Door door in doors) {
                        door.OnOpenDoor();
                }
        }
}