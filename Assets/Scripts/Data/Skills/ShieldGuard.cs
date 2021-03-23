namespace RogueParty.Data.Skills {
    internal class ShieldGuard : Skill {
        protected override void Execute() {
            description = "Alyx increases his Damage Reduction but lessens his move speed and can't attack.";
            var applyStatusEffectSelf = new ApplyStatusEffectSelf(gameObject, new StatusEffects.ShieldGuard());
        }
    }
}