using System;
using System.Collections.Generic;
using System.Linq;
using RogueParty.Core.UI;
using RogueParty.Data;
using UnityEngine;
using UnityEngine.AI;

namespace RogueParty.Core.Actors {
    public class PartyController : MonoBehaviour {
        private Camera Camera { get; set; }
        [SerializeField] private List<HeroController> heroControllers = new List<HeroController>();
        private HeroController SelectedHeroController { get; set; }
        private ITargeting targetingMode;

        private void Awake() {
            Camera = Camera.main;
        }

        public void OnEnable() {
            foreach (var heroController in heroControllers) {
                heroController.OnMouseClick += OnHeroClicked;
                heroController.OnTargetingMode += (sender, args) => { targetingMode = args.Targeting; };
            }
            var characterBarHolder = FindObjectOfType<CharacterBarHolder>();
            characterBarHolder.SetHeroes(heroControllers);
        }

        public void Start() {
            SelectHero(heroControllers[0]);
        }
        
        private void Update() {
            if (Input.GetMouseButtonUp(0)) LeftClick(Camera.ScreenToWorldPoint(Input.mousePosition));
            if (Input.GetMouseButtonUp(1)) RightClick(Camera.ScreenToWorldPoint(Input.mousePosition));
            if (Input.GetKeyUp(KeyCode.Tab)) TabNextHero();
            if (Input.GetKeyUp(KeyCode.Alpha1)) SelectHero(heroControllers[0]);
            if (Input.GetKeyUp(KeyCode.Alpha2)) SelectHero(heroControllers[1]);
            if (Input.GetKeyUp(KeyCode.Alpha3)) SelectHero(heroControllers[2]);
            if (Input.GetKeyUp(KeyCode.Q)) heroControllers[0].ExecuteSkill(heroControllers[0].actorStatus.skills[0]);
            if (Input.GetKeyUp(KeyCode.W)) heroControllers[0].ExecuteSkill(heroControllers[0].actorStatus.skills[1]);
            if (Input.GetKeyUp(KeyCode.A)) heroControllers[1].ExecuteSkill(heroControllers[1].actorStatus.skills[0]);
            if (Input.GetKeyUp(KeyCode.S)) heroControllers[1].ExecuteSkill(heroControllers[1].actorStatus.skills[1]);
            if (Input.GetKeyUp(KeyCode.D)) heroControllers[1].ExecuteSkill(heroControllers[1].actorStatus.skills[2]);
            if (Input.GetKeyUp(KeyCode.Z)) heroControllers[2].ExecuteSkill(heroControllers[2].actorStatus.skills[0]);
            if (Input.GetKeyUp(KeyCode.X)) heroControllers[2].ExecuteSkill(heroControllers[2].actorStatus.skills[1]);
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
            var interactable = GetInteractable(mousePosition, "Clickable");
            if (!interactable) CheckValidMove(mousePosition);
            else RightClickEnemy(interactable);
        }

        private void CheckValidMove(Vector3 mousePosition) {
            if(GetInteractable(mousePosition, "Nav"))
                SelectedHeroController.SetMovePosition(mousePosition);
        }

        private void RightClickEnemy(GameObject enemy) {
            if (enemy.GetComponent<ActorController>().targetable)
                SelectedHeroController.EngageEnemy(enemy);
        }

        private void LeftClick(Vector3 mousePosition) {
            if (targetingMode == null) return;
            var heroController = targetingMode.ActorController as HeroController;
            heroController.TargetingModeOff();
            targetingMode.Targeted(Instantiate(new GameObject(), mousePosition, Quaternion.identity));
            targetingMode = null;
        }

        private GameObject GetInteractable(Vector3 mousePosition, string layer) {
            var results = Physics2D.RaycastAll(mousePosition, Vector2.zero);
            return (from hit in results where hit.collider.gameObject.layer == LayerMask.NameToLayer(layer) 
                select hit.collider.gameObject).FirstOrDefault();
        }
    }
}