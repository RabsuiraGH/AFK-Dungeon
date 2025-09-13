namespace LA.Abilities.Interfaces
{
    public interface IStatAbility : IAbility
    {
        public void Apply(BattleUnit unit);

        public void Remove(BattleUnit unit);
    }
}