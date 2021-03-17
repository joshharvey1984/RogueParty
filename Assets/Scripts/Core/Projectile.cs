using UnityEngine;

namespace RogueParty.Core {
    public class Projectile : MonoBehaviour {
        public GameObject Target { get; set; }
        private float speed = 15.0F;

        private void Update() {
            if (Vector2.Distance(transform.position, Target.transform.position) > 0.05F) {
                transform.position = Vector2.MoveTowards(transform.position, 
                    Target.transform.position, speed * Time.deltaTime);
            }
            else {
                Target.GetComponent<ActorController>().TakeDamage();
                Destroy(gameObject);
            }
        }
    }
}