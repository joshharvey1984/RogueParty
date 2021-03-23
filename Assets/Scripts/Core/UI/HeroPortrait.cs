using System;
using UnityEngine;

namespace RogueParty.Core.UI {
    public class HeroPortrait : MonoBehaviour {
        private static readonly int Outline = Shader.PropertyToID("_OutlineAlpha");
        private static readonly int Hue = Shader.PropertyToID("_HsvShift");
        private Renderer _renderer;
        private SpriteRenderer _portraitSprite;
        public HeroController heroController;
        
        public event EventHandler<PortraitClickArgs> OnMouseClick;
        public class PortraitClickArgs { public HeroController heroController { get; set; } }

        private void Awake() {
            _renderer = GetComponent<Renderer>();
            _portraitSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        private void Start() {
            _portraitSprite.sprite = heroController.actor.portrait;
        }

        private void OnMouseEnter() => _renderer.material.SetFloat(Outline, 1F);
        private void OnMouseExit() => _renderer.material.SetFloat(Outline, 0F);
        private void OnMouseUp() {
            OnMouseClick?.Invoke(this, new PortraitClickArgs {heroController = heroController});
        }

        public void Select(bool selected) {
            _renderer.material.SetFloat(Hue, selected ? 300F : 0F);
        }
    }
}