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

        //�����ÿ�ν����ͼ,������Ϸ������ʱ��Ҫ���ô˷������������Լ���״̬
        public void ReadStatus(out int _maxHP, out int _currentHP) {
                _maxHP = maxHP;
                _currentHP = currentHP;
        }

        //�����ÿ���뿪��ͼʱ��Ҫ���ô˷�����������ǰ״̬�洢
        public void WriteStatus() {
                currentHP = Player.GetCurrentHP();
        }

        //������Ϸ������ǰ���ô˷�����������ǰ������Դ洢
        public void WriteStatusOnReburn(int _maxHP) {
                maxHP = _maxHP;
                currentHP = maxHP;
        }
}
