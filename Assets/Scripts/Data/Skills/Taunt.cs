using RogueParty.Data.SkillBehaviours;

namespace RogueParty.Data.Skills {
    internal class Taunt : Skill {
        public Taunt() {
            Name = "Taunt";
            Description = "Alyx taunts all enemies within a range around him to attack him.";
            ManaCost = 7;
            CastTime = 0;
            CooldownTime = 25;
            Targeting = new NoTargetTargeting();
            SkillBehaviours.Add(new ChangeEnemyTarget(AreaOfEffectTypes.EnemiesInRange, 10));
        }
    }
}