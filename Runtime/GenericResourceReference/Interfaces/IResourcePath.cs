namespace GenericResourceReference {
#if UNITY_EDITOR
    public interface IEditorResourcePath { }
    public interface IResourcePath : IEditorResourcePath {
#else
    public interface IResourcePath {
#endif
        public string Path { get; }
    }
}