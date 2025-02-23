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
using Hints;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class Vomit
    {                   //self explanatory
        public static void grenadeVomit(Player player)
        {
            string hint = "";
            switch (UnityEngine.Random.Range(0, 3))
            {
                case 0:
                    {
                        player.ThrowGrenade(ProjectileType.FragGrenade, true);
                        hint = "Haha, go bang!";
                        break;
                    }
                case 1:
                    {
                        player.ThrowGrenade(ProjectileType.Flashbang, true);
                        hint = "Be blind";
                        break;
                    }
                case 2:
                    {
                        player.ThrowGrenade(ProjectileType.Scp2176, true);
                        hint = "What does this do?";
                        break;
                    }
                case 3:
                    {
                        player.ThrowGrenade(ProjectileType.Scp018, true);
                        hint = "Here, catch";
                        break;
                    }
            }
            player.ShowHint($"<color=#0d98ba>{hint}</color>");
        }
        public static void itemVomit(Player player, ItemType item)
        {
            Vector3 spawnPos = player.Position + player.CameraTransform.forward * 2f;
            spawnPos.y += 2f;
            Pickup.CreateAndSpawn(item, spawnPos, default, player);
        }
    }
}