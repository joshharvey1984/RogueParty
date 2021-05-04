using UnityEngine;

namespace RogueParty.Core.Actors {
    public class ActorAnim : MonoBehaviour {
        private Animator Animator { get; set; }
        private string CurrentState { get; set; }
        private string QueueState { get; set; }
        private bool animLock;

        private void Start() {
            Animator = GetComponent<Animator>();
        }

        public void ChangeAnimationState(string newState, bool animationLock = false) {
            if (CurrentState == newState || animLock) return;
            animLock = animationLock;
            Animator.Play(newState);
            CurrentState = newState;
        }
        
        public void PlayAnimationOnce(string anim, string queueAnim, bool animationLock = false) {
            if (CurrentState == anim || animLock) return;
            QueueState = queueAnim;
            ChangeAnimationState(anim, animationLock);
        }

        public void PlayQueuedAnim() {
            animLock = false;
            Animator.Play(QueueState);
            CurrentState = QueueState;
        }

        public void DeathAnimation() {
            QueueState = null;
            animLock = true;
            Animator.Play("Die");
            CurrentState = "Die";
        }
    }
}