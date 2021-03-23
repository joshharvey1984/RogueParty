using System.Collections.Generic;
using RogueParty.Core.UI;
using UnityEngine;

namespace RogueParty.Core {
    public class SkillBar : MonoBehaviour {
        public List<GameObject> skillButtons;

        public void SetSkills(HeroController heroController) {
            var i = 0;
            foreach (var heroSkill in heroController.ActorStatus.skills) {
                var skillButton = skillButtons[i].GetComponent<SkillButton>();
                skillButton.SetSkill(heroSkill);
                skillButton.OnMouseClick += heroController.SkillButtonClicked;
                i++;
            }
        }
    }
}