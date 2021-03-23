using System.Collections.Generic;
using System.Linq;

namespace RogueParty.Core.Actors {
    public class ActorAttribute {
        public readonly AttributeName AttributeName;
        private readonly int baseValue;
        public int Value => CalculateFinalValue();
        private readonly List<ActorAttributeModifier> attributeModifiers = new List<ActorAttributeModifier>();

        public ActorAttribute(AttributeName name, int baseValue) {
            AttributeName = name;
            this.baseValue = baseValue;
        }

        public void AddModifier(ActorAttributeModifier modifier) => attributeModifiers.Add(modifier);
        public void RemoveModifier(ActorAttributeModifier modifier) => attributeModifiers.Remove(modifier);
        private int CalculateFinalValue() => baseValue + attributeModifiers.Sum(a => a.Value);
    }
}