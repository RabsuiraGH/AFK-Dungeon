using LA.Gameplay.Damage;
using UnityEngine;

namespace LA.Gameplay.WeaponSystem
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Game/WeaponSO")]
    public class WeaponSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; protected set; }

        [field:TextArea]
        [field: SerializeField] public string Description { get; set; }

        [field: SerializeField] public Sprite Sprite { get; set; }

        [field: SerializeField] public int BaseDamage { get; protected set; }

        [field: SerializeField] public DamageType DamageType { get; protected set; }
    }
}