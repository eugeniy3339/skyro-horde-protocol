using HordeProtocol.Data;
using UnityEditor;
using UnityEngine;
using HordeProtocol.Tests;

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
                WavesValidator.Validate();
            }
        }
    }
}
