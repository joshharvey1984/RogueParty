using UnityEngine;

namespace RogueParty.Core {
    public class SelectionCircle : MonoBehaviour {
        private static readonly int Glow = Shader.PropertyToID("_Glow");
        private Renderer Renderer { get; set; }

        private void Awake() {
            Renderer = GetComponent<Renderer>();
        }

        public void AddGlow() => Renderer.material.SetInt(Glow, 85);
        public void RemoveGlow() => Renderer.material.SetInt(Glow, 0);
    }
}