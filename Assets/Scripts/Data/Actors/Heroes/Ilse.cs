using RogueParty.Data.Skills;

namespace RogueParty.Data.Actors.Heroes {
    public class Ilse : Hero {
        public Ilse() {
            hitPoints = 70;
            manaPoints = 60;
            damage = 5;
            damageReduction = 1;
            attackRange = 500;
            attackSpeed = 50;
            moveSpeed = 25;
            
            skills.Add(new Dash());
            skills.Add(new Powershot());
        }
    }
}