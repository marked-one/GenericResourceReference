using UnityEditor;

namespace GenericResourceReference {
    [CustomPropertyDrawer(typeof(IEditorResourceReference), true)]
    public class ResourceReferenceDrawer : ResourceBaseDrawer {
        protected override bool IsNewObjectAllowed(SerializedProperty objectProperty, AssetContext context) =>
            context.AssetState != AssetState.None && !AssetDatabase.IsValidFolder(context.AssetPath);
    }
}