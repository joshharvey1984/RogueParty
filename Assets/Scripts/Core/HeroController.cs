using System;
using RogueParty.Data;
using UnityEngine;

namespace RogueParty.Core {
    public class HeroController : ActorController {
        [SerializeField] public Hero hero;
        public event EventHandler OnMouseClick;

        private new void Awake() => base.Awake();

        private void OnMouseDown() => OnMouseClick?.Invoke(this, EventArgs.Empty);
        
        public void SetMovePosition(Vector2 position) {
            if (EnemyTarget) HighlightEnemyTarget(false);
            EnemyTarget = null;
            BeginMove(position);
        }

        public void EngageEnemy(GameObject enemy) {
            EnemyTarget = enemy;
            HighlightEnemyTarget(true);
        }

        public void SelectHero() {
            SelectionCircle.AddGlow();
            if (EnemyTarget) HighlightEnemyTarget(true);
        }

        public void DeselectHero() {
            SelectionCircle.RemoveGlow();
            if (EnemyTarget) HighlightEnemyTarget(false);
        }

        private void HighlightEnemyTarget(bool highlight) {
            if (highlight) EnemyTarget.GetComponent<ActorController>().SelectionCircle.AddGlow();
            else EnemyTarget.GetComponent<ActorController>().SelectionCircle.RemoveGlow();
        }
    }
}