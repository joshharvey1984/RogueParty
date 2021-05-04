using System.Collections.Generic;
using UnityEngine;

namespace RogueParty.Data.SkillBehaviours {
    public class SpecialProjectile : SkillBehaviour {
        public List<SkillBehaviour> ProjectileBehaviors;
        public SpecialProjectile(List<SkillBehaviour> projectileBehaviors) {
            ProjectileBehaviors = projectileBehaviors;
        }
        public override void Execute(ITargeting targeting) {
            var projectile = Resources.Load<GameObject>($"Prefabs/Projectiles/{targeting.Skill.GetType().Name}");
            targeting.ActorController.FireSpecialProjectile(targeting.TargetPosition, projectile, ProjectileBehaviors);
        }

        public override void Execute(GameObject gameObject) {
        }
    }
}