using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour {
        //UI of GameMenu
        private GameObject gameMenu;
        //tab's sprites
        [SerializeField] private Sprite tabSpriteInactive;
        [SerializeField] private Sprite tabSpriteHover;
        [SerializeField] private Sprite tabSpriteActive;

        //List of tabs and pages
        [SerializeField] private List<Tab> tabs;
        [SerializeField] private List<GameObject> pages;

        //selected tab
        private Tab activingTab;


        private void Awake() {
                gameMenu = transform.GetChild(0).gameObject;
                if (tabs != null) {
                        activingTab = tabs[0];
                }
        }

        public void OpenGameMenu() {
                gameMenu.SetActive(true);
        }
        public void CloseGameMenu() {
                gameMenu.SetActive(false);
        }

        #region �����Ʊ�ǩѡ��
        //�������ǩ
        public void OnTabEnter(Tab tab) {
                ResetTabs();
                if (activingTab == null || tab != activingTab) {
                        tab.background.sprite = tabSpriteHover;
                }
        }
        //����뿪��ǩ
        public void OnTabExit(Tab tab) {
                ResetTabs();
        }
        #endregion

        #region ���̿��Ʊ�ǩѡ��
        //��һҳ
        public void OnNextPage() {
                activingTab = tabs[(tabs.IndexOf(activingTab) + 1) % tabs.Count];
                SelectTab(activingTab);
        }
        //��һҳ
        public void OnLastPage() {
                activingTab = tabs[(tabs.IndexOf(activingTab) + tabs.Count - 1) % tabs.Count];
                SelectTab(activingTab);
        }
        #endregion

        //ѡ��ñ�ǩ
        public void SelectTab(Tab tab) {
                activingTab = tab;
                ResetTabs();
                ActiveTab(activingTab);
        }

        //�����ǩ
        private void ActiveTab(Tab tab) {
                tab.background.sprite = tabSpriteActive;
                SwitchPage(tab);
        }

        //�л�����Ӧҳ��
        private void SwitchPage(Tab tab) {
                int index = tabs.IndexOf(tab);
                for (int i = 0; i < pages.Count; i++) {
                        if (i == index) {
                                pages[i].SetActive(true);
                        }
                        else {
                                pages[i].SetActive(false);
                        }
                }
        }

        //�������зǼ����б�ǩ�ı���ͼƬ
        private void ResetTabs() {
                foreach (Tab tab in tabs) {
                        if (activingTab != null && tab == activingTab) { continue; }
                        tab.background.sprite = tabSpriteInactive;
                }
        }
}