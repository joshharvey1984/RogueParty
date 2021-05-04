using System;
using System.Collections.Generic;
using UnityEngine;

namespace RogueParty.Core.Actors {
    public class ActorMove : MonoBehaviour {
        private ActorNavAgent actorNavAgent;
        private ActorAnim actorAnim;
        private SpriteRenderer spriteRenderer;
        
        private int _directionFacing = 3;
        private readonly Dictionary<int, string> directionKey = new Dictionary<int, string> {
            {0, "Side"}, {1, "Up"}, {2, "Side"}, {3, "Down"}
        };

        public GameObject directMoveTarget;
        public int directMoveSpeed;

        private void Awake() {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            actorAnim = gameObject.GetComponent<ActorAnim>();
            actorNavAgent = GetComponent<ActorNavAgent>();

            actorNavAgent.OnStopNavAgent += IdleAnimation;
            actorNavAgent.OnStartNavAgent += WalkAnimation;
        }

        private void Update() {
            if (directMoveTarget) UpdateDirectMove();
        }

        private void UpdateDirectMove() {
            var targetPos = directMoveTarget.transform.position;
            if (Vector2.Distance(transform.position, targetPos) > 0.1F) {
                transform.position = Vector2.MoveTowards
                    (transform.position, targetPos, directMoveSpeed * Time.deltaTime);
            }
            else {
                DirectMoveComplete();
            }
        }

        public void Move(GameObject target) {
            FaceTarget(target);
            actorNavAgent.SetTarget(target);
        }

        public void StopMove() {
            actorNavAgent.StopNavAgent();
        }

        public void DirectMove(GameObject target, int speed) {
            FaceTarget(target);
            directMoveSpeed = speed;
            directMoveTarget = target;
        }

        private void DirectMoveComplete() {
            Destroy(directMoveTarget);
            directMoveTarget = null;
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

        public void DieAnimation() {
            actorAnim.DeathAnimation();
        }

        public void CastAnimation(string anim) {
            actorAnim.ChangeAnimationState($"{anim}_{directionKey[_directionFacing]}");
        }
    }
}