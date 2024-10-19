using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPortal : MonoBehaviour {
        [SerializeField] private string portalNameThis;
        [SerializeField] private SceneAsset portalTo;
        [SerializeField] private string portalNameThat;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Vector2 exitDirection;
        [SerializeField] private float exitTime;

        private IEnumerator LoadNeMap;


        private void OnTriggerEnter2D(Collider2D collision) {
                if (collision.CompareTag("Player")) {
                        MapManager.Instance.portalFrom = portalNameThis;

                        StartCoroutine(LoadNewMap(portalTo));
                }
        }

        private void Start() {
                if (portalNameThat == MapManager.Instance.portalFrom) {
                        PlayerController.Instance.transform.position = startPoint.position;
                }
        }

        private IEnumerator LoadNewMap(SceneAsset scene) {
                GameMenuManager.Instance.mapLoadFader.FadeOut();
                yield return new WaitForSeconds(0.3f);
                AsyncOperation async = SceneManager.LoadSceneAsync(scene.name);
                async.completed += OnLoadScene;
        }

        private void OnLoadScene(AsyncOperation operation) {
                GameMenuManager.Instance.mapLoadFader.FadeIn();
        }
}
