using System;
using UnityObject = UnityEngine.Object;

namespace GenericResourceReference {
    [Serializable]
    public class ResourcePath<TObject> : ResourceBase<TObject>, IResourcePath where TObject : UnityObject {
        public string Path => _path;
    }

    [Serializable]
    public class ResourcePath : ResourcePath<UnityObject> { }
}