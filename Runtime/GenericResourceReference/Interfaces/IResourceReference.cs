using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace GenericResourceReference {
#if UNITY_EDITOR
    public interface IEditorResourceReference { }
    public interface IResourceReference<TObject> : IEditorResourceReference  where TObject : UnityObject {
#else
    public interface IResourceReference<TObject> where TObject : UnityObject {
#endif
        public TObject Load();
        public T Load<T>() where T : TObject;
        public TObject Load(Type systemTypeInstance);
        public ResourceRequest LoadAsync();
        public ResourceRequest LoadAsync<T>() where T : TObject;
        public ResourceRequest LoadAsync(Type systemTypeInstance);
    }
}