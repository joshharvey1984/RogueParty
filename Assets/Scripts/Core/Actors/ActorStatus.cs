using System.Collections.Generic;
using RogueParty.Data;
using UnityEngine;
using UnityEngine.Events;

namespace RogueParty.Core.Actors {
    public class ActorStatus : MonoBehaviour {
        public List<Skill> skills = new List<Skill>();
        
        public ActorAttributes ActorAttributes;
        public int? currentHitPoints;
        public int? currentManaPoints;
        public ActorAbilityToggles AbilityToggles;
        
        private readonly ActiveStatusEffects activeStatusEffects = new ActiveStatusEffects();
        
        public UnityEvent<StatusEffect> onStatusEffect = new UnityEvent<StatusEffect>();
        public UnityEvent onPointsChange = new UnityEvent();

        public void SetActor(Actor actor) {
            ActorAttributes = new ActorAttributes(actor);
            currentHitPoints = ActorAttributes.Get(AttributeName.HitPoints);
            currentManaPoints = ActorAttributes.Get(AttributeName.ManaPoints);
            AbilityToggles = new ActorAbilityToggles();
            skills.AddRange(actor.skills);
            foreach (var skill in skills) {
                skill.Icon = Resources.Load<Sprite>($"Sprites/UI/SkillIcons/{skill.GetType().Name}");
            }
        }
        
        public void Update() {
            UpdateStatusEffects();
        }

        public void TakeDamage(int? damage) {
            currentHitPoints -= damage;
            if (currentHitPoints < 0) currentHitPoints = 0;
            onPointsChange.Invoke();
            if (currentHitPoints == 0) Death();
        }

        private void Death() {
            
        }

        private void UpdateStatusEffects() {
            activeStatusEffects.CheckEffectTimes().ForEach(RemoveStatusEffect);
        }
        
        public void ApplyStatusEffect(StatusEffect statusEffect) {
            activeStatusEffects.Add(statusEffect);
            ActorAttributes.AddModifiers(statusEffect.AttributeChanges);
            AbilityToggles.AddModifiers(statusEffect.AbilityToggles);
            onStatusEffect.Invoke(statusEffect);
        }

        private void RemoveStatusEffect(ActiveStatusEffect statusEffect) {
            activeStatusEffects.Remove(statusEffect);
            ActorAttributes.RemoveModifiers(statusEffect.StatusEffect.AttributeChanges);
            AbilityToggles.RemoveModifiers(statusEffect.StatusEffect.AbilityToggles);
        }
    }
}