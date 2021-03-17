using RogueParty.Data;
using UnityEngine;
using UnityEngine.UI;

namespace RogueParty.Core {
    public class CharacterBar : MonoBehaviour {
        public Hero Hero { get; set; }
        [SerializeField] private GameObject portrait;
        [SerializeField] private GameObject healthBar;
        [SerializeField] private GameObject healthText;
        [SerializeField] private GameObject manaBar;
        [SerializeField] private GameObject manaText;
        [SerializeField] private GameObject skillBar;

        public void Start() {
            healthText.GetComponent<Text>().text = Hero.hitPoints.ToString();
            manaText.GetComponent<Text>().text = Hero.manaPoints.ToString();
        }
    }
}