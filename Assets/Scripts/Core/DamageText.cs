using UnityEngine;
using UnityEngine.UI;

namespace RogueParty.Core {
    public class DamageText : MonoBehaviour {
        private Transform _transform;
        private Text _text;
        private void Start() {
            _transform = transform;
            _text = GetComponent<Text>();
            Destroy(gameObject, 1.2F);
        }

        private void Update() {
            SlideUp();
            ReduceAlpha();
        }

        private void ReduceAlpha() {
            var alpha = _text.color.a - 0.005F;
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, alpha);
        }

        private void SlideUp() {
            var position = transform.position;
            var newPos = _transform.position.y + 0.1F;
            _transform.position = new Vector3(position.x, newPos);
        }
    }
}