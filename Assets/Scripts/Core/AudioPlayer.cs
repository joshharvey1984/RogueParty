using System.Linq;
using RogueParty.Core.Actors;
using UnityEngine;

namespace RogueParty.Core {
    public class AudioPlayer : MonoBehaviour {
        private AudioSource audioSource;
        
        private void Awake() => audioSource = GetComponent<AudioSource>();

        private void OnEnable() {
            FindObjectsOfType<ActorController>().ToList().ForEach(a => a.OnPlayAudioClip += PlayOneShot);
        }

        private void PlayOneShot(object sender, ActorController.AudioClipEventArgs audioClipEventArgs) {
            var clip = Resources.Load<AudioClip>($"Audio/{audioClipEventArgs.AudioClip}");
            audioSource.PlayOneShot(clip, 0.7F);
        }
    }
}