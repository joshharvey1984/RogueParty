using RogueParty.Core;
using RogueParty.Core.Actors;
using UnityEngine;

namespace RogueParty.Data {
    public abstract class SkillBehaviour { }

    class ApplyStatusEffectSelf : SkillBehaviour {
        public ApplyStatusEffectSelf(GameObject hero, StatusEffect statusEffect) {
            hero.GetComponent<ActorStatus>().ApplyStatusEffect(statusEffect);
        }
    }
}