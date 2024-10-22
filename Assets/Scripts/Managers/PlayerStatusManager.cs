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

        //玩家在每次进入地图,加载游戏，重生时都要调用此方法，来更新自己的状态
        public void ReadStatus(out int _maxHP, out int _currentHP) {
                _maxHP = maxHP;
                _currentHP = currentHP;
        }

        //玩家在每次离开地图时都要调用此方法，来将当前状态存储
        public void WriteStatus() {
                currentHP = Player.GetCurrentHP();
        }

        //加载游戏或重生前调用此方法，来将当前最大属性存储
        public void WriteStatusOnReburn(int _maxHP) {
                maxHP = _maxHP;
                currentHP = maxHP;
        }
}
