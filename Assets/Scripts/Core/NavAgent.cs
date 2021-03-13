using System;
using UnityEngine;
using UnityEngine.AI;

namespace RogueParty.Core {
    public class NavAgent : MonoBehaviour {
        private GameObject TargetDestination { get; set; }
        private NavMeshAgent Agent { get; set; }
        
        public event EventHandler OnStopNavAgent;
        public event EventHandler OnStartNavAgent;
        
        private void Start() {
            Agent = GetComponent<NavMeshAgent>();
            Agent.updateRotation = false;
            Agent.updateUpAxis = false;
            Agent.isStopped = true;
        }

        private void Update() {
            if (!Agent.isStopped) CheckForStop();
        }

        private void CheckForStop() {
            if (!TargetDestination || 
                Vector2.Distance(transform.position, TargetDestination.transform.position) < 0.05F) StopNavAgent();
        }

        private void StopNavAgent() {
            if (TargetDestination) Destroy(TargetDestination);
            Agent.isStopped = true;
            OnStopNavAgent?.Invoke(this, EventArgs.Empty);
        }

        public void SetTarget(GameObject target) {
            if (TargetDestination) Destroy(TargetDestination);
            TargetDestination = target;
            Agent.SetDestination(TargetDestination.transform.position);
            Agent.isStopped = false;
            OnStartNavAgent?.Invoke(this, EventArgs.Empty);
        }
    }
}