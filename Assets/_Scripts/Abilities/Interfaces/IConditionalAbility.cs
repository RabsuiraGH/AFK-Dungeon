namespace LA.Abilities.Interfaces
{
    public interface IConditionalAbility : IAbility
    {
        public bool CheckCondition(BattleContext context);
    }
}