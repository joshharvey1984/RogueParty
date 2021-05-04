using System;
using System.Collections.Generic;
using RogueParty.Core.Actors;
using UnityEngine;

namespace RogueParty.Data.SkillBehaviours {
    internal class ApplyStatusEffect : SkillBehaviour {
        private readonly StatusEffect statusEffect;
        private readonly AreaOfEffectTypes areaOfEffectType;

        public ApplyStatusEffect(StatusEffect statusEffect, AreaOfEffectTypes areaOfEffectType, float range = 0) {
            this.statusEffect = statusEffect;
            this.areaOfEffectType = areaOfEffectType;
            Range = range;
        }
        public override void Execute(ITargeting targeting) {
            var targets = new List<ActorController>();
            if (targeting.TargetPosition) {
                targets = ActorsInAreaOfEffect.GetTargets(areaOfEffectType, targeting.TargetPosition, Range);
                targets.ForEach(t => Console.WriteLine(t));
            }
            else targets = ActorsInAreaOfEffect.GetTargets(areaOfEffectType, targeting.ActorController.gameObject);
            targets.ForEach(t => t.actorStatus.ApplyStatusEffect(statusEffect));
        }

        public override void Execute(GameObject gameObject) {
            throw new System.NotImplementedException();
        }
    }
}