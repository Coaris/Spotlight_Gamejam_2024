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

        //���˵�New Game
        public void SetStatusNewGame() {
                maxHP = playerBeginConfig.maxHP;
                currentHP = maxHP;
        }

        //�����ÿ�ν����ͼʱ��Ҫ���ô˷������������Լ���״̬
        public void UpdateStatus(out int _maxHP, out int _currentHP) {
                _maxHP = maxHP;
                _currentHP = currentHP;
        }

        //�����ÿ���뿪��ͼʱ��Ҫ���ô˷�����������ǰ״̬�洢
        public void UpdateStatus() {
                currentHP = Player.GetCurrentHP();
        }
}
