using UnityEngine;
using UnityEditor;

using static System.IO.Directory;
using static System.IO.Path;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;

namespace tannerhbrewer.tools {

    public class SetupWindow : EditorWindow {

        public enum FolderSetting {
            Default,
            Custom
        }

        [MenuItem("Tools/Setup")]
        public static void LaunchWindow() => GetWindow<SetupWindow>("Setup Project");

        public FolderSetting folderSetting;

        SerializedObject so;
        SerializedProperty propFolderSetting;

        void OnEnable() {

            so = new SerializedObject(this);
            propFolderSetting = so.FindProperty("folderSetting");

        }

        void OnGUI() {

            so.Update();

            EditorGUILayout.PropertyField(propFolderSetting);

            if (folderSetting == FolderSetting.Default) {
                if (GUILayout.Button("Create Default Folders")) {
                    CreateDefaultFolders();
                }
            }
            else if (folderSetting == FolderSetting.Custom) {

            }

            so.ApplyModifiedProperties();

        }

        private void CreateDefaultFolders() {

            TryCreateDirectory("External", "Framework", "PLAYER (RENAME)", "Game", "Resources");
            TryCreateParentDirectory("Framework", "Input");
            TryCreateParentDirectory("Player (RENAME)", "Code", "Resources");
            Refresh();

        }

        private void TryCreateParentDirectory(string root, params string[] children) {

            var fullpath = Combine(dataPath, root);
            foreach (var newDirectory in children)
                CreateDirectory(Combine(fullpath, newDirectory));

        }

        private void TryCreateDirectory(params string[] children) {

            foreach (var newDirectory in children)
                CreateDirectory(Combine(dataPath, newDirectory));

        }

    }

}