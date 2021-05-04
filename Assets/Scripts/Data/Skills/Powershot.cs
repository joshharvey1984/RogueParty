using System.Collections.Generic;
using RogueParty.Data.SkillBehaviours;

namespace RogueParty.Data.Skills {
    public class Powershot : Skill {
        public Powershot() {
            Name = "Powershot";
            Description = "Ilse takes aim and delivers a devastating power arrow, damaging all in its path.";
            ManaCost = 7;
            CastTime = 0;
            CooldownTime = 3;
            Targeting = new TargetPointTargeting();
            TargetingRange = 5;
            
            SkillBehaviours.Add(new SpecialProjectile(new List<SkillBehaviour> {
                new TakeDamage(AreaOfEffectTypes.SingleUnit, 0, 10)
            }));
        }
    }
}