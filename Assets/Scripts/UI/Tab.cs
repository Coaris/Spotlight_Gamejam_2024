using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class Tab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
        [HideInInspector] public Image background;
        [HideInInspector] public GameMenuManager gameMenuManager;

        public UnityEvent onSelected;
        public UnityEvent onDeselected;

        #region Interfaces
        public void OnPointerEnter(PointerEventData eventData) {
                gameMenuManager.OnTabEnter(this);
        }
        public void OnPointerExit(PointerEventData eventData) {
                gameMenuManager.OnTabExit(this);
        }
        public void OnPointerClick(PointerEventData eventData) {
                gameMenuManager.SelectTab(this);
        }
        #endregion

        #region Events
        public void OnSelected() {
                if (onSelected != null) onSelected.Invoke();
        }
        public void OnDeselected() {
                if (onDeselected != null) onDeselected.Invoke();
        }
        #endregion

        private void Start() {
                background = GetComponent<Image>();
                gameMenuManager = transform.parent.parent.GetComponentInParent<GameMenuManager>();
        }
}