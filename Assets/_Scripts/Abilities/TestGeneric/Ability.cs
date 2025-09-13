using System.Collections.Generic;
using LA;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BaseAbility")]
public class Ability : ScriptableObject
{
    public string abilityName;
    public List<ConditionEntry> conditions;

    public void TryActivate(BattleContext context)
    {
        foreach (var entry in conditions)
        {
            if (!entry.condition.IsMet(context, entry.parameters))
                return;
        }

        Activate(context);
    }

    protected virtual void Activate(BattleContext context)
    {
        // эффект
    }
}