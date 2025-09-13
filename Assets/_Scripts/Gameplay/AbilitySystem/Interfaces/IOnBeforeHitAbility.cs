namespace LA.Gameplay.Abilities.Interfaces
{
    public interface IOnBeforeHitAbility : IAbility
    {
        public void OnBeforeHit(BattleContext context);
    }
}