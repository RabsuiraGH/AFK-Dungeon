using System;
using System.Collections.Generic;
using LA.Gameplay.AbilitySystem;
using UnityEngine;

namespace LA.Gameplay.Player.ClassSystem
{
    [Serializable]
    public class LevelUpgrade
    {
        [field: SerializeField] public Stat.Stats StatUpgrades { get; protected set; }

        [field: SerializeField] public List<AbilitySO> NewAbilities { get; protected set; }


        public void ApplyToPlayer(Player player)
        {
            player.Stats.AddStats(StatUpgrades);
            player.Abilities.AddRange(NewAbilities);
        }


        public void RemoveFromPlayer(Player player)
        {
            player.Stats.RemoveStats(StatUpgrades);

            player.Abilities.RemoveAll(x => NewAbilities.Contains(x));
        }
    }
}