using UnityEngine;
using UnityEngine.UI;

public class PrefabScript : MonoBehaviour {
    [SerializeField] Text _label;

    void Start() {
        _label.text = "Hello from PrefabScript!";
    }
}