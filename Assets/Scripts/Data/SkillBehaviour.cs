using System.Collections.Generic;
using UnityEngine;

namespace RogueParty.Data {
    public abstract class SkillBehaviour {
        protected int Range;
        public abstract void Execute(ITargeting targeting);
    }

    public class SpecialProjectile : SkillBehaviour {
        public List<SkillBehaviour> ProjectileBehaviors;
        public SpecialProjectile(List<SkillBehaviour> projectileBehaviors) {
            ProjectileBehaviors = projectileBehaviors;
        }
        public override void Execute(ITargeting targeting) {
            var projectile = Resources.Load<GameObject>($"Prefabs/Projectiles/{targeting.Skill.GetType().Name}");
            targeting.ActorController.FireSpecialProjectile(targeting.TargetPosition, projectile, ProjectileBehaviors);
        }
    }

    public class TakeDamageOnContact : SkillBehaviour {
        public int Damage;
        public TakeDamageOnContact(int damage) {
            Damage = damage;
        }
        public override void Execute(ITargeting targeting) {
            
        }
    }
}