using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RogueParty.Core {
    public class ScreenShake : MonoBehaviour {
        private Transform _transform;
        private float _shakeDuration;
        private float _shakeMagnitude = 0.03F;
        private float _dampingSpeed = 2.0F;
        private Vector3 _initialPosition;

        private void Awake() {
            _transform = transform;
        }

        private void OnEnable() {
            FindObjectsOfType<ActorController>().ToList().ForEach(a => a.OnScreenShake += TriggerShake);
            _initialPosition = transform.localPosition;
        }

        private void Update() {
            if (_shakeDuration > 0) {
                _transform.localPosition = _initialPosition + Random.insideUnitSphere * _shakeMagnitude;
                _shakeDuration -= Time.deltaTime * _dampingSpeed;
            }
            else {
                _shakeDuration = 0F;
                _transform.localPosition = _initialPosition;
            }
        }

        private void TriggerShake(object sender, EventArgs e) {
            _shakeDuration = 0.3F;
        }
    }
}