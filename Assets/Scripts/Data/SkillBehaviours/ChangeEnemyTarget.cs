namespace RogueParty.Data.SkillBehaviours {
    internal class ChangeEnemyTarget : SkillBehaviour {
        private readonly AreaOfEffectTypes areaOfEffectTypes;
        public ChangeEnemyTarget(AreaOfEffectTypes areaOfEffectTypes, int range = 0) {
            this.areaOfEffectTypes = areaOfEffectTypes;
            Range = range;
        }
        public override void Execute(ITargeting targeting) {
            var targets = ActorsInAreaOfEffect.GetTargets(areaOfEffectTypes, targeting.ActorController, 10);
            targets.ForEach(t => t.EnemyTarget = targeting.ActorController.gameObject);
        }
    }
}