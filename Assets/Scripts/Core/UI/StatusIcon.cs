using RogueParty.Data;
using UnityEngine;

namespace RogueParty.Core.UI {
    public class StatusIcon : MonoBehaviour {
        private SpriteRenderer _renderer;
        private StatusEffect _statusEffect;
        private GameObject timeBar;
        private float timeLeft;

        private void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
            timeBar = transform.GetChild(0).gameObject;
        }

        private void Update() {
            timeLeft -= Time.deltaTime;
            var scale = timeBar.transform.localScale;
            scale.x = (timeLeft / _statusEffect.Time) * 0.28F;
            timeBar.transform.localScale = scale;
        }

        public void SetStatus(StatusEffect statusEffect) {
            _statusEffect = statusEffect;
            timeLeft = _statusEffect.Time;
            _renderer.sprite = Resources.Load<Sprite>($"Sprites/UI/StatusIcons/{_statusEffect.GetType().Name}");
            Destroy(transform.parent.gameObject, _statusEffect.Time);
        }
    }
}