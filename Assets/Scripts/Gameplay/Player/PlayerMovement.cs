using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

        [SerializeField] private PlayerConfig playerConfig;

        #region Variables
        [HideInInspector] public bool canMove;//�Ƿ�����ƶ���falseʱ������PlayerController������
        private Vector2 moveInput;//��ҵ��ƶ�����

        [HideInInspector] public bool canJump;//�Ƿ������Ծ��falseʱ������PlayerController������

        [HideInInspector] public bool canDash;//�Ƿ���Գ�̣�falseʱ������PlayerController������
        #endregion

        private void Start() {
                canJump = true;
                canMove = true;
                canDash = true;
        }

        private void Update() {

        }

        #region �ƶ�
        public void Move(Vector2 input) {
                moveInput = input;
                Debug.Log(moveInput);
        }
        #endregion

        #region ��Ծ������
        public void Jump() {
                Debug.Log("����");
        }
        #endregion

        #region ���
        public void Dash() {
                Debug.Log("��̣�");
        }
        #endregion
}
