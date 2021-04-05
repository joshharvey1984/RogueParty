using UnityEngine;
using UnityEngine.UI;

namespace RogueParty.Core.UI {
    public class CreateText : MonoBehaviour {
        private Camera _camera;
        [SerializeField] private GameObject uiText;

        private void Awake() {
            _camera = Camera.main;
        }

        public Text CreateUIText(Vector2 position) {
            return Instantiate(uiText, _camera.WorldToScreenPoint(position), 
                Quaternion.identity, gameObject.transform).GetComponent<Text>();
        }
        
    }
}