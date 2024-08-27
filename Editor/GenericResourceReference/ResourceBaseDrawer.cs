using UnityEditor;
using UnityEngine;

namespace GenericResourceReference {
    public abstract class ResourceBaseDrawer : PropertyDrawer {
        const string _pathPropertyName = "_path";
        const string _messagePropertyName = "_message";
        const string _objectPropertyName = "_object";
        const string _unfoldedPropertyName = "_unfolded";

        public override float GetPropertyHeight(SerializedProperty property, GUIContent _) {
            var objectProperty = property.FindPropertyRelative(_objectPropertyName);
            var context = objectProperty.CreateHeightContext();
            if (!context.ShouldDisplayMessage)
                return EditorGUIUtility.singleLineHeight;

            var unfoldedProperty = property.FindPropertyRelative(_unfoldedPropertyName);
            var isUnfolded = unfoldedProperty.boolValue;
            if (isUnfolded)
                return EditorGUIUtility.singleLineHeight * 2;

            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var originalObjectProperty = property.FindPropertyRelative(_objectPropertyName);
            var originalMessageProperty = property.FindPropertyRelative(_messagePropertyName);
            var originalPathProperty = property.FindPropertyRelative(_pathPropertyName);
            
            var context = originalObjectProperty.CreateGuiContext(position, originalMessageProperty);
            using var _ = new ColorScope(Color.red, condition: context.IsError);

            using var temporarySerializedObject = new SerializedObject(property.serializedObject.targetObject);
            var temporaryObjectProperty = temporarySerializedObject.FindProperty(originalObjectProperty.propertyPath);

            using var changeCheck = new EditorGUI.ChangeCheckScope();
            DrawObjectProperty(context, temporaryObjectProperty, label);
            if (changeCheck.changed) {
                UpdateOriginalProperties(temporaryObjectProperty, originalPathProperty,
                    originalMessageProperty, originalObjectProperty);
            }

            if (context.ShouldDisplayMessage && DrawFoldout(context, property))
                DrawMessage(context);
        }

        static void DrawObjectProperty(GuiContext context, SerializedProperty objectProperty, GUIContent label) =>
            EditorGUI.ObjectField(context.ObjectRect, objectProperty, label);

        void UpdateOriginalProperties(
            SerializedProperty temporaryObjectProperty, SerializedProperty originalPathProperty,
            SerializedProperty originalMessageProperty, SerializedProperty originalObjectProperty) {
            var context = temporaryObjectProperty.objectReferenceValue.GetAssetContext();
            if (context.AssetState == AssetState.Missing)
                return;

            if (temporaryObjectProperty.objectReferenceValue != null &&
                !IsNewObjectAllowed(temporaryObjectProperty, context))
                return;

            originalPathProperty.stringValue = context.AssetState == AssetState.Resource ? context.ResourcePath : null;
            originalMessageProperty.stringValue = context.AssetState != AssetState.None ? context.AssetPath : null;
            originalObjectProperty.objectReferenceValue = temporaryObjectProperty.objectReferenceValue;
        }

        protected abstract bool IsNewObjectAllowed(SerializedProperty objectProperty, AssetContext context);

        static bool DrawFoldout(GuiContext context, SerializedProperty property) {
            var unfoldedProperty = property.FindPropertyRelative(_unfoldedPropertyName);
            var isUnfolded = unfoldedProperty.boolValue;

            var foldoutRect = context.FoldoutRect;
            unfoldedProperty.boolValue = EditorGUI.Foldout(foldoutRect, isUnfolded, context.FoldoutLabel);

            var currentEvent = Event.current;
            if (currentEvent.type == EventType.MouseDown && foldoutRect.Contains(currentEvent.mousePosition)) {
                unfoldedProperty.boolValue = !isUnfolded;
                currentEvent.Use();
            }

            return isUnfolded;
        }

        static void DrawMessage(GuiContext context) =>
            EditorGUI.LabelField(context.MessageRect, context.MessageLabel, context.Message, EditorStyles.helpBox);
    }
}