using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
        private SafeGroundSaver safeGroundSaver;

        private PlayerMovement playerMovement;
        private Rigidbody2D rb;
        private PlayerInput playerInput;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private bool isInvicible;



        [SerializeField] private static int maxHP = 3;
        [SerializeField] private static int currentHP = 3;


        private void Start() {
                safeGroundSaver = GetComponent<SafeGroundSaver>();

                playerMovement = GetComponent<PlayerMovement>();
                rb = GetComponent<Rigidbody2D>();
                playerInput = GetComponent<PlayerInput>();

                PlayerStatusManager.Instance.ReadStatus(out maxHP, out currentHP);
                GUIManager.Instance.UpdateHP(currentHP);
                GUIManager.Instance.UpdateMaxHP(maxHP);
        }

        public void FallDamage(int damage, bool isFallDamage) {
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
                        OnReturnSafeGround();//临时
                }
        }
        public void Damage(int _damage, float _Xfrom) {
                if (isInvicible) return;
                isInvicible = true;
                currentHP -= _damage;
                HitFlash();
                if (currentHP <= 0) {
                        //玩家死亡
                        currentHP = 0;
                        GUIManager.Instance.UpdateHP(currentHP);
                        Dead();
                }
                else {
                        //玩家受伤
                        GUIManager.Instance.UpdateHP(currentHP);
                        //被击退
                        KnockBack(_Xfrom);
                }
        }

        #region 受击闪烁
        private void HitFlash() {
                spriteRenderer.material.color = Color.red;
                StartCoroutine(ResetHitFlash());
        }
        private IEnumerator ResetHitFlash() {
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.material.color = Color.white;
        }
        #endregion

        #region 受伤被击退
        private void KnockBack(float _Xfrom) {
                playerInput.enabled = false;

                playerMovement.SetGravityScale(playerMovement.Data.gravityScale * 2);
                rb.velocity = Vector3.zero;

                playerMovement.CheckDirectionToFace(_Xfrom > 0);
                rb.velocity += Vector2.up * 20;
                if (_Xfrom > 0) {
                        StartBackForceOnRight(false);
                }
                else {
                        StartBackForceOnRight(true);
                }
        }

        private void StartBackForceOnRight(bool isRight) {
                if (isRight) {
                        SetBackForceOnX(1);
                        StartCoroutine(ResetBackForceOnX(0.3f));
                }
                else {
                        SetBackForceOnX(-1);
                        StartCoroutine(ResetBackForceOnX(0.3f));
                }
        }

        private void SetBackForceOnX(float x) {
                playerMovement.SetBackForce(x);
        }

        private IEnumerator ResetBackForceOnX(float time) {
                yield return new WaitForSeconds(time);
                playerInput.enabled = true;
                SetBackForceOnX(0);
                playerMovement.IsAttacking = false;
                isInvicible = false;
        }
        #endregion

        public void Heal(int heal) {
                currentHP = Mathf.Clamp(currentHP + heal, 0, maxHP);
                GUIManager.Instance.UpdateHP(currentHP);
        }
        private void Dead() {
                GameManager.Instance.LoadGame();
                //播放死亡动画
                OnReburn();//临时
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