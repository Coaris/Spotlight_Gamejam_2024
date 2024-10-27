using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderMush : MonoBehaviour {
        [SerializeField] List<GameObject> dialogs = new List<GameObject>();
        private int currentIndex;


        public void Talk() {
                dialogs[currentIndex].SetActive(false);
                currentIndex = (currentIndex + 1) % dialogs.Count;
                dialogs[currentIndex].SetActive(true);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
                if (collision.CompareTag("Player")) {
                        currentIndex = 0;
                        dialogs[currentIndex].SetActive(true);
                        collision.gameObject.GetComponent<PlayerController>().interactType = InteractType.ElderMush;
                        collision.gameObject.GetComponent<PlayerController>().elderMush = this;
                }
        }
        private void OnTriggerExit2D(Collider2D collision) {
                if (collision.CompareTag("Player")) {
                        EndTalk();
                        collision.gameObject.GetComponent<PlayerController>().interactType = InteractType.None;
                        collision.gameObject.GetComponent<PlayerController>().elderMush = null;
                }
        }
        private void EndTalk() {
                foreach (GameObject go in dialogs) {
                        go.SetActive(false);
                }
        }
}
