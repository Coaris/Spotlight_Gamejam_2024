using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configurations/PlayerConfig")]
public class PlayerConfig : ScriptableObject {
        [Header("Gravity")]
        [HideInInspector] public float gravityStrength; //����ǿ�ȣ����������߶Ⱥ�����ʱ��������
        [HideInInspector] public float gravityScale; //�����ȣ���������ǿ�Ⱥ����������������
                                                     //���õ���ҵ� rigidbody2D.gravityScale
        [Space(5)]
        public float fallGravityMult; //����ʱ����������
        public float maxFallSpeed; //��������ٶ�
        [Space(5)]
        public float fastFallGravityMult; //��������ʱ���������ʣ�����ڿ���ʱ���´���
        public float maxFastFallSpeed; //�����������ٶ�

        [Space(20)]

        [Header("Run")]
        public float runMaxSpeed; //������ٶ�
        public float runAcceleration; //���ܼ��ټ��ٶȣ��������������ٶ�ʱ�������ڰ���ʱ�ͼ��ٵ�����ٶȡ����ڼ��㱼�ܼ�������
        [HideInInspector] public float runAccelAmount; // ���ܼ��������ƶ���������õı���
        public float runDecceleration; //���ܼ��ټ��ٶȣ��������������ٶ�ʱ�������ڰ���ʱ�ͼ��ٵ�0�����ڼ��㱼�ܼ�������
        [HideInInspector] public float runDeccelAmount; //���ܼ��������ƶ���������õı���
        [Space(5)]
        [Range(0f, 1)] public float accelInAir; //���м��ټ��ٶȱ���
        [Range(0f, 1)] public float deccelInAir;//���м��ټ��ٶȱ���
        [Space(5)]
        public bool doConserveMomentum = true;//�Ƿ񱣳ֶ���

        [Space(20)]

        [Header("Jump")]
        public float jumpHeight; //�����߶�
        public float jumpTimeToApex; //����ʱ�䡣�˱�����Ӱ�������Ⱥ���Ծ���ٶȡ�
        [HideInInspector] public float jumpForce; //��Ծ���ٶȣ��ƶ�������õı���

        [Header("Jump Cut & Jump Hang")]
        public float jumpCutGravityMult; //����������Ծʱ�ɿ���Ծ��ť�������������ı������Կ�������
        [Range(0f, 1)] public float jumpHangGravityMult; //�����Ϳ������ȡ��ڽӽ���Ծ���㣨�������߶ȣ�ʱ����������������и�����Ϳ�ʱ��
        public float jumpHangTimeThreshold; //�����Ϳ��ٶ���ֵ��
        [Space(0.5f)]
        public float jumpHangAccelerationMult;//�����Ϳռ��ٶȱ���
        public float jumpHangMaxSpeedMult;//�����Ϳ�����ٶ�

        [Header("Wall Jump")]
        public Vector2 wallJumpForce; //����ɫ����ǽ����Ծʱʩ�Ӹ���ɫ���������ٶȡ�
        [Space(5)]
        [Range(0f, 1f)] public float wallJumpRunLerp; //��ǽ����Ծʱ�����ٽ�ɫ�ƶ���Ч����
        [Range(0f, 1.5f)] public float wallJumpTime; //ǽ����Ծ���ɫ���ƶ����ٳ���ʱ�䡣
        public bool doTurnOnWallJump; //��ҽ���ǽ����Ծʱ��ת��������Ծ����

        [Space(20)]

        [Header("Wall Slide")]
        public float slideSpeed;//��ǽ�ٶ�
        public float slideAccel;//��ǽ���ٶ�

        [Header("Assists")]
        [Range(0.01f, 0.5f)] public float coyoteTime; //����ʱ��
        [Range(0.01f, 0.5f)] public float jumpInputBufferTime; //Ԥ��ʱ�䡣������Ծ��ť����������Ծ�����������ŵأ�֮ǰ���Զ�ִ����Ծ�Ŀ�����

        [Space(20)]

        [Header("Dash")]
        public int dashAmount; //���õĳ�̴���
        public float dashSpeed;// ��̵��ٶ�
        public float dashSleepTime; //���³�̺�����Ķ���ʱ��
        [Space(5)]
        public float dashAttackTime; //��̹����ĳ���ʱ��
        [Space(5)]
        public float dashEndTime; //��ɳ�ʼ�϶��׶κ��ʱ�䣬����ƽ�����ɻؿ���״̬�����κα�׼״̬��
        public Vector2 dashEndSpeed; //��������ٶȣ�ʹ��̸о�������Ӧ�ԣ��ڡ�Celeste����ʹ�ã�
        [Range(0f, 1f)] public float dashEndRunLerp; //�ڳ��ʱ��������ƶ���Ч��
        [Space(5)]
        public float dashRefillTime; //��̵Ĳ���ʱ��
        [Space(5)]
        [Range(0.01f, 0.5f)] public float dashInputBufferTime;// ���³�̰�ť��Ŀ�����


        //��SO�ļ����ݸ���ʱ�ᱻ����һ�Σ��Զ���������
        private void OnValidate() {
                //���������߶� �� ����ʱ�� ��������ǿ�� gravity = 2 * jumpHeight / timeToJumpApex^2 
                gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);

                //��������ǿ�� �����������ţ��������ø�rigidbody2D
                gravityScale = gravityStrength / Physics2D.gravity.y;

                //�����ƶ��ļ��ٶ� amount = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed
                runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
                runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

                //�����������ٶ� initialJumpVelocity = gravity * timeToJumpApex
                jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

                #region �ƶ����ٶȵ�ȡֵ��Χ
                runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
                runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
                #endregion
        }
}
