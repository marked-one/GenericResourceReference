using UnityEditor;

namespace GenericResourceReference {
    [CustomPropertyDrawer(typeof(IEditorResourceFolder), true)]
    public class ResourceFolderDrawer : ResourceBaseDrawer {
        protected override bool IsNewObjectAllowed(SerializedProperty objectProperty, AssetContext context) =>
            context.AssetState != AssetState.None && AssetDatabase.IsValidFolder(context.AssetPath);
    }
}