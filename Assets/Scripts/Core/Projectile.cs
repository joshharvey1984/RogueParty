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

        private void Update() {
            CheckDistance();
            if (projectileBehaviours != null) CheckCollider();
        }

        private void CheckCollider() {
            
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