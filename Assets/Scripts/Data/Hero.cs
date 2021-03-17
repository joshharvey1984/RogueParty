using UnityEngine;

namespace RogueParty.Data {
    [CreateAssetMenu(fileName = "New Hero", menuName = "Hero")]
    public class Hero : ScriptableObject {
        public Sprite portrait;
        public GameObject sprite;

        public int hitPoints;
        public int manaPoints;
        public float attackRange;
        public float attackSpeed;
    }
}