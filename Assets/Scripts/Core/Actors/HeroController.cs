using System;
using RogueParty.Core.UI;
using RogueParty.Data;
using UnityEngine;

namespace RogueParty.Core.Actors {
    public class HeroController : ActorController {
        private GameObject pointTarget;
        private GameObject areaTarget;
        private ITargeting currentTargeting;
        private GameObject instTarget;
        public event EventHandler OnMouseClick;
        public event EventHandler<TargetingModeArgs> OnTargetingMode;
        public class TargetingModeArgs {
            public ITargeting Targeting; 
        }
        private new void Awake() {
            base.Awake();
            pointTarget = Resources.Load<GameObject>("Prefabs/UI/PointTarget");
            areaTarget = Resources.Load<GameObject>("Prefabs/UI/AreaTarget");
        } 

        private new void Update() {
            base.Update();
            if (currentTargeting != null) RedrawTargeting();
        }
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

        public void SkillButtonClicked(object sender, SkillButton.SkillClickArgs skillClickArgs) {
            ExecuteSkill(skillClickArgs.SkillClicked);
        }
        
        public void TargetingMode(ITargeting targeting) {
            currentTargeting = targeting;
            OnTargetingMode?.Invoke(this, new TargetingModeArgs {Targeting = targeting});
        }

        public void TargetingModeOff() {
            currentTargeting = null;
            EraseTargeting();
        }
        
        private void RedrawTargeting() {
            EraseTargeting();
            if (currentTargeting.GetType() == typeof(TargetPointTargeting)) PointTarget();
            if (currentTargeting.GetType() == typeof(AreaTargeting)) AreaTarget();
        }

        private void PointTarget() {
            instTarget = Instantiate(pointTarget, transform.position, Quaternion.identity);
            instTarget.GetComponent<LineRenderer>().SetPositions(new []
                {transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)});
        }

        private void AreaTarget() {
            instTarget = Instantiate(areaTarget, transform.position, Quaternion.identity);
            instTarget
                .GetComponent<CircleDraw>().Draw(Camera.main.ScreenToWorldPoint(Input.mousePosition), 
                    currentTargeting.Skill.Range);
        }

        private void EraseTargeting() {
            if (instTarget != null) Destroy(instTarget);
        }
    }
}