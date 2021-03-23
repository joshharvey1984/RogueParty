using RogueParty.Data;
using UnityEngine;

namespace RogueParty.Core.UI {
    public class StatusIcon : MonoBehaviour {
        private SpriteRenderer _renderer;
        private StatusEffect _statusEffect;

        private void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void SetStatus(StatusEffect statusEffect) {
            _statusEffect = statusEffect;
            _renderer.sprite = Resources.Load<Sprite>($"Sprites/UI/StatusIcons/{_statusEffect.GetType().Name}");
            Destroy(transform.parent.gameObject, _statusEffect.Time);
        }
    }
}