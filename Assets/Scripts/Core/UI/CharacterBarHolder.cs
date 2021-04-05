using System.Collections.Generic;
using RogueParty.Core.Actors;
using UnityEngine;

namespace RogueParty.Core.UI {
    public class CharacterBarHolder : MonoBehaviour {
        public List<GameObject> characterBars;

        public void SetHeroes(List<HeroController> heroControllers) {
            foreach (var characterBar in characterBars) {
                characterBar.GetComponent<CharacterBar>().SetHero(heroControllers[characterBars.IndexOf(characterBar)]);
            }
        }
    }
}