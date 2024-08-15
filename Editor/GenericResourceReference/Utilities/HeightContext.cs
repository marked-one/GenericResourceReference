namespace GenericResourceReference {
    public class HeightContext {
        readonly AssetState _assetState;
        public bool ShouldDisplayMessage => _assetState != AssetState.None;
        public HeightContext(AssetState assetState) => _assetState = assetState;
    }
}