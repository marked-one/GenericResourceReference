using UnityEditor;
using UnityEngine;

namespace GenericResourceReference {
    public class GuiContext {
        const string _emptyLabel = " ";
        const string _notAResourceMessagePrefix = "Not a Resource: ";

        readonly Rect _position;
        readonly AssetState _assetState;
        readonly string _message;

        public GuiContext(
            Rect position, AssetState assetState, string message) {
            _position = position;
            _assetState = assetState;
            _message = message;
        }

        public bool IsError => _assetState is AssetState.Missing or AssetState.Asset;

        public Rect ObjectRect => new() {
            x = ShouldDisplayMessage ? _position.x + EditorGUIUtility.singleLineHeight : _position.x,
            y = _position.y,
            width = ShouldDisplayMessage ? _position.width - EditorGUIUtility.singleLineHeight : _position.width,
            height = EditorGUIUtility.singleLineHeight
        };

        public Rect FoldoutRect => new() {
            x = _position.x, y = _position.y,
            width = EditorGUIUtility.singleLineHeight,
            height = EditorGUIUtility.singleLineHeight
        };

        public string FoldoutLabel => string.Empty;

        public bool ShouldDisplayMessage => _assetState != AssetState.None;

        public Rect MessageRect => new() {
            x = _position.x + EditorGUIUtility.singleLineHeight,
            y = _position.y + EditorGUIUtility.singleLineHeight,
            width = _position.width - EditorGUIUtility.singleLineHeight * 2,
            height = EditorGUIUtility.singleLineHeight
        };

        public string MessageLabel => _emptyLabel;

        public string Message {
            get {
                if (!ShouldDisplayMessage)
                    return null;

                if (!IsAsset)
                    return _message;

                return _notAResourceMessagePrefix + _message;
            }
        }

        bool IsAsset {
            get {
                if (_assetState == AssetState.Asset)
                    return true;

                if (_assetState != AssetState.Missing)
                    return false;

                if (_message.IsResourcePath())
                    return false;

                return !_message.IsResourcesFolder();
            }
        }
    }
}