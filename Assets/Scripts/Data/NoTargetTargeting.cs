using RogueParty.Core.Actors;
using UnityEngine;

namespace RogueParty.Data {
    public class NoTargetTargeting : ITargeting {
        public Skill Skill { get; set; }
        public ActorController ActorController { get; set; }
        public GameObject TargetPosition { get; set; }
        public void Execute(Skill skill, ActorController actorController) {
            ActorController = actorController;
            skill.Execute(this);
        }
        public void Targeted(GameObject targetPosition) { }
    }
}