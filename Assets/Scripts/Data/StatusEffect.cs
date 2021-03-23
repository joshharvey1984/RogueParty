using System.Collections.Generic;
using RogueParty.Core.Actors;
using UnityEngine;

namespace RogueParty.Data {
    public abstract class StatusEffect {
        public Sprite Icon;
        public float Time;
        public Dictionary<string, string> AnimationChanges;
        public List<ActorAttributeModifier> AttributeChanges;
        public List<ActorAttributeModifier> ActorAttributeModifiers;
        public List<AbilityToggleModifier> AbilityToggles;

    }
}