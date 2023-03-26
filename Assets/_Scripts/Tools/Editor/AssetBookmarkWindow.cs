using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class AssetBookmarkWindow : EditorWindow {
    #region Data

    private enum ButtonShape {
        Single,
        Left,
        Middle,
        Right
    }

    #endregion

    #region Window

    [MenuItem("Window/Glitchers/Asset Bookmark")]
    private static void OpenWindow() {
        GetWindow(typeof(AssetBookmarkWindow), false, "Asset Bookmark");
    }

    #endregion

    #region Serialized

    public List<Object> m_Objects = new();

    #endregion

    #region Variables

    private Vector2 m_Scroll = Vector2.zero;

    private Dictionary<ButtonShape, GUIStyle> m_CustomButtonStyles;

    private IAssetBookmark m_QueuedBookmark;
    private int m_QueuedBookmarkFunctionIndex;

    #endregion

    #region GUI

    public void OnGUI() {
        GUILayout.Space(4f);

        GUILayout.BeginVertical();
        m_Scroll = GUILayout.BeginScrollView(m_Scroll);

        for (int i = 0, c = m_Objects.Count; i < c; i++) {
            if (i < m_Objects.Count) {
                DrawObjectGUI(m_Objects[i], i);

                if (i < c - 1) {
                    GUILayout.Space(2f);
                }
            }
        }

        GUILayout.EndScrollView();
        GUILayout.EndVertical();

        if (GUILayout.Button("Add")) {
            m_Objects.Add(null);
        }

        GUILayout.Space(6f);

        // moved this out of the GUILayout things because some processes block the function from finishing (e.g. calling Build) resulting in GUI errors from poppin' clips etc
        if (m_QueuedBookmark != null) {
            var bookmark = m_QueuedBookmark;
            m_QueuedBookmark = null;
            bookmark.RunBookmarkFunction(m_QueuedBookmarkFunctionIndex);
        }
    }

    private void DrawObjectGUI(Object obj, int index) {
        GUILayout.BeginHorizontal();

        if (obj is SceneAsset) {
            DrawBookmarkScene(obj);
        } else if (obj is IAssetBookmark) {
            obj = EditorGUILayout.ObjectField(obj, typeof(Object), false);
            DrawBookmark(obj);
        } else {
            obj = EditorGUILayout.ObjectField(obj, typeof(Object), false);
        }

        GUI.backgroundColor = Color.red;

        if (GUILayout.Button("\u2716", GetButtonStyle(ButtonShape.Single), GUILayout.ExpandWidth(false))) {
            GUILayout.EndHorizontal();
            m_Objects.RemoveAt(index);
            return;
        }

        GUI.backgroundColor = Color.white;
        GUILayout.EndHorizontal();

        m_Objects[index] = obj;
    }

    private void DrawBookmarkScene(Object obj) {
        var scene = obj as SceneAsset;
        obj = EditorGUILayout.ObjectField(scene, typeof(SceneAsset), false);

        if (EditorApplication.isPlaying == false) {
            if (GUILayout.Button("Open", GetButtonStyle(ButtonShape.Left), GUILayout.ExpandWidth(false))) {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {
                    var scenePath = AssetDatabase.GetAssetPath(scene);
                    EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                }
            }

            GUILayout.Space(4);

            if (GUILayout.Button("Run", GetButtonStyle(ButtonShape.Right), GUILayout.ExpandWidth(false))) {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {
                    var scenePath = AssetDatabase.GetAssetPath(scene);
                    EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                    EditorApplication.isPlaying = true;
                }
            }
        }
    }

    private void DrawBookmark(Object obj) {
        var bookmark = obj as IAssetBookmark;
        var keys = bookmark.BookmarkKeys;

        for (var i = 0; i < keys.Length; i++) {
            DrawBookmarkButton(keys[i], bookmark, i, keys.Length);

            if (i < keys.Length - 1) {
                GUILayout.Space(4);
            }
        }
    }

    private void DrawBookmarkButton(string key, IAssetBookmark assetBookmark, int index, int count) {
        if (GUILayout.Button(key, GetButtonStyle(index, count), GUILayout.ExpandWidth(false))) {
            m_QueuedBookmark = assetBookmark;
            m_QueuedBookmarkFunctionIndex = index;
        }
    }

    #endregion

    #region Style

    private ButtonShape GetButtonShape(int index, int count) {
        if (count > 1) {
            if (index == 0) {
                return ButtonShape.Left;
            } else if (index == count - 1) {
                return ButtonShape.Right;
            } else {
                return ButtonShape.Middle;
            }
        }

        return ButtonShape.Single;
    }

    private GUIStyle GetMiniButtonStyle(ButtonShape buttonShape) {
        switch (buttonShape) {
            case ButtonShape.Left: return EditorStyles.miniButtonLeft;
            case ButtonShape.Middle: return EditorStyles.miniButtonMid;
            case ButtonShape.Right: return EditorStyles.miniButtonRight;
            case ButtonShape.Single:
            default:
                return EditorStyles.miniButton;
        }
    }

    private GUIStyle GetButtonStyle(ButtonShape shape) {
        if (m_CustomButtonStyles == null) {
            m_CustomButtonStyles = new Dictionary<ButtonShape, GUIStyle>();
        }

        if (m_CustomButtonStyles.ContainsKey(shape) == false) {
            var baseStyle = GetMiniButtonStyle(shape);

            var shapeStyle = new GUIStyle(baseStyle) {
                fontSize = 11
            };

            m_CustomButtonStyles[shape] = shapeStyle;
        }

        return m_CustomButtonStyles[shape];
    }

    private GUIStyle GetButtonStyle(int index, int count) {
        return GetButtonStyle(GetButtonShape(index, count));
    }

    #endregion
}