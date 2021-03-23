using RogueParty.Data;
using UnityEngine;

namespace RogueParty.Core.UI {
    public class StatusBar : MonoBehaviour {
        [SerializeField] private GameObject statusIcon;

        public void AddStatusIcon(StatusEffect statusEffect) {
            var icon = Instantiate(statusIcon, transform);
            icon.GetComponentInChildren<StatusIcon>().SetStatus(statusEffect);
        }
    }
}