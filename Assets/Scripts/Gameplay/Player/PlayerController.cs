using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
        private PlayerController instance;

        private PlayerInput playerInput;
        private PlayerMovement playerMovement;

        private GameMenuManager gameMenu;
        private bool isOpeningGameMenu;

        private void Awake() {
                if (instance == null) instance = this;
                else Destroy(gameObject);
        }
        private void Start() {
                gameMenu = FindAnyObjectByType<GameMenuManager>();
                playerInput = GetComponent<PlayerInput>();
                playerMovement = GetComponent<PlayerMovement>();
        }

        #region Input Actions of Movement
        public void OnDirection(InputAction.CallbackContext context) {
                if (playerMovement.canMove) {
                        playerMovement.Move(context.ReadValue<Vector2>());
                        
                }
        }
        public void OnJump(InputAction.CallbackContext context) {
                if (playerMovement.canJump && context.phase == InputActionPhase.Started) {
                        playerMovement.Jump();
                }
        }
        public void OnDash(InputAction.CallbackContext context) {
                if (playerMovement.canDash && context.phase == InputActionPhase.Started) {
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
}
