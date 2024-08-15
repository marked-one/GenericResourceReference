#if UNITY_EDITOR

namespace GenericResourceReference {
    public struct AssetContext {
        public AssetState AssetState { get; }
        public string AssetPath { get; }
        public string ResourcePath { get; }

        public AssetContext(
            AssetState assetState,
            string assetPath = null,
            string resourcePath = null) {
            AssetState = assetState;
            AssetPath = assetPath;
            ResourcePath = resourcePath;
        }
    }
}

#endif