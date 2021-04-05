using RogueParty.Core.Actors;
using RogueParty.Data.SkillBehaviours;

namespace RogueParty.Data.Skills {
    internal class Dash : Skill {
        public Dash() {
            Name = "Dash";
            Description = "Ilse dashes to a point in range.";
            ManaCost = 7;
            CastTime = 0;
            CooldownTime = 3;
            Targeting = new TargetPointTargeting();
            TargetingRange = 5;
            SkillBehaviours.Add(new DashToPosition(10));
            SkillBehaviours.Add(new SetActorEffect(ActorController.GetShaderPropertyID("_MotionBlurDist"), 1.1F));
        }
    }
}