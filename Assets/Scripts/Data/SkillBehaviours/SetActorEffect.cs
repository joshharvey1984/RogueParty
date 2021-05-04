using UnityEngine;

namespace RogueParty.Data.SkillBehaviours {
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

        public override void Execute(GameObject gameObject) {
            throw new System.NotImplementedException();
        }
        
    }
}