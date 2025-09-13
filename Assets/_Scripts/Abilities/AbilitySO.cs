using UnityEngine;

namespace LA.Abilities
{
    public class AbilitySO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; set; }
    }
}