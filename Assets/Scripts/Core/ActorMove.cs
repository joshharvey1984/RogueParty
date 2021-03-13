using System;
using System.Collections.Generic;
using UnityEngine;

namespace RogueParty.Core {
    public class ActorMove : MonoBehaviour {
        private NavAgent _navAgent;
        private ActorAnim actorAnim;
        private SpriteRenderer spriteRenderer;
        
        [SerializeField] private int _directionFacing = 3;
        private readonly Dictionary<int, string> directionKey = new Dictionary<int, string> {
            {0, "Side"}, {1, "Up"}, {2, "Side"}, {3, "Down"}
        };

        private void Awake() {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            actorAnim = gameObject.GetComponent<ActorAnim>();
            _navAgent = GetComponent<NavAgent>();

            _navAgent.OnStopNavAgent += IdleAnimation;
            _navAgent.OnStartNavAgent += WalkAnimation;
        }

        public void Move(GameObject target) {
            FaceTarget(target);
            _navAgent.SetTarget(target);
        }

        private int DirectionCalculate(Vector3 targetPosition) {
            var diff = targetPosition - transform.position;
            var angleBetween = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            angleBetween += 45.0f;
            angleBetween %= 360;
            if (angleBetween < 0) angleBetween += 360;
            return (int) angleBetween / 90;
        }

        public void FaceTarget(GameObject target) {
            _directionFacing = DirectionCalculate(target.transform.position);
            spriteRenderer.flipX = _directionFacing == 2;
        }

        private void WalkAnimation(object sender, EventArgs e) {
            actorAnim.ChangeAnimationState($"Move_{directionKey[_directionFacing]}");
        }

        private void IdleAnimation(object sender, EventArgs e) {
            actorAnim.ChangeAnimationState($"Idle_{directionKey[_directionFacing]}");
        }

        public void AttackAnimation() {
            var direction = directionKey[_directionFacing];
            actorAnim.PlayAnimationOnce($"Attack_{direction}", $"Idle_{direction}", true);
        }

        public void HitAnimation() {
            actorAnim.PlayAnimationOnce($"Hit_{directionKey[_directionFacing]}", $"Idle_{directionKey[_directionFacing]}");
        }
    }
}