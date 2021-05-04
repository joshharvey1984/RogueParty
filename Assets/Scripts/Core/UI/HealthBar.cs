using UnityEngine;

namespace RogueParty.Core.UI {
    public class HealthBar : MonoBehaviour {
        private Transform bar;
        private Transform whiteBar;
        
        private void Start() {
            bar = transform.Find("Bar");
            whiteBar = transform.Find("WhiteBar");
        }

        private void Update() {
            if (whiteBar.localScale.x > bar.localScale.x)
                SetWhiteSize(whiteBar.localScale.x - 0.005F);
        }

        public void SetSize(float sizeNormalized) {
            bar.localScale = new Vector2(sizeNormalized, 1f);
        }

        private void SetWhiteSize(float sizeNormalized) {
            whiteBar.localScale = new Vector2(sizeNormalized, 1f);
            if (sizeNormalized <= 0F) gameObject.SetActive(false);
        }
    }
}