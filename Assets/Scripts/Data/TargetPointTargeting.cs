using RogueParty.Core.Actors;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace RogueParty.Data {
    public class TargetPointTargeting : ITargeting {
        public Skill Skill { get; set; }
        public ActorController ActorController { get; set; }
        public GameObject TargetPosition { get; set; }
        
        public void Execute(Skill skill, ActorController actorController) {
            Skill = skill;
            ActorController = actorController;
            var heroController = ActorController as HeroController;
            Debug.Assert(heroController != null, nameof(heroController) + " != null");
            heroController.TargetingMode(this);
        }

        public void Targeted(GameObject targetPosition) {
            TargetPosition = targetPosition;
            Skill.Execute(this);
        }
    }
}