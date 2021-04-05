using RogueParty.Core.Actors;
using UnityEngine;
using UnityEngine.UI;

namespace RogueParty.Core.UI {
    public class CharacterBar : MonoBehaviour {
        private HeroController HeroController { get; set; }
        [SerializeField] private GameObject portrait;
        [SerializeField] private GameObject healthBar;
        [SerializeField] private GameObject healthText;
        [SerializeField] private GameObject manaBar;
        [SerializeField] private GameObject manaText;
        [SerializeField] private GameObject skillBar;
        [SerializeField] private GameObject statusBar;

        public void SetHero(HeroController heroController) {
            HeroController = heroController;
            portrait.GetComponent<HeroPortrait>().heroController = HeroController;
            skillBar.GetComponent<SkillBar>().SetSkills(HeroController);
            HeroController.actorStatus.onStatusEffect.AddListener(statusBar.GetComponent<StatusBar>().AddStatusIcon);
            HeroController.actorStatus.onPointsChange.AddListener(UpdatePointBars);
        }

        public void Start() {
            UpdatePointBars();
        }

        public void UpdatePointBars() {
            healthText.GetComponent<Text>().text = HeroController.actorStatus.currentHitPoints.ToString();
            manaText.GetComponent<Text>().text = HeroController.actorStatus.currentManaPoints.ToString();
        }
    }
}