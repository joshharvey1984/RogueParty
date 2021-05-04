using UnityEngine;

namespace RogueParty.Core {
    public class PauseManager : MonoBehaviour {
        public bool slowMo;
        private void Update() {
            if (Input.GetKeyUp(KeyCode.Space)) {
                if (!slowMo)
                    SlowMoOn();
                else SlowMoOff();
            }
        }

        private void SlowMoOff() {
            Time.timeScale = 1.0F;
            slowMo = false;
        }

        private void SlowMoOn() {
            Time.timeScale = 0.1F;
            slowMo = true;
        }
    }
}
