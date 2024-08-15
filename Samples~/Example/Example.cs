using GenericResourceReference;
using UnityEngine;
using UnityEngine.UI;

public class Example : MonoBehaviour {
    [SerializeField] ResourcePath<TextAsset> _resourcePath;
    [SerializeField] ResourceReference<PrefabScript> _resourceReference;
    [SerializeField] ResourceFolder _resourceFolder;

    [Header("Example UI")]
    [SerializeField] Canvas _canvas;
    [Space(15)]
    [SerializeField] Button _loadFromPath;
    [SerializeField] Button _loadFromReference;
    [SerializeField] Button _loadFromFolder;
    [Space(15)]
    [SerializeField] Text _pathLabel;
    [SerializeField] Text _folderLabel;
    [SerializeField] RawImage _folderImage;

    void Start() {
        _loadFromPath.onClick.AddListener(() => {
            _loadFromPath.interactable = false;
            LoadFromPath();
        });

        _loadFromReference.onClick.AddListener(() => {
            _loadFromReference.interactable = false;
            LoadFromReference();
        });

        _loadFromFolder.onClick.AddListener(() => {
            _loadFromFolder.interactable = false;
            LoadFromFolder();
        });
    }

    void LoadFromPath() {
        var textAsset = Resources.Load<TextAsset>(_resourcePath.Path);
        _pathLabel.text = textAsset.text;
    }

    void LoadFromReference() {
        var prefab = _resourceReference.Load<PrefabScript>();
        Instantiate(prefab, _canvas.transform);
    }

    void LoadFromFolder() {
        var resources = _resourceFolder.LoadAll();
        foreach (var resource in resources) {
            if (resource is TextAsset asset)
                _folderLabel.text = asset.text;

            if (resource is Texture texture)
                _folderImage.texture = texture;
        }
    }

    void OnDestroy() {
        _loadFromPath.onClick.RemoveAllListeners();
        _loadFromReference.onClick.RemoveAllListeners();
        _loadFromFolder.onClick.RemoveAllListeners();
    }
}