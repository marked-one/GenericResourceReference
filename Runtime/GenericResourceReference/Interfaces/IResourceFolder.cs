using UnityObject = UnityEngine.Object;

namespace GenericResourceReference {
#if UNITY_EDITOR
    public interface IEditorResourceFolder { }
    public interface IResourceFolder : IEditorResourceFolder {
#else
    public interface IResourceFolder {
#endif
        public UnityObject[] LoadAll();
        public T[] LoadAll<T>() where T : UnityObject;
        public UnityObject[] LoadAll(System.Type systemTypeInstance);
    }
}