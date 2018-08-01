//Copyright 2017 Looking Glass Factory Inc.
//All rights reserved.
//Unauthorized copying or distribution of this file, and the source code contained herein, is strictly prohibited.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HoloPlay
{
    namespace UI
    {
        [InitializeOnLoad]
        [CustomEditor(typeof(Quilt))]
        public class QuiltEditor : Editor
        {
            SerializedProperty captures;
            SerializedProperty overrideQuilt;
            // SerializedProperty overrideViews;
            SerializedProperty renderOverrideBehind;
            SerializedProperty quiltRT;
            SerializedProperty tilesX;
            SerializedProperty tilesY;
            SerializedProperty quiltW;
            SerializedProperty quiltH;
            SerializedProperty tilingPresetIndex;
            SerializedProperty tileSizeX;
            SerializedProperty tileSizeY;
            SerializedProperty numViews;
            SerializedProperty onQuiltSetup;
            SerializedProperty advancedFoldout;
            SerializedProperty renderIn2D;
            SerializedProperty debugPrintoutKey;
            SerializedProperty screenshot2DKey;
            SerializedProperty screenshot3DKey;
            SerializedProperty forceConfigResolution;
#if CALIBRATOR
            SerializedProperty config;
#endif

            void OnEnable()
            {
                captures = serializedObject.FindProperty("captures");
                overrideQuilt = serializedObject.FindProperty("overrideQuilt");
                // overrideViews = serializedObject.FindProperty("overrideViews");
                renderOverrideBehind = serializedObject.FindProperty("renderOverrideBehind");
                quiltRT = serializedObject.FindProperty("quiltRT");
                tilesX = serializedObject.FindProperty("tiling").FindPropertyRelative("tilesX");
                tilesY = serializedObject.FindProperty("tiling").FindPropertyRelative("tilesY");
                quiltW = serializedObject.FindProperty("tiling").FindPropertyRelative("quiltW");
                quiltH = serializedObject.FindProperty("tiling").FindPropertyRelative("quiltH");
                tilingPresetIndex = serializedObject.FindProperty("tilingPresetIndex");
                tileSizeX = serializedObject.FindProperty("tiling").FindPropertyRelative("tileSizeX");
                tileSizeY = serializedObject.FindProperty("tiling").FindPropertyRelative("tileSizeY");
                numViews = serializedObject.FindProperty("tiling").FindPropertyRelative("numViews");
                onQuiltSetup = serializedObject.FindProperty("onQuiltSetup");
                advancedFoldout = serializedObject.FindProperty("advancedFoldout");
                renderIn2D = serializedObject.FindProperty("renderIn2D");
                debugPrintoutKey = serializedObject.FindProperty("debugPrintoutKey");
                screenshot2DKey = serializedObject.FindProperty("screenshot2DKey");
                screenshot3DKey = serializedObject.FindProperty("screenshot3DKey");
                forceConfigResolution = serializedObject.FindProperty("forceConfigResolution");
#if CALIBRATOR
                config = serializedObject.FindProperty("config");
#endif
            }

            public override void OnInspectorGUI()
            {
                serializedObject.Update();
                Quilt quilt = (Quilt)target;

                EditorGUILayout.Space();

                GUI.color = Misc.guiColor;
                EditorGUILayout.LabelField("- Quilt -", EditorStyles.whiteMiniLabel);
                GUI.color = Color.white;

                GUI.enabled = false;
                EditorGUILayout.PropertyField(quiltRT);
                GUI.enabled = true;

                advancedFoldout.boolValue = EditorGUILayout.Foldout(
                    advancedFoldout.boolValue,
                    "Advanced",
                    true
                );
                if (advancedFoldout.boolValue)
                {
                    EditorGUI.indentLevel++;

                    EditorGUILayout.PropertyField(captures, true);
                    EditorGUILayout.PropertyField(overrideQuilt);
                    // EditorGUILayout.PropertyField(overrideViews, true);
                    EditorGUILayout.PropertyField(renderOverrideBehind);
                    EditorGUILayout.PropertyField(debugPrintoutKey);
                    EditorGUILayout.PropertyField(screenshot2DKey);
                    EditorGUILayout.PropertyField(screenshot3DKey);
                    EditorGUILayout.PropertyField(forceConfigResolution);

                    List<string> tilingPresetNames = new List<string>();
                    foreach (var p in Quilt.tilingPresets)
                    {
                        tilingPresetNames.Add(p.presetName);
                    }
                    tilingPresetNames.Add("Default (determined by quality setting)");
                    tilingPresetNames.Add("Custom");

                    EditorGUI.BeginChangeCheck();
                    tilingPresetIndex.intValue = EditorGUILayout.Popup(
                        "Tiling",
                        tilingPresetIndex.intValue,
                        tilingPresetNames.ToArray()
                    );
                    if (EditorGUI.EndChangeCheck())
                    {
                        serializedObject.ApplyModifiedProperties();
                    }

                    // if it's a custom
                    int custom = Quilt.tilingPresets.Length + 1;
                    if (tilingPresetIndex.intValue == custom)
                    {
                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(tilesX);
                        EditorGUILayout.PropertyField(tilesY);
                        EditorGUILayout.PropertyField(quiltW);
                        EditorGUILayout.PropertyField(quiltH);
                        if (EditorGUI.EndChangeCheck())
                        {
                            quilt.ApplyPreset();
                        }
                    }

                    string tilingDisplay = numViews.displayName + ": " + numViews.intValue.ToString() + "\n";
                    tilingDisplay += "Tiles: " + tilesX.intValue + " x " + tilesY.intValue.ToString() + "\n";
                    tilingDisplay += "Quilt Size: " + quiltW.intValue.ToString() + " x " +
                                    quiltH.intValue.ToString() + " px" + "\n";
                    tilingDisplay += "Tile Size: " + tileSizeX.intValue.ToString() + " x " +
                                    tileSizeY.intValue.ToString() + " px";

                    EditorGUILayout.LabelField(tilingDisplay, EditorStyles.helpBox);

                    // on quilt setup event
                    EditorGUILayout.PropertyField(onQuiltSetup);

                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.Space();

                GUI.color = Misc.guiColor;
                EditorGUILayout.LabelField("- Preview -", EditorStyles.whiteMiniLabel);
                GUI.color = Color.white;

                EditorGUILayout.PropertyField(renderIn2D);

                string previewerShortcutKey = "Ctrl + E";
                string settingsShortcutKey = "Ctrl + Shift + E";
#if UNITY_EDITOR_OSX
                previewerShortcutKey = "⌘E";
                settingsShortcutKey = "⌘^E";
#endif

                if (GUILayout.Button(new GUIContent(
                    "Toggle Preview (" + previewerShortcutKey + ")",
                    "If your LKG device is set up as a second display, " +
                    "this will generate a game window on it to use as a " +
                    "realtime preview"),
                    EditorStyles.miniButton
                ))
                {
                    PreviewWindow.ToggleWindow();
                }

                if (GUILayout.Button(new GUIContent(
                    "Settings (" + settingsShortcutKey + ")",
                    "Use to set previewer position"),
                    EditorStyles.miniButton
                ))
                {
                    EditorApplication.ExecuteMenuItem("HoloPlay/Preview Settings");
                }

                EditorGUILayout.Space();

                GUI.color = Misc.guiColor;
                EditorGUILayout.LabelField("- Config -", EditorStyles.whiteMiniLabel);
                GUI.color = Color.white;

#if CALIBRATOR
                EditorGUILayout.PropertyField(config, true);
#endif

                if (GUILayout.Button(new GUIContent(
                    "Reload Config",
                    "Reload the config, only really necessary if " +
                    "you edited externally and the new config settings won't load"),
                    EditorStyles.miniButton
                ))
                {
                    quilt.LoadConfig();
                }

                EditorGUILayout.Space();

                GUI.color = Misc.guiColor;
                EditorGUILayout.LabelField("- Project Settings -", EditorStyles.whiteMiniLabel);
                GUI.color = Color.white;

                if (GUILayout.Button(new GUIContent(
                    "Optimization Settings",
                    "Open a window that will let you select project settings " +
                    "to be optimized for best performance with HoloPlay"),
                    EditorStyles.miniButton
                ))
                {
                    OptimizationSettings window = EditorWindow.GetWindow<OptimizationSettings>();
                    window.Show();
                }

                EditorGUILayout.Space();

                serializedObject.ApplyModifiedProperties();
            }

            [MenuItem("GameObject/HoloPlay Capture", false, 10)]
            public static void CreateHoloPlay()
            {
                var asset = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/HoloPlay/HoloPlay Capture.prefab", typeof(GameObject));
                if (asset == null)
                {
                    Debug.LogWarning(Misc.debugLogText + "Couldn't find the holoplay capture folder or prefab. Must be at Assets/HoloPlay/HoloPlay Capture");
                    return;
                }

                var clone = Instantiate(asset, Vector3.zero, Quaternion.identity);
                clone.name = asset.name;
            }
        }
    }
}