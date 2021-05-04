using RogueParty.Data;
using UnityEngine;

namespace RogueParty.Core.UI {
    public class StatusIndicator : MonoBehaviour {
        private GameObject stun;

        private void Awake() {
            stun = transform.Find("Stun").gameObject;
        }

        public void SetIndicator(StatusEffect statusEffect, bool onOff) {
            var statusName = statusEffect.GetType().Name;
            var method = GetType().GetMethod(statusName);
            if (method != null) method.Invoke(this, new object[] { onOff });
        }

        public void Stun(bool onOff) => stun.SetActive(onOff);
    }
}