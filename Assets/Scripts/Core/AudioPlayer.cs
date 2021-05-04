using UnityEngine;

namespace RogueParty.Core {
    public class AudioPlayer : MonoBehaviour {
        public static AudioPlayer Instance;
        private AudioSource audioSource;
        
        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayOneShot(string audioClip) {
            var clip = Resources.Load<AudioClip>($"Audio/{audioClip}");
            audioSource.PlayOneShot(clip, 0.7F);
        }
    }
}