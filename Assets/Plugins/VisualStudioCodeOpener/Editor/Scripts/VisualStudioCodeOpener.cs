using UnityEditor;
using System;
using System.Diagnostics;
using System.Linq;

namespace VisualStudioCodeOpener
{
    internal class VisualStudioCodeOpener : AssetPostprocessor
    {
        [UnityEditor.Callbacks.OnOpenAsset(0)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            string path = AssetDatabase.GetAssetPath(instanceID);

            string[] extensions = VisualStudioCodeOpenerPreferences.GetExtensionsFromEditorPrefs();

            if (extensions.Any(ext => path.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
            {
                string fullPath = System.IO.Path.GetFullPath(path);

                OpenFileInVSCode(fullPath);

                // Return true to indicate that we've handled the opening of this asset
                return true;
            }

            // Return false to let Unity handle other files (e.g., .cs files)
            return false;
        }

        private static void OpenFileInVSCode(string filePath)
        {
            if (!VisualStudioCodeOpenerPreferences.IsVSCodePathValid())
            {
                return;
            }

            string vsCodePath = VisualStudioCodeOpenerPreferences.GetVSCodePathFromEditorPrefs();

            Process process = new Process();
            process.StartInfo.FileName = vsCodePath;
            process.StartInfo.Arguments = $"\"{filePath}\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;

            try
            {
                process.Start();
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError($"Failed to open file in Visual Studio Code: {e.Message}");
            }
        }
    }
}