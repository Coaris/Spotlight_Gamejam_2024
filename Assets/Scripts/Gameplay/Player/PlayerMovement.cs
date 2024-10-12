using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

        [SerializeField] private PlayerConfig playerConfig;

        #region Variables
        [HideInInspector] public bool canMove;//是否可以移动，false时不接受PlayerController的输入
        private Vector2 moveInput;//玩家的移动输入

        [HideInInspector] public bool canJump;//是否可以跳跃，false时不接受PlayerController的输入

        [HideInInspector] public bool canDash;//是否可以冲刺，false时不接受PlayerController的输入
        #endregion

        private void Start() {
                canJump = true;
                canMove = true;
                canDash = true;
        }

        private void Update() {

        }

        #region 移动
        public void Move(Vector2 input) {
                moveInput = input;
                Debug.Log(moveInput);
        }
        #endregion

        #region 跳跃和重力
        public void Jump() {
                Debug.Log("跳！");
        }
        #endregion

        #region 冲刺
        public void Dash() {
                Debug.Log("冲刺！");
        }
        #endregion
}
