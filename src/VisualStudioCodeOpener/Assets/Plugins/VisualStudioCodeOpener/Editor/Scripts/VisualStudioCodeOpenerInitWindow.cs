using UnityEditor;
using UnityEngine;

namespace VisualStudioCodeOpener
{
    internal class VisualStudioCodeOpenerInitWindow : EditorWindow
    {
        private static readonly string s_IgnoreWindowAtStartKey = "VisualStudioCodeOpener_IgnoreWindowAtStart";
        private static readonly string s_alreadyAttemptedWindowOpenKey = "VisualStudioCodeOpener_AlreadyAttemptedWindowOpen";

        [InitializeOnLoadMethod]
        private static void OpenWindow()
        {
            if (!EditorPrefs.HasKey(s_IgnoreWindowAtStartKey))
            {
                EditorPrefs.SetBool(s_IgnoreWindowAtStartKey, false);
            }

            EditorApplication.delayCall += () =>
            {
                if (SessionState.GetBool(s_alreadyAttemptedWindowOpenKey, false))
                {
                    return;
                }

                SessionState.SetBool(s_alreadyAttemptedWindowOpenKey, true);

                bool ignoreAtStart = EditorPrefs.GetBool(s_IgnoreWindowAtStartKey, false);

                bool vsCodePathValid = VisualStudioCodeOpenerPreferences.IsVSCodePathValid();
    
                bool open = !ignoreAtStart && !vsCodePathValid;

                if (open)
                {
                    VisualStudioCodeOpenerInitWindow wnd = GetWindow<VisualStudioCodeOpenerInitWindow>(true, "Visual Studio Code Opener");
                    wnd.minSize = new Vector2(400, 200);
                    wnd.maxSize = new Vector2(400, 200);
                    wnd.ShowUtility();
                }
            };
        }

        private void OnGUI()
        {
            // Set up a padding and spacing for better readability
            GUILayout.Space(10); // Add some space at the top

            // Create a centered label for the message
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 14,
                wordWrap = true
            };
            GUILayout.Label("No path specified for Visual Studio Code Editor", labelStyle, GUILayout.ExpandWidth(true));

            GUILayout.Space(20); // Add more space between the label and the button

            // Center the button horizontally
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace(); // Push the button to the center

                if (GUILayout.Button("Open Preferences", GUILayout.Width(150), GUILayout.Height(30)))
                {
                    // Open the preferences window to a specific page
                    SettingsService.OpenUserPreferences("Preferences/Visual Studio Code Opener");
                }

                GUILayout.FlexibleSpace(); // Push the button to the center
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(20); // Add more space between the button and the toggle

            // Center the toggle horizontally
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace(); // Push the toggle to the center

                bool ignoreAtStart = GUILayout.Toggle(EditorPrefs.GetBool(s_IgnoreWindowAtStartKey), "Don't show again");

                if (GUI.changed)
                {
                    EditorPrefs.SetBool(s_IgnoreWindowAtStartKey, ignoreAtStart);
                }

                GUILayout.FlexibleSpace(); // Push the toggle to the center
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10); // Add some space at the bottom
        }
    }
}