using HordeProtocol.Data;
using UnityEditor;
using UnityEngine;

namespace HordeProtocol.Editor
{
    public sealed class WaveValidatorWindow : EditorWindow
    {
        [MenuItem("Horde/Validate Waves")]
        public static void Open()
        {
            GetWindow<WaveValidatorWindow>("Wave validator");
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Validate"))
            {
                Validate();
            }
        }

        private void Validate()
        {
            var guids = AssetDatabase.FindAssets("t:" + "WaveDefenition");
            foreach(var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = (WaveDefenition)AssetDatabase.LoadAssetAtPath(path, typeof(WaveDefenition));
                ValidateScriptableObject(asset);
            }
        }

        private void ValidateScriptableObject(WaveDefenition waveDefenition)
        {
            string errorToDebug = string.Empty;

            foreach(var enemyToSpawn in waveDefenition.EnemiesToSpawn)
            {
                if (enemyToSpawn.EnemyDefenition == null)
                    errorToDebug += ("--> Enemy Defenition is not setted up in " + waveDefenition.name + "!!! If you dont set it up correctly your enemy wont be spawned. \n");
                if (enemyToSpawn.Count <= 0)
                    errorToDebug += ("--> Enemies count to spawn is setuped to 0 or less in " + waveDefenition.name + "!!! If you dont set it up correctly your enemy wont be spawned. \n");
            }

            if (errorToDebug.Length > 0)
            {
                Debug.LogError(errorToDebug, waveDefenition);
            }
        }
    }
}
