using System.Collections.Generic;
using RogueParty.Core.Actors;
using RogueParty.Data;
using UnityEngine;

namespace RogueParty.Core {
    public class Projectile : MonoBehaviour {
        public ActorController firedBy;
        public GameObject Target { get; set; }
        private float speed = 15.0F;
        public List<SkillBehaviour> projectileBehaviours;
        public List<GameObject> affectedGamObjects;

        private void Update() {
            CheckDistance();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Enemy") && !affectedGamObjects.Contains(other.gameObject)) {
                foreach (var projectileBehaviour in projectileBehaviours) {
                    projectileBehaviour.Execute(other.gameObject);
                    affectedGamObjects.Add(other.gameObject);
                }
            }
        }

        private void CheckDistance() {
            if (Vector2.Distance(transform.position, Target.transform.position) > 0.05F) {
                transform.position = Vector2.MoveTowards(transform.position, 
                    Target.transform.position, speed * Time.deltaTime);
            }
            else {
                if (projectileBehaviours == null) firedBy.ProjectileHit(Target);
                Destroy(gameObject);
            }
        }
    }
}