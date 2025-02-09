using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace VisualStudioCodeOpener
{
    public class VisualStudioCodeOpenerPreferences : SettingsProvider
    {
        private static string[] extensions;
        private static string newExtension = "";

        private static readonly string EditorPrefsKey = "VisualStudioCodeOpener_Extensions";
        private static readonly string EditorPrefsKey_VSCodePath = "VisualStudioCodeOpener_VSCodePath";

        private static readonly string s_extensionTextFieldName = "VisualStudioCodeOpener_ExtensionTextField";

        static class Styles
        {
            public static readonly GUIContent TimeUnitLabel = new GUIContent("Visual Studio Code Path");
        }

        public VisualStudioCodeOpenerPreferences(string path, SettingsScope scope = SettingsScope.User)
            : base(path, scope) { }

        [SettingsProvider]
        public static SettingsProvider CreateVisualStudioCodeOpenerPreferences()
        {
            return new VisualStudioCodeOpenerPreferences("Preferences/Visual Studio Code Opener", SettingsScope.User)
            {
                guiHandler = searchContext =>
                {
                    DrawGUI();
                }
            };

            
        }

        //public override void OnGUI(string searchContext)
        private static void DrawGUI()
        {
            using (new SettingsProviderGUIScope())
            {
                string vsCodePath = GetVSCodePathFromEditorPrefs();

                // Visual Studio Code path
                GUILayout.Space(6f);

                GUILayout.Label("Visual Studio Code Path", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal();
                GUI.enabled = false;
                vsCodePath = EditorGUILayout.TextField(vsCodePath);
                GUI.enabled = true;
                if (GUILayout.Button("Browse", GUILayout.Width(60)))
                {
                    string path = EditorUtility.OpenFilePanel("Select Visual Studio Code Executable", "", "");
                    if (!string.IsNullOrEmpty(path))
                    {
                        if (Path.GetFileName(path).Equals(GetExecutableNameWithExtension(), StringComparison.OrdinalIgnoreCase))
                        {
                            vsCodePath = path;
                            SaveVSCodePathToEditorPrefs(vsCodePath);
                        }
                        else
                        {
                            EditorUtility.DisplayDialog("Invalid Path", "Only a path to Visual Studio Code is valid.", "OK");
                        }
                    }
                }
                GUILayout.EndHorizontal();

                // Save the VS Code path if it was manually edited
                if (GUI.changed)
                {
                    SaveVSCodePathToEditorPrefs(vsCodePath);
                }

                GUILayout.Space(20);

                // Add new extension
                GUI.SetNextControlName(s_extensionTextFieldName);
                GUILayout.Label("Add New Extension", EditorStyles.boldLabel);

                GUILayout.BeginHorizontal();
                newExtension = EditorGUILayout.TextField(newExtension);

                if (GUILayout.Button("Add", GUILayout.Width(60)))
                {
                    string tempExtension = newExtension;

                    if (!tempExtension.StartsWith("."))
                    {
                        tempExtension = "." + tempExtension;
                    }

                    if (tempExtension.Equals(".cs", StringComparison.OrdinalIgnoreCase))
                    {
                        EditorUtility.DisplayDialog("Invalid Extension", "C# files are not supported, they are intended to be opened by the default configured IDE.", "OK");
                    }
                    else if (IsValidExtension(tempExtension))
                    {
                        AddExtension(tempExtension);
                        newExtension = "";

                        string nameOfFocusedControl = GUI.GetNameOfFocusedControl();
                        if (nameOfFocusedControl == s_extensionTextFieldName)
                        {
                            GUI.FocusControl(null);
                        }
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Invalid Extension", "Extensions can only contain letters and numbers.", "OK");
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(10);
                GUILayout.Label("File Extensions to Open in Visual Studio Code", EditorStyles.boldLabel);
                // Load extensions from EditorPrefs
                extensions = GetExtensionsFromEditorPrefs();

                // Display current extensions
                for (int i = 0; i < extensions.Length; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUI.enabled = false;
                    extensions[i] = EditorGUILayout.TextField(extensions[i]);
                    GUI.enabled = true;
                    if (GUILayout.Button("Remove", GUILayout.Width(60)))
                    {
                        RemoveExtension(i);
                    }
                    GUILayout.EndHorizontal();
                }
            }
        }

        private static void AddExtension(string extension)
        {
            extension = extension.ToLower();
            if (!string.IsNullOrEmpty(extension) && !extensions.Contains(extension))
            {
                // Add the new extension
                string[] newExtensions = new string[extensions.Length + 1];
                extensions.CopyTo(newExtensions, 0);
                newExtensions[extensions.Length] = extension.StartsWith(".") ? extension : "." + extension;

                // Save to EditorPrefs
                SaveExtensionsToEditorPrefs(newExtensions);
            }
        }

        private static void RemoveExtension(int index)
        {
            if (index >= 0 && index < extensions.Length)
            {
                // Remove the extension
                string[] newExtensions = new string[extensions.Length - 1];
                for (int i = 0, j = 0; i < extensions.Length; i++)
                {
                    if (i != index)
                    {
                        newExtensions[j++] = extensions[i];
                    }
                }

                // Save to EditorPrefs
                SaveExtensionsToEditorPrefs(newExtensions);
            }
        }

        internal static string[] GetExtensionsFromEditorPrefs()
        {
            // Retrieve the extensions from EditorPrefs
            string extensionsString = EditorPrefs.GetString(EditorPrefsKey, "");

            if (string.IsNullOrWhiteSpace(extensionsString))
            {
                return Array.Empty<string>();
            }

            return extensionsString.Split(',');
        }

        private static string GetExecutableNameWithExtension()
        {
#if UNITY_EDITOR_WIN
            return "code.exe";
#elif UNITY_EDITOR_OSX
            return "Visual Studio Code.app";
#elif UNITY_EDITOR_LINUX
            return "code";
#endif
            throw new PlatformNotSupportedException();
        }

        private static void SaveExtensionsToEditorPrefs(string[] extensions)
        {
            // Save the extensions to EditorPrefs
            string extensionsString = string.Join(",", extensions);
            EditorPrefs.SetString(EditorPrefsKey, extensionsString);
        }

        internal static string GetVSCodePathFromEditorPrefs()
        {
            // Retrieve the Visual Studio Code path from EditorPrefs
            return EditorPrefs.GetString(EditorPrefsKey_VSCodePath, "");
        }

        internal static bool IsVSCodePathValid()
        {
            return !string.IsNullOrWhiteSpace(GetVSCodePathFromEditorPrefs());
        }

        private static void SaveVSCodePathToEditorPrefs(string path)
        {
            // Save the Visual Studio Code path to EditorPrefs
            EditorPrefs.SetString(EditorPrefsKey_VSCodePath, path);
        }

        private static bool IsValidExtension(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
                return false;

            // Ensure the extension starts with a '.' and contains only valid characters
            if (!extension.StartsWith("."))
                return false;

            if (extension.Length == 1)
                return false;

            for (int i = 1; i < extension.Length; i++)
            {
                char c = extension[i];
                if (!char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

internal class SettingsProviderGUIScope : GUI.Scope
{
    public SettingsProviderGUIScope(int offset = 10)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(offset);
        GUILayout.BeginVertical();
    }

    protected override void CloseScope()
    {
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }
}