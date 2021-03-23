using System.Collections.Generic;
using System.Linq;

namespace RogueParty.Core.Actors {
    public class ActorAbilityToggles {
        private readonly List<ActorAbilityToggle> abilityToggles = new List<ActorAbilityToggle>();

        public ActorAbilityToggles() {
            abilityToggles.Add(new ActorAbilityToggle(AbilityToggleName.CanAttack));
            abilityToggles.Add(new ActorAbilityToggle(AbilityToggleName.CanMove));
            abilityToggles.Add(new ActorAbilityToggle(AbilityToggleName.CanCast));
        }

        public bool? GetToggle(AbilityToggleName toggle) => abilityToggles.FirstOrDefault(a
            => a.AbilityToggleName == toggle)?.Value;

        public void AddModifiers(List<AbilityToggleModifier> modifiers) {
            modifiers.ForEach(at => abilityToggles.FirstOrDefault(a => 
                a.AbilityToggleName == at.AbilityToggleName)?.AddModifier(at));
        }
        
        public void RemoveModifiers(List<AbilityToggleModifier> modifiers) {
            modifiers.ForEach(at => abilityToggles.FirstOrDefault(a => 
                a.AbilityToggleName == at.AbilityToggleName)?.RemoveModifier(at));
        }
    }
}