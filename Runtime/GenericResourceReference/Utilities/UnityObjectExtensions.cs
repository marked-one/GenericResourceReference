#if UNITY_EDITOR

using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace GenericResourceReference {
    public static class UnityObjectExtensions {
        const string _resourcesFolderAtEnd = "/Resources";
        const string _resourcesFolder = _resourcesFolderAtEnd + "/";

        public static AssetContext GetAssetContext(this UnityObject unityObject) {
            if (unityObject == null)
                return new(AssetState.None);

            var assetPath = AssetDatabase.GetAssetPath(unityObject);
            if (string.IsNullOrEmpty(assetPath))
                return new(AssetState.Missing);

            var isFolder = AssetDatabase.IsValidFolder(assetPath);
            if (isFolder && assetPath.IsResourcesFolder())
                return new(AssetState.Resource, assetPath, string.Empty);

            var assetState = assetPath.IsResourcePath()
                ? AssetState.Resource
                : AssetState.Asset;

            if (assetState == AssetState.Asset)
                return new(assetState, assetPath);

            var resourcePath = GetResourcePathFromAssetPath(assetPath, isFolder);
            return new(assetState, assetPath, resourcePath);
        }

        public static bool IsResourcesFolder(this string path) =>
            path.EndsWith(_resourcesFolderAtEnd);

        public static bool IsResourcePath(this string path) => path.Contains(_resourcesFolder);

        static string GetResourcePathFromAssetPath(string assetPath, bool isFolder) {
            if (isFolder)
                return assetPath.Split(_resourcesFolder)[^1];

            var resourcePathWithExtension = assetPath.Split(_resourcesFolder)[^1];
            var splitPath = resourcePathWithExtension.Split('.');
            return splitPath.Length <= 1
                ? resourcePathWithExtension
                : string.Join('.', splitPath.SkipLast(1));
        }

        // TODO: Unload texture for Sprite? Unload GameObjects like prefabs?
        public static void UnloadAsset(this UnityObject unityObject) => Resources.UnloadAsset(unityObject);
    }
}

#endif