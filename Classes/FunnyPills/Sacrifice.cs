using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class Sacrifice
    {
        public static void sacrifice(Player player)
        {
            player.ShowHint($"<color=#0d98ba>We thank you for your <b>sacrifice</b></color>");
            Thread.Sleep(1500);
            if (!player.IsAlive)
                return;
            Vector3 pos = player.Position;
            player.Kill("Sacrificed to the pill gods");
            // Sacrifice the player

            float radius = Plugin.Instance.Config.sacrificeRadius;
            float amount = Plugin.Instance.Config.sacificeAmount;
            
            // Summon a circle made of pills
            for (int i = 0; i < amount; i++)
            {
                float angle = i / amount * 2 * Mathf.PI;

                float x = radius * Mathf.Cos(angle);
                float z = radius * Mathf.Sin(angle);
                Vector3 spawnPos = new(pos.x + x, pos.y, pos.z + z);

                Pickup.CreateAndSpawn(ItemType.Painkillers, spawnPos, default, player);
                Thread.Sleep(25); // Prevent pills from killing the server
            }
        }
    }
}
