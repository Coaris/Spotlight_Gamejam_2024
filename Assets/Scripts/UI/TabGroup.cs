using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour {
        public List<TabButton> tabButtons;//ok
        public Sprite tabIdle;//ok
        public Sprite tabHover;//ok
        public Sprite tabActived;//ok

        public List<GameObject> PagesToSwap;//ok

        private TabButton selectedButton;//ok

        public void SubScribe(TabButton button) {
                if (tabButtons == null) {
                        tabButtons = new List<TabButton>();
                }
                tabButtons.Add(button);
        }//ok
        public void OnTabEnter(TabButton button) {
                ResetTabs();
                if (selectedButton == null || button != selectedButton) {
                        button.background.sprite = tabHover;
                }
        }//ok
        public void OnTabExit(TabButton button) {
                ResetTabs();
        }//ok
        public void OnTabSelected(TabButton button) {
                if (selectedButton != null) {
                        selectedButton.Deselect();
                }

                selectedButton = button;//ok

                selectedButton.Select();

                ResetTabs();//ok
                button.background.sprite = tabActived;//ok
                //ÇÐ»»Ò³Ãæ
                int index = button.transform.GetSiblingIndex();
                for (int i = 0; i < PagesToSwap.Count; i++) {
                        if (i == index) {
                                PagesToSwap[i].SetActive(true);
                        }
                        else {
                                PagesToSwap[i].SetActive(false);
                        }
                }
        }
        private void ResetTabs() {
                foreach (TabButton button in tabButtons) {
                        if (selectedButton != null && button == selectedButton) continue;
                        button.background.sprite = tabIdle;
                }
        }//ok
}
