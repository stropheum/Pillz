using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolbarExtender;

namespace Pillz.Editor
{
    internal static class ToolbarStyles
    {
        public static readonly GUIStyle CommandButtonStyle;
        public static readonly GUIStyle DropdownStyle;

        static ToolbarStyles()
        {
            CommandButtonStyle = new GUIStyle("Command")
            {
                fontSize = 16,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Bold,
                border = new RectOffset(4, 4, 4, 4)
            };

            DropdownStyle = new GUIStyle(EditorStyles.popup)
            {
                fontSize = 12,
                alignment = TextAnchor.MiddleCenter,
                fixedHeight = 22,
                stretchHeight = false
            };
        }
    }

    [InitializeOnLoad]
    public class PlayFromBootstrapButton
    {
        private static string[] _allScenePaths;
        private static string[] _allSceneNames;

        static PlayFromBootstrapButton()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
            RefreshSceneList();
        }

        private static void RefreshSceneList()
        {
            string[] guids = AssetDatabase.FindAssets("t:scene", new[] { "Assets/Scenes" });
            _allScenePaths = new string[guids.Length];
            _allSceneNames = new string[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                _allScenePaths[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
                _allSceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(_allScenePaths[i]);
            }
        }

        private static void OnToolbarGUI()
        {
            Color oldBackgroundColor = GUI.backgroundColor;
            Color oldColor = GUI.color;
            Color oldContentColor = GUI.contentColor;

            GUILayout.FlexibleSpace();
            ConfigureSceneDropdown();
            ConfigureBootstrapButton();

            GUI.backgroundColor = oldBackgroundColor;
            GUI.color = oldColor;
            GUI.contentColor = oldContentColor;
        }

        private static void ConfigureBootstrapButton()
        {
            GUI.enabled = !EditorApplication.isPlaying;
            Texture playIcon = EditorGUIUtility.TrIconContent("rocket").image;
            if (GUILayout.Button(new GUIContent(playIcon, "Start From Bootstrap Scene"), ToolbarStyles.CommandButtonStyle))
            {
                SceneHelper.StartScene("_Bootstrap");
            }

            GUI.enabled = true;
        }

        private static void ConfigureSceneDropdown()
        {
            if (_allSceneNames is not { Length: > 0 }) { return; }

            GUI.enabled = !EditorApplication.isPlaying;
            string currentScenePath = SceneManager.GetActiveScene().path;
            int currentIndex = System.Array.IndexOf(_allScenePaths, currentScenePath);
            int selectedIndex = EditorGUILayout.Popup(currentIndex, _allSceneNames, ToolbarStyles.DropdownStyle, GUILayout.Width(180));
            GUI.enabled = true;

            if (selectedIndex < 0 || selectedIndex == currentIndex) { return; }
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) { return; }

            EditorSceneManager.OpenScene(_allScenePaths[selectedIndex]);
        }
    }

    internal static class SceneHelper
    {
        private static string _sceneToOpen;

        public static void StartScene(string sceneName)
        {
            if (EditorApplication.isPlaying) { EditorApplication.isPlaying = false; }

            _sceneToOpen = sceneName;
            EditorApplication.update += OnUpdate;
        }

        private static void OnUpdate()
        {
            if (_sceneToOpen == null
                || EditorApplication.isPlaying
                || EditorApplication.isPaused
                || EditorApplication.isCompiling
                || EditorApplication.isPlayingOrWillChangePlaymode) { return; }

            EditorApplication.update -= OnUpdate;

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                string[] guids = AssetDatabase.FindAssets("t:scene " + _sceneToOpen, null);
                if (guids.Length == 0) { Debug.LogWarning("Couldn't find scene file"); }
                else
                {
                    string scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);
                    EditorSceneManager.OpenScene(scenePath);
                    EditorApplication.isPlaying = true;
                }
            }

            _sceneToOpen = null;
        }
    }
}