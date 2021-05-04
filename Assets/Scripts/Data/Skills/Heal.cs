using RogueParty.Data.SkillBehaviours;

namespace RogueParty.Data.Skills {
    public class Heal : Skill {
        public Heal() {
            Name = "Heal";
            Description = "Ilse heals all friendly units in a radius around her.";
            ManaCost = 7;
            CastTime = 0;
            CooldownTime = 25;
            Targeting = new NoTargetTargeting();
            SkillBehaviours.Add(new HealAlly(AreaOfEffectTypes.AlliesInRange, 10, 20));
        }
    }
}