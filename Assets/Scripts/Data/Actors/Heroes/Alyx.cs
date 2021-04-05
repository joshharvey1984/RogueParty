using RogueParty.Data.Skills;

namespace RogueParty.Data.Actors.Heroes {
    public class Alyx : Hero {
        public Alyx() {
            hitPoints = 100;
            manaPoints = 50;
            damage = 8;
            damageReduction = 1;
            attackRange = 80;
            attackSpeed = 50;
            moveSpeed = 20;

            skills.Add(new ShieldGuard());
            skills.Add(new Taunt());
        }
    }
}