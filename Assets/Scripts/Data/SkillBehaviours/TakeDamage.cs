using RogueParty.Core.Actors;
using UnityEngine;

namespace RogueParty.Data.SkillBehaviours {
    public class TakeDamage : SkillBehaviour {
        private readonly AreaOfEffectTypes areaOfEffectTypes;
        private readonly int damage;
        public TakeDamage(AreaOfEffectTypes areaOfEffectTypes, float range, int damage) {
            this.areaOfEffectTypes = areaOfEffectTypes;
            this.damage = damage;
            Range = range;
        }
        public override void Execute(ITargeting targeting) {
            var targets = ActorsInAreaOfEffect.GetTargets(areaOfEffectTypes, targeting.TargetPosition, Range);
            targets.ForEach(t => t.TakeDamage(damage));
        }

        public override void Execute(GameObject gameObject) {
            gameObject.GetComponent<ActorController>().TakeDamage(damage);
        }
    }
}