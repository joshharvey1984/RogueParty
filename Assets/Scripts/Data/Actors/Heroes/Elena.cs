using RogueParty.Data.Skills;

namespace RogueParty.Data.Actors.Heroes {
    public class Elena : Hero {
        public Elena() {
            hitPoints = 50;
            manaPoints = 100;
            damage = 4;
            damageReduction = 0;
            attackRange = 400;
            attackSpeed = 40;
            moveSpeed = 22;
            
            skills.Add(new Boulder());
        }
    }
}