using UnityEngine;

namespace RogueParty.Core.Actors {
    public class MonsterController : ActorController {
        private void Start() {
            GetNearestTarget();
        }

        private void GetNearestTarget() {
            var targets = FindObjectsOfType<HeroController>();
            foreach (var target in targets) {
                if (EnemyTarget == null) EnemyTarget = target.gameObject;
                var targetDistance = Vector2.Distance(target.gameObject.transform.position, 
                    transform.position);
                if (targetDistance < Vector2.Distance(EnemyTarget.transform.position, transform.position))
                    EnemyTarget = target.gameObject;
            }
        }
    }
}