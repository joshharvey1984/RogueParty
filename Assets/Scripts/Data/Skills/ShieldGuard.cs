using RogueParty.Data.SkillBehaviours;
using static RogueParty.Data.AreaOfEffectTypes;

namespace RogueParty.Data.Skills {
    internal class ShieldGuard : Skill {
        public ShieldGuard() {
            Name = "Shield Guard";
            Description = "Alyx increases his Damage Reduction but lessens his move speed and can't attack.";
            ManaCost = 5;
            CastTime = 0;
            CooldownTime = 20;
            Targeting = new NoTargetTargeting();
            SkillBehaviours.Add(new ApplyStatusEffect(new StatusEffects.ShieldGuard(6), Self));
        }
    }
}