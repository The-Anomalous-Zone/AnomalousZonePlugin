using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Enums;
using UnityEngine;
using Exiled.API.Features.Pickups;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class Vomit
    {
        public static ProjectileType grenadeVomit(Player player)
        {            
            switch (UnityEngine.Random.Range(0, 3))
            {
                case 0:
                    {
                        player.ThrowGrenade(ProjectileType.FragGrenade, true);
                        break;
                    }
                case 1:
                    {
                        player.ThrowGrenade(ProjectileType.Flashbang, true);
                        break;
                    }
                case 2: 
                    {
                        player.ThrowGrenade(ProjectileType.Scp2176, true);
                        break;
                    }
                case 3:
                    {
                        player.ThrowGrenade(ProjectileType.Scp018, true);
                        break;
                    }
            }
        }
        public static void itemVomit(Player player, ItemType item)
        {
            Vector3 spawnPos = player.Position + player.CameraTransform.forward * 2f;
            spawnPos.y += 2f; 
            Pickup.CreateAndSpawn(item, spawnPos, default, player);
        }
    }
}
