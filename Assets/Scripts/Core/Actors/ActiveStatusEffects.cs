using System.Collections.Generic;
using System.Linq;
using RogueParty.Data;

namespace RogueParty.Core.Actors {
    public class ActiveStatusEffects {
        private readonly List<ActiveStatusEffect> statusEffects = new List<ActiveStatusEffect>();

        public void Add(StatusEffect statusEffect) => statusEffects.Add(new ActiveStatusEffect(statusEffect));
        public void Remove(ActiveStatusEffect statusEffect) => statusEffects.Remove(statusEffect);

        public List<ActiveStatusEffect> CheckEffectTimes() {
            return statusEffects.Where(effect => !effect.CheckTime()).ToList();
        }
    }
}