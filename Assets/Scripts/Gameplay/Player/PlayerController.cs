using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using Cinemachine;

public class PlayerController : MonoBehaviour {
        public static PlayerController Instance { get; private set; }

        private PlayerInput playerInput;
        private PlayerMovement playerMovement;
        private Rigidbody2D rb;
        private Player player;
        //public Player Player { get; private set; }

        private GameMenuManager gameMenu;
        private bool isOpeningGameMenu;

        public InteractType interactType { set; private get; }

        //进入新场景走一小段距离，或从下方跳出来
        //public IEnumerator WalkIntoNewMap(Vector2 exitDir, float delay) {
        //        playerInput.enabled = false;
        //        if (exitDir.y > 0) {
        //                rb.AddForce(Vector2.up * playerMovement.Data.jumpForce, ForceMode2D.Impulse);
        //        }

        //        if (exitDir.x != 0) {
        //                playerMovement.Move(new Vector2(exitDir.x, 0));
        //        }
        //        yield return new WaitForSeconds(delay);
        //        //playerMovement.Move(Vector2.zero);
        //        playerInput.enabled = true;
        //}

        //private CinemachineBrain cineBrain;
        //private CinemachineVirtualCamera currentCamera;

        private void Awake() {
                if (Instance != null && Instance != this) {
                        Destroy(gameObject);
                }
                else {
                        Instance = this;
                }
                //DontDestroyOnLoad(gameObject);
        }
        private void Start() {
                gameMenu = FindAnyObjectByType<GameMenuManager>();
                playerInput = GetComponent<PlayerInput>();
                player = GetComponent<Player>();
                rb = GetComponent<Rigidbody2D>();
                playerMovement = GetComponent<PlayerMovement>();
                //cineBrain = Camera.main.GetComponent<CinemachineBrain>();
                //OnFlip(playerMovement.IsFacingRight);
        }

        #region Input Actions of Interaction
        public void OnInteract(InputAction.CallbackContext context) {
                if (context.phase == InputActionPhase.Started) {
                        switch (interactType) {
                                case InteractType.None:
                                        //禁用移动
                                        //释放孢子
                                        Debug.Log("释放光孢子");
                                        break;
                                case InteractType.SavePoint:
                                        //保存
                                        GameManager.Instance.SaveGame();
                                        //回血
                                        player.Heal(Player.GetMaxHP());
                                        break;
                        }
                }
        }


        #endregion

        #region Input Actions of Movement
        public void OnDirection(InputAction.CallbackContext context) {
                playerMovement.Move(context.ReadValue<Vector2>());
        }
        public void OnJump(InputAction.CallbackContext context) {
                if (context.phase == InputActionPhase.Started) {
                        playerMovement.JumpStart();
                }
                else if ((context.phase == InputActionPhase.Canceled)) {
                        playerMovement.JumpEnd();
                }
        }
        public void OnDash(InputAction.CallbackContext context) {
                if (context.phase == InputActionPhase.Started) {
                        playerMovement.Dash();
                }
        }
        #endregion

        #region Input Actions of GameMenu
        public void OnESC(InputAction.CallbackContext context) {
                if (context.phase == InputActionPhase.Started) {
                        if (!isOpeningGameMenu) {
                                isOpeningGameMenu = true;
                                SwitchInputMap("GameMenu");
                                gameMenu.OpenGameMenu();
                        }
                        else {
                                isOpeningGameMenu = false;
                                SwitchInputMap("Gameplay");
                                gameMenu.CloseGameMenu();
                        }
                }
        }
        public void OnNextPage(InputAction.CallbackContext context) {
                if (context.phase == InputActionPhase.Started) {
                        gameMenu.OnNextPage();
                }
        }
        public void OnLastPage(InputAction.CallbackContext context) {
                if (context.phase == InputActionPhase.Started) {
                        gameMenu.OnLastPage();
                }
        }
        #endregion

        private void SwitchInputMap(string mapName) {
                playerInput.currentActionMap = playerInput.actions.FindActionMap(mapName);
        }

        //#region Camera
        //public void OnFlip(bool isFacingRight) {
        //        GetCurrentCamera();
        //        currentCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x =
        //}

        //private void GetCurrentCamera() {
        //        currentCamera = cineBrain.ActiveVirtualCamera as CinemachineVirtualCamera;
        //        if (currentCamera != null) {
        //                Debug.Log(currentCamera.Name);
        //        }
        //        else {
        //                Debug.Log("当前没有相机");
        //        }
        //}
        //#endregion
}

public enum InteractType {
        None,
        SavePoint
}