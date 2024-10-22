using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem {
        public static void SaveGame(string saveFileName, object data) {
                var json = JsonUtility.ToJson(data);
                var path = Path.Combine(Application.persistentDataPath, saveFileName);
                Debug.Log(path);
                File.WriteAllText(path, json);
        }

        public static T LoadGame<T>(string saveFileName) {
                var path = Path.Combine(Application.persistentDataPath, saveFileName);

                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<T>(json);
                return data;
        }

        //public static void DeleteSave(string saveFileName) {
        //        var path = Path.Combine(Application.persistentDataPath, saveFileName);

        //        File.Delete(path);
        //}
}