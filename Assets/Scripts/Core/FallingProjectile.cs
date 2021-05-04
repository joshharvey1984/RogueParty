
using System.Collections.Generic;
using RogueParty.Data;
using UnityEngine;

namespace RogueParty.Core {
    public abstract class SpellAnimation : MonoBehaviour {
        internal List<SkillBehaviour> skillBehaviours;

        public abstract void Begin(ITargeting targetPosition);
    }
    
    public class FallingProjectile : SpellAnimation {
        private static readonly int FadeAmount = Shader.PropertyToID("_FadeAmount");
        private bool falling;
        private ITargeting targeting;
        private Renderer _renderer;

        private void Awake() {
            _renderer = GetComponent<Renderer>();
        }

        private void Update() {
            if (!falling) {
                Fade();
                return;
            }
            Fall();
        }

        private void Fall() {
            if (transform.position.y > targeting.TargetPosition.transform.position.y) {
                var pos = transform.position;
                pos.y -= 30.1F * Time.deltaTime;
                transform.position = pos;
            }
            else {
                End();
            }
        }

        private void Fade() {
            var fadeAmount = _renderer.material.GetFloat(FadeAmount) + 0.01F;
            if (fadeAmount > 1.0F) Destroy(gameObject);
            _renderer.material.SetFloat(FadeAmount, fadeAmount);
        }

        private void End() {
            falling = false;
            foreach (var skillBehaviour in skillBehaviours) { skillBehaviour.Execute(targeting); }
        }

        public override void Begin(ITargeting targetPos) {
            targeting = targetPos;
            falling = true;
            AudioPlayer.Instance.PlayOneShot("boulder_drop");
        }
    }
}