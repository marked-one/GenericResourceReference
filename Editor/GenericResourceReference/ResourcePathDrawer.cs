using UnityEditor;

namespace GenericResourceReference {
    [CustomPropertyDrawer(typeof(IEditorResourcePath), true)]
    public class ResourcePathDrawer : ResourceBaseDrawer {
        protected override bool IsNewObjectAllowed(SerializedProperty objectProperty, AssetContext context) => true;
    }
}