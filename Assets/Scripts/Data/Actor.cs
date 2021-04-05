using System.Collections.Generic;
using UnityEngine;

namespace RogueParty.Data {
    public abstract class Actor : MonoBehaviour {
        public Sprite portrait;
        public GameObject sprite;

        public int hitPoints;
        public int manaPoints;
        public int damage;
        public int damageReduction;
        public int attackRange;
        public int attackSpeed;
        public int moveSpeed;

        public List<Skill> skills = new List<Skill>();
        public GameObject actorProjectile;

        private void Awake() {
            portrait = Resources.Load<Sprite>($"Sprites/UI/Portraits/{GetType().Name}");
        }
    }
}