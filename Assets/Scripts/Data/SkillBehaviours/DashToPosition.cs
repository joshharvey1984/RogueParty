using UnityEngine;

namespace RogueParty.Data.SkillBehaviours {
    public class DashToPosition : SkillBehaviour {
        private readonly int dashSpeed;
        public DashToPosition(int dashSpeed) {
            this.dashSpeed = dashSpeed;
        }
        public override void Execute(ITargeting targeting) {
            targeting.ActorController.DirectMoveToObject(targeting.TargetPosition, dashSpeed);
        }

        public override void Execute(GameObject gameObject) {
            throw new System.NotImplementedException();
        }
        
    }
}