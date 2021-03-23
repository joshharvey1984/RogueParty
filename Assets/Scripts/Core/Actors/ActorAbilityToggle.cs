using System.Collections.Generic;
using System.Linq;

namespace RogueParty.Core.Actors {
    public class ActorAbilityToggle {
        public readonly AbilityToggleName AbilityToggleName;
        public bool Value => CalculateFinalValue();
        private readonly List<AbilityToggleModifier> toggleModifiers = new List<AbilityToggleModifier>();

        public ActorAbilityToggle(AbilityToggleName abilityToggleName) {
            AbilityToggleName = abilityToggleName;
        }
        
        public void AddModifier(AbilityToggleModifier modifier) => toggleModifiers.Add(modifier);
        public void RemoveModifier(AbilityToggleModifier modifier) => toggleModifiers.Remove(modifier);
        private bool CalculateFinalValue() => !toggleModifiers.Any();
    }
}