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
        protected float CastTime;
        protected float CooldownTime;
        protected ITargeting Targeting;
        protected int TargetingRange;
        protected readonly List<SkillBehaviour> SkillBehaviours = new List<SkillBehaviour>();
        
        public bool CanUse { get; set; } = true;
        public readonly UnityEvent<float> OnSkillUse = new UnityEvent<float>();
        
        public bool Trigger(ActorController actorController) {
            if (!CanUse) return false;
            Targeting.Execute(this, actorController);
            return true;
        }

        public void Execute(ITargeting targeting) {
            OnSkillUse.Invoke(CooldownTime);
            foreach (var skillBehaviour in SkillBehaviours) skillBehaviour.Execute(targeting);
        }
    }

    public class Powershot : Skill {
        public Powershot() {
            Name = "Powershot";
            Description = "Ilse takes aim and delivers a devastating power arrow, damaging all in its path.";
            ManaCost = 7;
            CastTime = 0;
            CooldownTime = 3;
            Targeting = new TargetPointTargeting();
            TargetingRange = 5;
            
            SkillBehaviours.Add(new SpecialProjectile(new List<SkillBehaviour> {
                new TakeDamageOnContact(10)
            }));
        }
    }
}