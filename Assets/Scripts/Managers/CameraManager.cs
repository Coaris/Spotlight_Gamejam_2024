using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour {
        public static CameraManager instance;

        [SerializeField] private CinemachineVirtualCamera[] allVirtualCameras;

        [Header("当玩家跳跃/降落时，Y轴移动")]
        [SerializeField] private float fallPanAmount = 0.25f;
        [SerializeField] private float fallPanTime = 0.35f;
        public float fallSpeedYDampingChangeThreshold = -15;

        public bool IsLerpingYDamping { get; private set; }
        public bool LerpedFromPlayerFalling;
        private Coroutine lerpYPanCoroutine;
        private Coroutine panCameraCoroutine;

        private CinemachineVirtualCamera currentCamera;
        private CinemachineFramingTransposer framingTransposer;

        private float normYPanAmount;

        private Vector2 startingTrackedObjectOffset;

        private void Awake() {
                if (instance == null) instance = this;
                else Destroy(gameObject);

                for (int i = 0; i < allVirtualCameras.Length; i++) {
                        if (allVirtualCameras[i].enabled) {
                                currentCamera = allVirtualCameras[i];
                                framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
                        }
                }

                normYPanAmount = framingTransposer.m_YDamping;

                startingTrackedObjectOffset = framingTransposer.m_TrackedObjectOffset;

        }

        #region Lerp the Y Damping
        public void LerpYDamping(bool isPlayerFalling) {
                lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
        }

        private IEnumerator LerpYAction(bool isPlayerFalling) {
                IsLerpingYDamping = true;
                //获取起点
                float startDampAmount = framingTransposer.m_YDamping;
                float endDampAmount = 0f;
                //确定重点
                if (isPlayerFalling) {
                        endDampAmount = fallPanAmount;
                        LerpedFromPlayerFalling = true;
                }
                else {
                        endDampAmount = normYPanAmount;
                }
                //插值运算
                float elapsedTime = 0f;
                while (elapsedTime < fallPanTime) {
                        elapsedTime += Time.deltaTime;

                        float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime / fallPanTime));
                        framingTransposer.m_YDamping = lerpedPanAmount;

                        yield return null;
                }


                IsLerpingYDamping = false;
        }
        #endregion

        #region Pan Camera
        public void PanCameraOnContact(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos) {
                panCameraCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection, panToStartingPos));
        }
        private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos) {
                Vector2 endPos = Vector2.zero;
                Vector2 startPos = Vector2.zero;

                if (!panToStartingPos) {
                        switch (panDirection) {
                                case PanDirection.Up:
                                        endPos = Vector2.up;
                                        break;
                                case PanDirection.Down:
                                        endPos = Vector2.down;
                                        break;
                                case PanDirection.Left:
                                        endPos = Vector2.left;
                                        break;
                                case PanDirection.Right:
                                        endPos = Vector2.right;
                                        break;
                        }

                        endPos *= panDistance;

                        startPos = startingTrackedObjectOffset;

                        endPos += startPos;
                }
                else {
                        startPos = framingTransposer.m_TrackedObjectOffset;
                        endPos = startingTrackedObjectOffset;
                }

                float elapedTime = 0f;
                while (elapedTime < panTime) {
                        elapedTime += Time.deltaTime;

                        Vector3 panLerp = Vector3.Lerp(startPos, endPos, (elapedTime / panTime));
                        framingTransposer.m_TrackedObjectOffset = panLerp;

                        yield return null;
                }
        }

        #endregion

        #region Swap Camera
        public void SwapCamera(CinemachineVirtualCamera cameraFromLeft, CinemachineVirtualCamera cameraFromRight, Vector2 triggerExitDirection) {
                if (currentCamera == cameraFromLeft && triggerExitDirection.x > 0f) {
                        cameraFromRight.enabled = true;
                        cameraFromLeft.enabled = false;
                        currentCamera = cameraFromRight;
                        framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
                }
                else if (currentCamera == cameraFromRight && triggerExitDirection.x < 0f) {
                        cameraFromLeft.enabled = true;
                        cameraFromRight.enabled = false;
                        currentCamera = cameraFromLeft;
                        framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
                }
        }
        #endregion
}
