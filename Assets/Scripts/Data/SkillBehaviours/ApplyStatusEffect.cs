namespace RogueParty.Data.SkillBehaviours {
    internal class ApplyStatusEffect : SkillBehaviour {
        private readonly StatusEffect statusEffect;
        private readonly AreaOfEffectTypes areaOfEffectTypes;

        public ApplyStatusEffect(StatusEffect statusEffect, AreaOfEffectTypes areaOfEffectTypes, int range = 0) {
            this.statusEffect = statusEffect;
            this.areaOfEffectTypes = areaOfEffectTypes;
            Range = range;
        }
        public override void Execute(ITargeting targeting) {
            var targets = ActorsInAreaOfEffect.GetTargets(areaOfEffectTypes, targeting.ActorController);
            targets.ForEach(t => t.actorStatus.ApplyStatusEffect(statusEffect));
        }
    }
}