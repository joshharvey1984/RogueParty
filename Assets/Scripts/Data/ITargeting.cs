using RogueParty.Core.Actors;
using UnityEngine;

namespace RogueParty.Data {
    public interface ITargeting {
        public Skill Skill { get; set; }
        public ActorController ActorController { get; set; }
        public GameObject TargetPosition { get; set; }
        public void Execute(Skill skill, ActorController actorController);
        public void Targeted(GameObject targetPosition);
    }
}