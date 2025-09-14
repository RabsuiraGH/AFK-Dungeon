namespace LA.Gameplay.AbilitySystem.Interfaces
{
    public interface IOnBeforeHitAbility : IAbility
    {
        public void OnBeforeHit(BattleContext context);
    }
}