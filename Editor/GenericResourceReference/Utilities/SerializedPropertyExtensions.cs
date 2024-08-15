using UnityEditor;
using UnityEngine;

namespace GenericResourceReference {
    public static class SerializedPropertyExtensions {
        public static HeightContext CreateHeightContext(this SerializedProperty objectProperty) {
            if (objectProperty.IsMissingReference())
                return new HeightContext(AssetState.Missing);

            if (objectProperty.IsNullReference())
                return new HeightContext(AssetState.None);

            var context = objectProperty.objectReferenceValue.GetAssetContext();
            return new HeightContext(context.AssetState);
        }

        public static GuiContext CreateGuiContext(
            this SerializedProperty objectProperty, Rect position, SerializedProperty messageProperty) {
            if (objectProperty.IsMissingReference())
                return new GuiContext(position, AssetState.Missing, messageProperty.stringValue);

            if (objectProperty.IsNullReference())
                return new GuiContext(position, AssetState.None, messageProperty.stringValue);

            var context = objectProperty.objectReferenceValue.GetAssetContext();
            return new GuiContext(position, context.AssetState, messageProperty.stringValue);
        }

        static bool IsMissingReference(this SerializedProperty objectProperty) =>
            objectProperty.objectReferenceValue == null && objectProperty.objectReferenceInstanceIDValue != 0;

        static bool IsNullReference(this SerializedProperty objectProperty) => 
            objectProperty.objectReferenceValue == null;
    }
}