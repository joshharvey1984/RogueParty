using System.Collections.Generic;
using RogueParty.Core.Actors;

namespace RogueParty.Data {
    public class Stun : StatusEffect {
        public Stun(float time) {
            Time = time;
            AnimationChanges = new Dictionary<string, string> {{"Walk", "GuardWalk"}, {"Idle", "GuardIdle"}};
            AbilityToggles = new List<AbilityToggleModifier> {
                new AbilityToggleModifier(AbilityToggleName.CanAttack),
                new AbilityToggleModifier(AbilityToggleName.CanCast),
                new AbilityToggleModifier(AbilityToggleName.CanMove)
            };
        }
    }
}