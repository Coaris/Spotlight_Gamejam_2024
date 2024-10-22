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
                        //玩家死亡
                        currentHP = 0;
                        GUIManager.Instance.UpdateHP(currentHP);
                        Dead();
                }
                else {
                        //玩家受伤
                        GUIManager.Instance.UpdateHP(currentHP);
                        hit(isFallDamage);
                }
        }
        private void Dead() {
                GameManager.Instance.LoadGame();
                //播放死亡动画
                OnReburn();//临时
        }
        private void hit(bool isFallDamage) {

                if (isFallDamage) {
                        //播放掉落受击动画
                        OnReturnSafeGround();//临时
                }
                else {
                        //播放受击动画
                }
        }
        public void OnReturnSafeGround() {
                //地刺伤害，受伤动画结束后事件调用
                safeGroundSaver.WarpPlayerToSafeGround();
        }
        public void OnReburn() {
                //死亡动画结束后事件调用
                GameManager.Instance.LoadGameReburn();
        }

        public static int GetCurrentHP() {
                return currentHP;
        }
        public static int GetMaxHP() {
                return maxHP;
        }



}