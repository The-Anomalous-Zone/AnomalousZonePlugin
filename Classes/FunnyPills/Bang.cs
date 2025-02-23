using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Enums;
using UnityEngine;
using Exiled.API.Features.Pickups;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class Bang
    {
        public static void bang(Player player, ItemType grenadeType, float fuseTime)
        {
            // Haha
            // Died to gambling mechanics
            ExplosiveGrenade grenade;
            grenade = (ExplosiveGrenade)Item.Create(grenadeType);
            grenade.FuseTime = fuseTime;
            grenade.SpawnActive(player.Position, player);
        }
    }
}