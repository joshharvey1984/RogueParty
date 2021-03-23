using System;
using System.Collections.Generic;
using System.Linq;
using RogueParty.Core.UI;
using UnityEngine;

namespace RogueParty.Core.Actors {
    public class PartyController : MonoBehaviour {
        private Camera Camera { get; set; }
        [SerializeField] private List<HeroController> heroControllers = new List<HeroController>();
        private HeroController SelectedHeroController { get; set; }

        private void Awake() {
            Camera = Camera.main;
        }

        public void OnEnable() {
            foreach (var heroController in heroControllers) { heroController.OnMouseClick += OnHeroClicked; }
            var characterBarHolder = FindObjectOfType<CharacterBarHolder>();
            characterBarHolder.SetHeroes(heroControllers);
        }

        public void Start() {
            SelectHero(heroControllers[0]);
        }
        
        private void Update() {
            if (Input.GetMouseButtonUp(1)) RightClick(Camera.ScreenToWorldPoint(Input.mousePosition));
            if (Input.GetKeyUp(KeyCode.Tab)) TabNextHero();
            if (Input.GetKeyUp(KeyCode.Alpha1)) SelectHero(heroControllers[0]);
            if (Input.GetKeyUp(KeyCode.Alpha2)) SelectHero(heroControllers[1]);
            if (Input.GetKeyUp(KeyCode.Alpha3)) SelectHero(heroControllers[2]);
            if (Input.GetKeyUp(KeyCode.Q)) SelectedHeroController.ExecuteSkill(SelectedHeroController.ActorStatus.skills[0]);
        }

        private void SelectHero(HeroController heroController) {
            if (heroController == SelectedHeroController) return;
            if (SelectedHeroController) SelectedHeroController.DeselectHero();
            SelectedHeroController = heroController;
            SelectedHeroController.SelectHero();
            foreach (var portrait in FindObjectsOfType<HeroPortrait>()) {
                portrait.Select(portrait.heroController == SelectedHeroController);
            }
        }

        private void TabNextHero() {
            var currentHeroIndex = heroControllers.IndexOf(SelectedHeroController) + 1;
            if (currentHeroIndex >= heroControllers.Count) currentHeroIndex = 0;
            SelectHero(heroControllers[currentHeroIndex]);
        }

        private void OnHeroClicked(object sender, EventArgs e) => SelectHero((HeroController)sender);
        private void OnPortraitClicked(object sender, HeroPortrait.PortraitClickArgs e) => SelectHero(e.heroController);
        
        private void RightClick(Vector3 mousePosition) {
            var interactable = GetInteractable();
            if (!interactable) SelectedHeroController.SetMovePosition(mousePosition);
            else SelectedHeroController.EngageEnemy(interactable);
        }

        private GameObject GetInteractable() {
            var results = Physics2D.RaycastAll(Camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            return (from hit in results where hit.collider.gameObject.layer == LayerMask.NameToLayer("Clickable") 
                select hit.collider.gameObject).FirstOrDefault();
        }
    }
}