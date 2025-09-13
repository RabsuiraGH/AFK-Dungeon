using LA;
using UnityEngine;

public abstract class AbilityCondition : ScriptableObject
{
    public abstract bool IsMet(BattleContext context, object parameters);
}

public abstract class AbilityCondition<TParams> : AbilityCondition where TParams : struct
{
    public abstract bool IsMet(BattleContext context, TParams parameters);

    public override bool IsMet(BattleContext context, object parameters)
    {
        return IsMet(context, (TParams)parameters);
    }
}