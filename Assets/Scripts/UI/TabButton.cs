using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler {
        private TabGroup tabGroup;//ok
        [HideInInspector] public Image background;//ok

        public UnityEvent onTabSelected;
        public UnityEvent onTabDeselected;

        #region ½Ó¿Ú
        public void OnPointerClick(PointerEventData eventData) {
                tabGroup.OnTabSelected(this);
        }//ok
        public void OnPointerEnter(PointerEventData eventData) {
                tabGroup.OnTabEnter(this);
        }//ok
        public void OnPointerExit(PointerEventData eventData) {
                tabGroup.OnTabExit(this);
        }//ok
        #endregion//ok

        private void Start() {
                background = GetComponent<Image>();//ok
                tabGroup = GetComponentInParent<TabGroup>();//ok
                tabGroup.SubScribe(this);//ok
        }//ok

        public void Select() {
                if (onTabSelected != null) { onTabSelected.Invoke(); }
        }
        public void Deselect() {
                if (onTabDeselected != null) { onTabDeselected.Invoke(); }
        }
}
