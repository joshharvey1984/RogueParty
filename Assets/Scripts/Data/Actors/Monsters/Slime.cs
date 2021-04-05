namespace RogueParty.Data.Actors.Monsters {
    public class Slime : Monster {
        public Slime() {
            hitPoints = 50;
            manaPoints = 0;
            damage = 5;
            damageReduction = 0;
            attackRange = 100;
            attackSpeed = 50;
            moveSpeed = 23;
        }
    }
}