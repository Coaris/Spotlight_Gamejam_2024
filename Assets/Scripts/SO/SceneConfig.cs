using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "SceneConfig", menuName = "Configurations/SceneConfig")]
public class SceneConfig : ScriptableObject {
        public SceneAsset MainMenu;
        public SceneAsset Settings;
        public SceneAsset Credits;
        public SceneAsset NewGame;
}
