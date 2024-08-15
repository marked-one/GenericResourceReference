using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace GenericResourceReference {
    [Serializable]
    public class ResourceReference<TObject>
        : ResourcePath<TObject>, IResourceReference<TObject> where TObject : UnityObject {
        public TObject Load() => Resources.Load(_path, typeof(TObject)) as TObject;
        public T Load<T>() where T : TObject => Resources.Load<T>(_path);
        public TObject Load(Type systemTypeInstance) => Resources.Load(_path, systemTypeInstance) as TObject;
        public ResourceRequest LoadAsync() => Resources.LoadAsync(_path, typeof(TObject));
        public ResourceRequest LoadAsync<T>() where T : TObject => Resources.LoadAsync<T>(_path);
        public ResourceRequest LoadAsync(Type systemTypeInstance) => Resources.LoadAsync(_path, systemTypeInstance);
    }
}