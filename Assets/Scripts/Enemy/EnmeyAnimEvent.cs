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
}
