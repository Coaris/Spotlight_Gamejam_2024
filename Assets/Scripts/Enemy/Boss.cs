using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TreeEditor;

public class Boss : EnemyBase {
        private int hpMax;
        private BossState state;
        private Transform player;

        [SerializeField] private float walkSpeed;
        [SerializeField] private float rushPrepareSpeed;
        [SerializeField] private float rushSpeed;
        private Vector2 move;
        private bool isPreparingForRush;
        private bool isRushing;

        [SerializeField] private float skillCD;
        private float skillTimer;

        [SerializeField] private Transform stagePointL;
        [SerializeField] private Transform stagePointR;
        [SerializeField] private Transform stagePointM;

        private void Start() {
                player = FindObjectOfType<PlayerController>().transform;
                hpMax = hp;
                state = BossState.Sleeping;
                move = Vector2.zero;
                skillTimer = skillCD;
                stagePointL.SetParent(null);
                stagePointR.SetParent(null);
                stagePointM.SetParent(null);
        }
        private void Update() {
                if (isDead) {
                        rb.velocity = Vector2.zero;
                        return;
                }

                CheckSleeping();
                if (state == BossState.Sleeping) return;

                #region TIMERS
                if (state == BossState.Walking) {
                        skillTimer -= Time.deltaTime;
                }
                #endregion

                #region CHECKERS
                if (!isRushing) {
                        CheckFaceDirection(player.transform.position.x - transform.position.x > 0);
                }
                CheckSkillTimer();
                CheckGetRushStartPosition();
                CheckGetRushEndPosition();
                #endregion
        }
        private void FixedUpdate() {
                Walk();
                Rush();
        }

        private void CheckSkillTimer() {
                if (skillTimer <= 0) {
                        state = DoASkill();
                        skillTimer = skillCD;
                }
        }
        private BossState DoASkill() {
                move = Vector2.zero;
                BossState _state;
                int _skillIndex = Random.Range(0, 3);
                switch (_skillIndex) {

                        //case 0:
                        //        MoveToRushPosition();
                        //        _state = BossState.Walking;
                        //        break;
                        //case 1:
                        //        ChangeToKnocking();
                        //        _state = BossState.Knocking;
                        //        break;
                        //case 2:
                        //        ChangeToPiercing();
                        //        _state = BossState.Piercing;
                        //        break;
                        default:
                                ChangeToKnocking();
                                _state = BossState.Knocking;
                                break;
                }
                return _state;
        }

        #region WALK
        private void Walk() {
                if (state != BossState.Walking) return;

                if (!isPreparingForRush) {
                        if (isFacingRight) {
                                move.x = walkSpeed;
                        }
                        else {
                                move.x = -walkSpeed;
                        }
                }

                rb.velocity = move;
        }

        public void ChangeToWalk() {
                state = BossState.Walking;
                anim.SetTrigger("Walk");
        }
        #endregion

        #region RUSH
        private void Rush() {
                if (isRushing) {
                        rb.velocity = move;
                }
        }

        private void MoveToRushPosition() {
                isPreparingForRush = true;
                if (isFacingRight) {
                        move.x = -rushPrepareSpeed;
                }
                else {
                        move.x = rushPrepareSpeed;
                }
        }

        private void CheckGetRushStartPosition() {
                if (!isPreparingForRush) return;

                if (isFacingRight) {
                        if (transform.position.x <= stagePointL.position.x) {
                                ChangeToRushStart();
                        }
                }
                else if (!isFacingRight) {
                        if (stagePointR.position.x <= transform.position.x) {
                                ChangeToRushStart();
                        }
                }
        }
        private void CheckGetRushEndPosition() {
                if (!isRushing) return;

                if (isFacingRight) {
                        if (stagePointR.position.x <= transform.position.x) {
                                isRushing = false;
                                ChangeToWalk();
                        }
                }
                else if (!isFacingRight) {
                        if (transform.position.x <= stagePointL.position.x) {
                                isRushing = false;
                                ChangeToWalk();
                        }
                }
        }

        public void ChangeToRushStart() {
                isPreparingForRush = false;
                move = Vector2.zero;
                state = BossState.Rushing;
                anim.SetTrigger("RushStart");
        }
        public void ChangeToRush() {
                if (isFacingRight) {
                        move.x = rushSpeed;
                }
                else {
                        move.x = -rushSpeed;
                }

                isRushing = true;
                anim.SetTrigger("Rush");
        }
        #endregion

        #region KNOCK
        public void ChangeToKnocking() {
                move = Vector2.zero;
                anim.SetTrigger("Knock");
        }

        public void FallDropNuts() {
                ChangeToWalk();
                //µôÂä¶«Î÷
        }
        #endregion

        #region PIERCE
        public void ChangeToPiercing() {
                anim.SetTrigger("Pierce");
                Debug.Log("´©´Ì");
        }
        #endregion

        #region SLEEP
        private void CheckSleeping() {
                if (state == BossState.Sleeping && hp != hpMax) {
                        state = BossState.Awaking;
                        anim.SetTrigger("Awake");
                }
        }
        #endregion
}
