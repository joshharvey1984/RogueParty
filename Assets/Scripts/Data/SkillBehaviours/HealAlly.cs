using UnityEngine;

namespace RogueParty.Data.SkillBehaviours {
    internal class HealAlly : SkillBehaviour {
        private readonly AreaOfEffectTypes areaOfEffectTypes;
        private readonly int HealAmount;
        public HealAlly(AreaOfEffectTypes areaOfEffectTypes, float range, int healAmount) {
            this.areaOfEffectTypes = areaOfEffectTypes;
            HealAmount = healAmount;
            Range = range;
        }
        public override void Execute(ITargeting targeting) {
            var targets = ActorsInAreaOfEffect.GetTargets(areaOfEffectTypes, targeting.ActorController.gameObject, Range);
            targets.ForEach(t => t.HealUnit(HealAmount));
        }

        public override void Execute(GameObject gameObject) {
            
        }
    }
}