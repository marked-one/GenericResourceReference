using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace GenericResourceReference {
    [Serializable]
    public class ResourceFolder : ResourcePath<UnityObject>, IResourceFolder {
        public UnityObject[] LoadAll() =>
            _path == null ? Array.Empty<UnityObject>() : Resources.LoadAll(_path, typeof(UnityObject));

        public T[] LoadAll<T>() where T : UnityObject => _path == null ? Array.Empty<T>() : Resources.LoadAll<T>(_path);

        public UnityObject[] LoadAll(Type systemTypeInstance) =>
            _path == null ? Array.Empty<UnityObject>() : Resources.LoadAll(_path, systemTypeInstance);
    }
}