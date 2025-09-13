using LA;
using UnityEngine;

[System.Serializable]
public struct TurnConditionParams
{
    public Comparison comparison;
    public int turnValue;

    public enum Comparison { Equal, Less, Greater }
}

[CreateAssetMenu(menuName = "Abilities/Conditions/TurnCondition")]
public class TurnCondition : AbilityCondition<TurnConditionParams>
{
    public override bool IsMet(BattleContext context, TurnConditionParams p)
    {
        switch (p.comparison)
        {
            case TurnConditionParams.Comparison.Equal:   return context.TurnNumber == p.turnValue;
            case TurnConditionParams.Comparison.Less:    return context.TurnNumber < p.turnValue;
            case TurnConditionParams.Comparison.Greater: return context.TurnNumber > p.turnValue;
        }
        return false;
    }
}