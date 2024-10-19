using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

        [SerializeField] private static int maxHP = 3;
        [SerializeField] private static int currentHP = 3;

        private void Start() {
                PlayerStatusManager.Instance.UpdateStatus(out maxHP, out currentHP);
                GUIManager.Instance.UpdateHP(currentHP);
                GUIManager.Instance.UpdateMaxHP(maxHP);
        }

        public bool Damage(int damage) {
                bool isDead = false;

                currentHP -= damage;
                if (currentHP <= 0) {
                        //Íæ¼ÒËÀÍö
                        currentHP = 0;
                        isDead = true;
                        GUIManager.Instance.UpdateHP(currentHP);
                }
                else {
                        //Íæ¼ÒÊÜÉË
                        isDead = false;
                        GUIManager.Instance.UpdateHP(currentHP);
                }
                return isDead;
        }

        public static int GetCurrentHP() {
                return currentHP;
        }
}