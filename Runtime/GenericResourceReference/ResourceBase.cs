using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace GenericResourceReference {
    public abstract class ResourceBase { }

#if UNITY_EDITOR
    [Serializable]
    public abstract class ResourceBase<TObject> :
        ResourceBase, ISerializationCallbackReceiver where TObject : UnityObject {
        [SerializeField] protected string _path;  // Is either a Resources path or null
        [SerializeField] string _message; // Editor-only. Is either last asset path or null
        [SerializeField] TObject _object; // Editor-only. Is either object reference or null
        [SerializeField] bool _unfolded; // Editor-only. Is true when _message is displayed by the PropertyDrawer

        public virtual void OnBeforeSerialize() {
            var assetContext = _object.GetAssetContext();
            switch (assetContext.AssetState) {
                case AssetState.None:
                case AssetState.Missing:
                    _path = null;
                    // Missing asset state is only for a brief moment, then the _object becomes null
                    // So don't set _message to null here even when asset state is None
                    // This way we keep the path to the original asset for display
                    break;
                case AssetState.Asset:
                    _path = null; // Not a valid Resource
                    _message = assetContext.AssetPath;
                    break;
                case AssetState.Resource:
                    _path = assetContext.ResourcePath;
                    _message = assetContext.AssetPath; // Still display the full asset path
                    break;
            }
        }

        public virtual void OnAfterDeserialize() { }
    }

#else
    [Serializable]
    public abstract class ResourceBase<TObject> : ResourceBase where TObject : UnityObject {
        [SerializeField] protected string _path;
    }
#endif
}