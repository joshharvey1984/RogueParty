using System.Collections.Generic;
using System.Linq;
using RogueParty.Data;

namespace RogueParty.Core.Actors {
    public class ActorAttributes {
        private readonly List<ActorAttribute> actorAttributes = new List<ActorAttribute>();

        public ActorAttributes(Actor actor) {
            actorAttributes.Add(new ActorAttribute(AttributeName.HitPoints, actor.hitPoints));
            actorAttributes.Add(new ActorAttribute(AttributeName.ManaPoints, actor.manaPoints));
            actorAttributes.Add(new ActorAttribute(AttributeName.Damage, actor.damage));
            actorAttributes.Add(new ActorAttribute(AttributeName.DamageReduction, actor.damageReduction));
            actorAttributes.Add(new ActorAttribute(AttributeName.MoveSpeed, actor.moveSpeed));
            actorAttributes.Add(new ActorAttribute(AttributeName.AttackSpeed, actor.attackSpeed));
            actorAttributes.Add(new ActorAttribute(AttributeName.AttackRange, actor.attackRange));
        }

        public int? Get(AttributeName attributeName) => actorAttributes.FirstOrDefault(a => 
            a.AttributeName == attributeName)?.Value;

        public void AddModifiers(List<ActorAttributeModifier> modifiers) {
            modifiers.ForEach(am => {actorAttributes.FirstOrDefault(aa 
                => aa.AttributeName == am.AttributeName)?.AddModifier(am);});
        }

        public void RemoveModifiers(List<ActorAttributeModifier> modifiers) {
            modifiers.ForEach(am => {actorAttributes.FirstOrDefault(aa 
                => aa.AttributeName == am.AttributeName)?.RemoveModifier(am);});
        }
    }
}