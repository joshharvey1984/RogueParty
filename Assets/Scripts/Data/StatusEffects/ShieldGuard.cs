using System.Collections.Generic;
using RogueParty.Core.Actors;
using static RogueParty.Core.Actors.AttributeName;

namespace RogueParty.Data.StatusEffects {
    internal class ShieldGuard : StatusEffect {
        public ShieldGuard(float time) {
            Time = time;
            AnimationChanges = new Dictionary<string, string> {{"Walk", "GuardWalk"}, {"Idle", "GuardIdle"}};
            AttributeChanges = new List<ActorAttributeModifier> {
                new ActorAttributeModifier(DamageReduction, 5),
                new ActorAttributeModifier(MoveSpeed, -10)
            };
            AbilityToggles = new List<AbilityToggleModifier> {new AbilityToggleModifier(AbilityToggleName.CanAttack)};
        }
    }
}