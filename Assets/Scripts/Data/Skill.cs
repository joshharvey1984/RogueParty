using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RogueParty.Data {
    public abstract class Skill : MonoBehaviour {
        public Sprite icon;
        public string description;
        public int manaCost;
        public float castTime;
        public float cooldownTime;
        public bool canUse;
        
        public List<SkillBehaviour> skillBehaviours;

        public UnityEvent<float> onSkillUse = new UnityEvent<float>();

        public void Trigger() {
            if (!canUse) return;
            onSkillUse.Invoke(cooldownTime);
            Execute();
            StartCooldown();
        }

        protected abstract void Execute();

        private void StartCooldown() {
            StartCoroutine(Cooldown());
            IEnumerator Cooldown() {
                canUse = false;
                yield return new WaitForSeconds(cooldownTime);
                canUse = true;
            }
        }
    }
}