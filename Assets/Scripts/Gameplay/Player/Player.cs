using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
        private SafeGroundSaver safeGroundSaver;



        [SerializeField] private static int maxHP = 3;
        [SerializeField] private static int currentHP = 3;


        private void Start() {
                safeGroundSaver = GetComponent<SafeGroundSaver>();

                PlayerStatusManager.Instance.ReadStatus(out maxHP, out currentHP);
                GUIManager.Instance.UpdateHP(currentHP);
                GUIManager.Instance.UpdateMaxHP(maxHP);
        }

        public void Damage(int damage, bool isFallDamage) {
                currentHP -= damage;
                if (currentHP <= 0) {
                        //�������
                        currentHP = 0;
                        GUIManager.Instance.UpdateHP(currentHP);
                        Dead();
                }
                else {
                        //�������
                        GUIManager.Instance.UpdateHP(currentHP);
                        hit(isFallDamage);
                }
        }
        private void Dead() {
                GameManager.Instance.LoadGame();
                //������������
                OnReburn();//��ʱ
        }
        private void hit(bool isFallDamage) {

                if (isFallDamage) {
                        //���ŵ����ܻ�����
                        OnReturnSafeGround();//��ʱ
                }
                else {
                        //�����ܻ�����
                }
        }
        public void OnReturnSafeGround() {
                //�ش��˺������˶����������¼�����
                safeGroundSaver.WarpPlayerToSafeGround();
        }
        public void OnReburn() {
                //���������������¼�����
                GameManager.Instance.LoadGameReburn();
        }

        public static int GetCurrentHP() {
                return currentHP;
        }
        public static int GetMaxHP() {
                return maxHP;
        }



}