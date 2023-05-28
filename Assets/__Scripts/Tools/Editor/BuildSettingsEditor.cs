using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SGJGrenoble2023
{
    [CustomEditor(typeof(BuildSettings))]
    public class BuildSettingsEditor : Editor
    {
        private BuildSettings _settings;

        private void OnEnable()
        {
            _settings = (BuildSettings)target;    
        }

        public override void OnInspectorGUI()
        {
            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Build"))
                {
                    _settings.Build();
                }

                if (GUILayout.Button("Deploy"))
                {
                    _settings.Deploy();
                }
            }
            
            EditorGUILayout.Space();

            base.OnInspectorGUI();
        }
    }
}
