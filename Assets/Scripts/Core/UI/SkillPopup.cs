using UnityEngine;

namespace RogueParty.Core.UI {
    public class SkillPopup : MonoBehaviour {
        private Transform _transform;
        private SpriteRenderer spriteRenderer;
        private static readonly int ShineEffect = Shader.PropertyToID("_ShineLocation");
        private float shineLocation;
        private float maxPosY;

        private void Awake() {
            _transform = GetComponent<Transform>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            Destroy(gameObject, 1.5F);
            shineLocation = 0F;
            maxPosY = _transform.position.y + 0.75F;
        }

        public void SetSprite(Sprite sprite) => spriteRenderer.sprite = sprite;

        private void Update() {
            Animate();
        }

        private void Animate() {
            if (_transform.position.y < maxPosY) {
                var pos = _transform.position;
                pos.y += 0.007F;
                _transform.position = pos;
            }
            else {
                spriteRenderer.material.SetFloat(ShineEffect, shineLocation);
                shineLocation += 0.015F;
            }

            var col = spriteRenderer.color;
            col.a += 0.006F;
            spriteRenderer.color = col;
        }
    }
}