using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnmeyAnimEvent : MonoBehaviour {
        [SerializeField] GameObject root;
        public void OnDeadEnd() {
                Destroy(root);
        }

        public void OnExplode() {
                root.GetComponent<IExplode>().OnExplode();
        }

        public void OnAwakeEnd() {
                root.GetComponent<Boss>().ChangeToWalk();
        }
        public void OnRushStartEnd() {
                root.GetComponent<Boss>().ChangeToRush();
        }
        public void OnKnockEnd() {
                root.GetComponent<Boss>().FallDropNuts();
        }
        public void OnPierceStartEnd() {
                root.GetComponent<Boss>().ChangeToPierce();
        }
        public void OnPierceEndEnd() {
                root.GetComponent<Boss>().ChangeToWalk();
        }
        public void OnStopBossBGM() {
                BGM.Instance.ChangeToNormalBGM();
        }
        public void OnOpenDoor() {
                root.GetComponent<Boss>().OnOpenDoor();
        }
}
