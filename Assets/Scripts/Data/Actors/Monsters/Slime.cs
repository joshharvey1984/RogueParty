namespace RogueParty.Data.Actors.Monsters {
    public class Slime : Monster {
        public Slime() {
            hitPoints = 25;
            manaPoints = 0;
            damage = 5;
            damageReduction = 0;
            attackRange = 100;
            attackSpeed = 50;
            moveSpeed = 23;
        }
    }
}