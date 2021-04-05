using System.Collections.Generic;
using System.Linq;
using RogueParty.Core.Actors;
using RogueParty.Data;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace RogueParty.Core.UI {
    public class SkillBar : MonoBehaviour {
        private HeroController _heroController;
        private List<SkillButton> skillButtons = new List<SkillButton>();

        private void Awake() {
            skillButtons = GetComponentsInChildren<SkillButton>().ToList();
        }

        public void SetSkills(HeroController heroController) {
            _heroController = heroController;
            foreach (var heroSkill in heroController.actorStatus.skills) SetSkill(heroSkill);
        }

        private void SetSkill(Skill skill) {
            var emptySkillButton = skillButtons.FirstOrDefault(sb => sb.Skill == null);
            Debug.Assert(emptySkillButton != null, nameof(emptySkillButton) + " != null");
            emptySkillButton.SetSkill(skill);
            emptySkillButton.OnMouseClick += _heroController.SkillButtonClicked;
        }
    }
}