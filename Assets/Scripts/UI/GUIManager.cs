using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIManager : MonoBehaviour {
        public static GUIManager Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI currentHPText;
        [SerializeField] private TextMeshProUGUI maxHPText;

        private void Awake() {
                if (Instance != null && Instance != this) {
                        Destroy(gameObject);
                }
                else {
                        Instance = this;
                }
        }

        public void UpdateHP(int currentHP) {
                currentHPText.text = currentHP.ToString();
        }
        public void UpdateMaxHP(int maxHP) {
                maxHPText.text = maxHP.ToString();
        }
}
