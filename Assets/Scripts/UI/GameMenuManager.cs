using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour {
        public static GameMenuManager Instance { get; private set; }

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

        public MapLoadFader mapLoadFader;

        private void Awake() {
                if (Instance != null && Instance != this) {
                        Destroy(gameObject);
                }
                else {
                        Instance = this;
                        DontDestroyOnLoad(gameObject);
                }


                gameMenu = transform.GetChild(0).gameObject;
                if (tabs != null) {
                        activingTab = tabs[0];
                }
        }

        //外部调用的打开菜单
        public void OpenGameMenu() {
                gameMenu.SetActive(true);
        }
        //外部调用的关闭菜单
        public void CloseGameMenu() {
                gameMenu.SetActive(false);
        }

        #region 光标控制标签选择
        //光标进入标签
        public void OnTabEnter(Tab tab) {
                ResetTabs();
                if (activingTab == null || tab != activingTab) {
                        tab.background.sprite = tabSpriteHover;
                }
        }
        //光标离开标签
        public void OnTabExit(Tab tab) {
                ResetTabs();
        }
        #endregion

        #region 键盘控制标签选择
        //下一页
        public void OnNextPage() {
                activingTab = tabs[(tabs.IndexOf(activingTab) + 1) % tabs.Count];
                SelectTab(activingTab);
        }
        //上一页
        public void OnLastPage() {
                activingTab = tabs[(tabs.IndexOf(activingTab) + tabs.Count - 1) % tabs.Count];
                SelectTab(activingTab);
        }
        #endregion

        //选择该标签
        public void SelectTab(Tab tab) {
                //事件触发
                if (activingTab != null) { activingTab.OnDeselected(); }
                activingTab = tab;
                activingTab.OnSelected();
                //视觉与逻辑切换
                ResetTabs();
                ActiveTab(activingTab);
        }

        //激活标签
        private void ActiveTab(Tab tab) {
                tab.background.sprite = tabSpriteActive;
                SwitchPage(tab);
        }

        //切换到对应页面
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

        //重置所有非激活中标签的背景图片
        private void ResetTabs() {
                foreach (Tab tab in tabs) {
                        if (activingTab != null && tab == activingTab) { continue; }
                        tab.background.sprite = tabSpriteInactive;
                }
        }
}