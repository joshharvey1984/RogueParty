using System.Collections.Generic;
using UnityEngine;

namespace RogueParty.Data {
    public abstract class SkillBehaviour {
        protected float Range;
        public abstract void Execute(ITargeting targeting);
        public abstract void Execute(GameObject gameObject);
    }

    class FallingProjectile : SkillBehaviour {
        public List<SkillBehaviour> ProjectileBehaviors;
        public FallingProjectile(List<SkillBehaviour> projectileBehaviors) => ProjectileBehaviors = projectileBehaviors;
        public override void Execute(ITargeting targeting) {
            var projectile = Resources.Load<GameObject>($"Prefabs/Projectiles/{targeting.Skill.GetType().Name}");
            targeting.ActorController.FallingProjectile(targeting, projectile, ProjectileBehaviors);
        }

        public override void Execute(GameObject gameObject) {
            
        }
    }
}