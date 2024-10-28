using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        //TIMER
        [SerializeField] private float skillCD;
        private float skillTimer;

        //RUSH
        [SerializeField] private Transform stagePointL;
        [SerializeField] private Transform stagePointR;
        [SerializeField] private Transform stagePointM;
        private bool isPreparingForRush;
        private bool isRushing;


        //KNOCK
        [SerializeField] private List<Transform> dropPoints;
        private List<Transform> randomDropPoints;
        [SerializeField] private GameObject dropNutB;
        [SerializeField] private int dropCount;

        //PIERCE
        [SerializeField] private Transform shadowA;
        [SerializeField] private Transform shadowB;

        [SerializeField] private Transform shadowLow;
        [SerializeField] private Transform shadowWaring;
        [SerializeField] private Transform shadowHigh;

        [SerializeField] private float warningTime;
        private bool isWarning;
        private float warningTimer;

        private Transform currentShadow;

        private void Start() {
                player = FindObjectOfType<PlayerController>().transform;
                randomDropPoints = new List<Transform>();
                hpMax = hp;
                state = BossState.Sleeping;
                move = Vector2.zero;
                skillTimer = skillCD;
                stagePointL.SetParent(null);
                stagePointR.SetParent(null);
                stagePointM.SetParent(null);

                foreach (Transform t in dropPoints) {
                        t.SetParent(null);
                }

                shadowA.SetParent(null);
                shadowB.SetParent(null);

                shadowLow.SetParent(null);
                shadowWaring.SetParent(null);
                shadowHigh.SetParent(null);

                Vector3 pos = Vector3.zero;
                pos.x=shadowA.position.x;
                pos.y = shadowLow.position.y;
                pos.z = shadowA.position.z;
                shadowA.position = pos;
                pos.x = shadowB.position.x;
                pos.y = shadowLow.position.y;
                pos.z = shadowB.position.z;
                shadowB.position = pos;

                warningTimer = warningTime;
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
                if (isWarning) {
                        warningTimer -= Time.deltaTime;
                }
                #endregion

                #region CHECKERS
                if (!isRushing) {
                        CheckFaceDirection(player.transform.position.x - transform.position.x > 0);
                }
                CheckSkillTimer();
                CheckGetRushStartPosition();
                CheckGetRushEndPosition();
                CheckWarnTimer();
                #endregion
        }

        private void FixedUpdate() {
                Walk();
                Rush();
        }

        #region SKILL TIMER
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

                        case 0:
                                MoveToRushPosition();
                                _state = BossState.Walking;
                                break;
                        case 1:
                                ChangeToKnocking();
                                _state = BossState.Knocking;
                                break;
                        case 2:
                                ChangeToPierceStart();
                                _state = BossState.Piercing;
                                break;
                        default:
                                ChangeToPierceStart();
                                _state = BossState.Piercing;
                                break;
                }
                return _state;
        }
        #endregion

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

                shadowA.GetComponent<Shadow>().CloseColliders();
                shadowB.GetComponent<Shadow>().CloseColliders();
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
                //Ëæ»ú¼¸µã
                randomDropPoints = dropPoints.OrderBy(x => Random.value).Take(dropCount).ToList();
                //µôÂä¶«Î÷
                foreach (Transform t in randomDropPoints) {
                        Instantiate(dropNutB, t.position, t.rotation);
                }
        }
        #endregion

        #region PIERCE

        public void ChangeToPierceStart() {
                anim.SetTrigger("PierceStart");
        }
        public void ChangeToPierce() {
                anim.SetTrigger("Pierce");
                //´©´Ì¹¥»÷
                Warn(0.2f);
        }
        public void ChangeToPierceEnd() {
                anim.SetTrigger("PierceEnd");
                
        }

        private void CheckWarnTimer() {
                if (isWarning && warningTimer <= 0) {
                        isWarning = false;
                        //Ö´ÐÐ¹¥»÷
                        currentShadow.GetComponent<Shadow>().OpenColliders();
                        currentShadow.DOMoveY(shadowHigh.position.y, 0.5f).SetEase(Ease.InExpo);
                        if (gameObject.activeInHierarchy) {
                                StartCoroutine(ResetShadow());
                        }
                }
        }
        private IEnumerator ResetShadow() {
                yield return new WaitForSeconds(0.5f);
                currentShadow.DOMoveY(shadowLow.position.y, 1f).SetEase(Ease.InExpo);
                ChangeToPierceEnd();
        }
        private void Warn(float _warnTime) {
                warningTimer = warningTime;
                isWarning = true;
                if (Random.Range(0, 2) == 0) {
                        currentShadow = shadowA;
                }
                else {
                        currentShadow = shadowB;
                }
                currentShadow.DOMoveY(shadowWaring.position.y, _warnTime).SetEase(Ease.InExpo);
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
