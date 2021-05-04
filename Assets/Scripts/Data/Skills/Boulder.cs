using System.Collections.Generic;
using RogueParty.Data.SkillBehaviours;

namespace RogueParty.Data.Skills {
    public class Boulder : Skill {
        public Boulder() {
            Name = "Heal";
            Description = "Yelena causes stone spikes to appear from the ground damaging and stunning enemies in AOE";
            ManaCost = 10;
            CastTime = 0F;
            CooldownTime = 25;
            Range = 1.5F;
            Targeting = new AreaTargeting();
            
            SkillBehaviours.Add(new FallingProjectile(new List<SkillBehaviour> {
                new TakeDamage(AreaOfEffectTypes.EnemiesInRange, Range, 5),
                new ApplyStatusEffect(new Stun(2.5F), AreaOfEffectTypes.EnemiesInRange, Range)
            }));
        }
    }
}