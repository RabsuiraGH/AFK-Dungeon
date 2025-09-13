using System;
using System.Collections.Generic;
using LA.Gameplay.Abilities;
using UnityEngine;

namespace LA
{
    [Serializable]
    public class LevelUpgrade
    {
        [field: SerializeField] public Stats StatUpgrades { get; protected set; }

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