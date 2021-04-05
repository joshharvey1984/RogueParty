namespace RogueParty.Data {
    public class SetActorEffect : SkillBehaviour {
        private readonly int effectInt;
        private readonly float amount;
        public SetActorEffect(int effectInt, float amount) {
            this.effectInt = effectInt;
            this.amount = amount;
        }

        public override void Execute(ITargeting targeting) {
            targeting.ActorController.SetDeterioratingEffect(effectInt, amount);
        }
    }
}