using System;
using RogueParty.Data;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace RogueParty.Core.UI {
    public class SkillButton : MonoBehaviour {
        private SpriteRenderer _renderer;
        public Skill skill;

        private void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void SetSkill(Skill newSkill) {
            skill = newSkill;
            _renderer.sprite = skill.icon;
            skill.onSkillUse.AddListener(CoolDownStart);
        }

        public event EventHandler<SkillClickArgs> OnMouseClick;
        public class SkillClickArgs { public Skill SkillClicked { get; set; } }
        
        private void OnMouseUp() {
            OnMouseClick?.Invoke(this, new SkillClickArgs { SkillClicked = skill });
        }

        private void CoolDownStart(float coolDown) {
            
        }
    }
}