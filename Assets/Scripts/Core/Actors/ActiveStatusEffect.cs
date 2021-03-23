using RogueParty.Data;
using UnityEngine;

namespace RogueParty.Core.Actors {
    public class ActiveStatusEffect {
        public readonly StatusEffect StatusEffect;
        public readonly float MaxTime;
        public float TimeLeft;

        public ActiveStatusEffect(StatusEffect statusEffect) {
            StatusEffect = statusEffect;
            MaxTime = statusEffect.Time;
            TimeLeft = MaxTime;
        }

        public bool CheckTime() {
            TimeLeft -= Time.deltaTime;
            return TimeLeft > 0;
        }
    }
}