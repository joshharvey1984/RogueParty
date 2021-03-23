using System.Collections.Generic;
using UnityEngine;

namespace RogueParty.Data {
    public abstract class Actor : ScriptableObject {
        public Sprite portrait;
        public GameObject sprite;

        public int hitPoints;
        public int manaPoints;
        public int damage;
        public int damageReduction;
        public float attackRange;
        public int attackSpeed;
        public int moveSpeed;

        public List<Skill> skills;
        
    }
}