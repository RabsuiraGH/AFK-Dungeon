using UnityEngine;

[System.Serializable]
public class ConditionEntry
{
    public AbilityCondition condition;

    [SerializeReference]
    public object parameters;
}