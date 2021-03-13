using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace RogueParty.Core {
    public class PartyController : MonoBehaviour {
        private Camera Camera { get; set; }
        public List<HeroController> HeroControllers { get; } = new List<HeroController>();
        private HeroController SelectedHeroController { get; set; }

        private void Awake() {
            Camera = Camera.main;
            var heroes = GameObject.FindGameObjectsWithTag("Player");
            foreach (var hero in heroes) {
                var newHero = hero.GetComponent<HeroController>();
                HeroControllers.Add(newHero);
                newHero.OnMouseClick += OnHeroClicked;
            }
        }

        public void Start() {
            
            
            SelectHero(HeroControllers[0]);
        }
        
        private void Update() {
            if (Input.GetMouseButtonUp(1)) RightClick(Camera.ScreenToWorldPoint(Input.mousePosition));
            if (Input.GetKeyUp(KeyCode.Tab)) TabNextHero();
        }

        private void SelectHero(HeroController heroController) {
            if (heroController == SelectedHeroController) return;
            if (SelectedHeroController) SelectedHeroController.DeselectHero();
            SelectedHeroController = heroController;
            SelectedHeroController.SelectHero();
        }

        private void TabNextHero() {
            var currentHeroIndex = HeroControllers.IndexOf(SelectedHeroController) + 1;
            if (currentHeroIndex >= HeroControllers.Count) currentHeroIndex = 0;
            SelectHero(HeroControllers[currentHeroIndex]);
        }

        private void OnHeroClicked(object sender, EventArgs e) {
            SelectHero((HeroController)sender);
        }

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