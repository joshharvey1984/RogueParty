namespace RogueParty.Core.Actors {
    public class ActorAttributeModifier {
        public readonly AttributeName AttributeName;
        public readonly int Value;

        public ActorAttributeModifier(AttributeName attributeName, int value) {
            AttributeName = attributeName;
            Value = value;
        }
    }
}