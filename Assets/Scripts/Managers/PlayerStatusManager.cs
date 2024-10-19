using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour {
        public static PlayerStatusManager Instance { get; private set; }

        [SerializeField] private PlayerBeginConfig playerBeginConfig;

        private void Awake() {
                if (Instance != null && Instance != this) {
                        Destroy(gameObject);
                }
                else {
                        Instance = this;
                        DontDestroyOnLoad(gameObject);
                }
        }


        [HideInInspector] public int maxHP { get; private set; }
        [HideInInspector] public int currentHP { get; private set; }

        //主菜单New Game
        public void SetStatusNewGame() {
                maxHP = playerBeginConfig.maxHP;
                currentHP = maxHP;
        }

        //玩家在每次进入地图时都要调用此方法，来更新自己的状态
        public void UpdateStatus(out int _maxHP, out int _currentHP) {
                _maxHP = maxHP;
                _currentHP = currentHP;
        }

        //玩家在每次离开地图时都要调用此方法，来将当前状态存储
        public void UpdateStatus() {
                currentHP = Player.GetCurrentHP();
        }
}
