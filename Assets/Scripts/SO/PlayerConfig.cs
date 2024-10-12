using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configurations/PlayerConfig")]
public class PlayerConfig : ScriptableObject {
        [Header("Gravity")]
        [HideInInspector] public float gravityStrength; //重力强度：根据满跳高度和满跳时间计算出来
        [HideInInspector] public float gravityScale; //重力比：根据重力强度和世界重力计算出来
                                                     //设置到玩家的 rigidbody2D.gravityScale
        [Space(5)]
        public float fallGravityMult; //下落时的重力倍率
        public float maxFallSpeed; //最大下落速度
        [Space(5)]
        public float fastFallGravityMult; //快速下落时的重力倍率，玩家在空中时按下触发
        public float maxFastFallSpeed; //最大快速下落速度

        [Space(20)]

        [Header("Run")]
        public float runMaxSpeed; //最大奔跑速度
        public float runAcceleration; //奔跑加速加速度，当其等于最大奔跑速度时，可以在按下时就加速到最大速度。用于计算奔跑加速量。
        [HideInInspector] public float runAccelAmount; // 奔跑加速量：移动组件所引用的变量
        public float runDecceleration; //奔跑减速加速度，当其等于最大奔跑速度时，可以在按下时就减速到0。用于计算奔跑减速量。
        [HideInInspector] public float runDeccelAmount; //奔跑减速量：移动组件所引用的变量
        [Space(5)]
        [Range(0f, 1)] public float accelInAir; //空中加速加速度倍率
        [Range(0f, 1)] public float deccelInAir;//空中减速加速度倍率
        [Space(5)]
        public bool doConserveMomentum = true;//是否保持动量

        [Space(20)]

        [Header("Jump")]
        public float jumpHeight; //满跳高度
        public float jumpTimeToApex; //满跳时间。此变量会影响重力比和跳跃初速度。
        [HideInInspector] public float jumpForce; //跳跃初速度：移动组件引用的变量

        [Header("Jump Cut & Jump Hang")]
        public float jumpCutGravityMult; //如果玩家在跳跃时松开跳跃按钮，则增加重力的倍数，以快速下落
        [Range(0f, 1)] public float jumpHangGravityMult; //顶点滞空重力比。在接近跳跃顶点（所需最大高度）时降低重力，让玩家有更大的滞空时间
        public float jumpHangTimeThreshold; //顶点滞空速度阈值。
        [Space(0.5f)]
        public float jumpHangAccelerationMult;//顶点滞空加速度倍率
        public float jumpHangMaxSpeedMult;//顶点滞空最大速度

        [Header("Wall Jump")]
        public Vector2 wallJumpForce; //当角色进行墙壁跳跃时施加给角色的起跳初速度。
        [Space(5)]
        [Range(0f, 1f)] public float wallJumpRunLerp; //在墙壁跳跃时，减少角色移动的效果。
        [Range(0f, 1.5f)] public float wallJumpTime; //墙壁跳跃后角色的移动减速持续时间。
        public bool doTurnOnWallJump; //玩家将在墙壁跳跃时旋转以面向跳跃方向。

        [Space(20)]

        [Header("Wall Slide")]
        public float slideSpeed;//滑墙速度
        public float slideAccel;//滑墙加速度

        [Header("Assists")]
        [Range(0.01f, 0.5f)] public float coyoteTime; //土狼时间
        [Range(0.01f, 0.5f)] public float jumpInputBufferTime; //预跳时间。按下跳跃按钮后，在满足跳跃条件（例如着地）之前，自动执行跳跃的宽限期

        [Space(20)]

        [Header("Dash")]
        public int dashAmount; //可用的冲刺次数
        public float dashSpeed;// 冲刺的速度
        public float dashSleepTime; //按下冲刺后，输入的冻结时间
        [Space(5)]
        public float dashAttackTime; //冲刺攻击的持续时间
        [Space(5)]
        public float dashEndTime; //完成初始拖动阶段后的时间，用于平滑过渡回空闲状态（或任何标准状态）
        public Vector2 dashEndSpeed; //减缓玩家速度，使冲刺感觉更具响应性（在《Celeste》中使用）
        [Range(0f, 1f)] public float dashEndRunLerp; //在冲刺时减缓玩家移动的效果
        [Space(5)]
        public float dashRefillTime; //冲刺的补充时间
        [Space(5)]
        [Range(0.01f, 0.5f)] public float dashInputBufferTime;// 按下冲刺按钮后的宽限期


        //当SO文件数据更新时会被调用一次，自动计算数据
        private void OnValidate() {
                //根据满跳高度 和 满跳时间 计算重力强度 gravity = 2 * jumpHeight / timeToJumpApex^2 
                gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);

                //根据重力强度 计算重力缩放，用于设置给rigidbody2D
                gravityScale = gravityStrength / Physics2D.gravity.y;

                //计算移动的加速度 amount = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed
                runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
                runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

                //计算起跳初速度 initialJumpVelocity = gravity * timeToJumpApex
                jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

                #region 移动加速度的取值范围
                runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
                runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
                #endregion
        }
}
