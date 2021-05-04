using System.Collections.Generic;
using RogueParty.Core.Actors;
using UnityEngine;
using UnityEngine.Events;

namespace RogueParty.Data {
    public abstract class Skill {
        protected string Name;
        public Sprite Icon;
        protected string Description;
        protected int ManaCost;
        public float CastTime;
        public string CastAnimation;
        protected float CooldownTime;
        protected internal float Range;
        public ITargeting Targeting;
        protected int TargetingRange;
        protected readonly List<SkillBehaviour> SkillBehaviours = new List<SkillBehaviour>();
        
        public bool CanUse { get; set; } = true;
        public readonly UnityEvent<float> OnSkillUse = new UnityEvent<float>();
        
        public void Trigger(ActorController actorController) {
            if (!CanUse) return;
            Targeting.Execute(this, actorController);
        }

        public void Cast(ActorController actorController) {
            actorController.BeginCast(this);
        }

        public void Execute() {
            OnSkillUse.Invoke(CooldownTime);
            foreach (var skillBehaviour in SkillBehaviours) skillBehaviour.Execute(Targeting);
        }
    }
}