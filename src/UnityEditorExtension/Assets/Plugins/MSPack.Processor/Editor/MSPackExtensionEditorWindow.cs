#if UNITY_EDITOR

using UnityEditor;

namespace MSPack.Processor.Unity.Editor
{
    // ReSharper disable once InconsistentNaming
    public class MSPackExtensionEditorWindow : EditorWindow
    {
        static MSPackExtensionEditorWindow window;

        [MenuItem("Window/MessagePack/Code Assembler")]
        public static void OpenWindow()
        {
            if (window != null)
            {
                window.Close();
            }

            // will called OnEnable(singleton instance will be set).
            GetWindow<MSPackExtensionEditorWindow>("MessagePack CodeGen").Show();
        }

        private void OnEnable() => window = this;

        private void OnGUI()
        {

        }
    }
}
#endif
