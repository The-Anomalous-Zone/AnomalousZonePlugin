using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using AnomalousZonePlugin.EventHandlers;
using System.Threading;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class Sping // Not my job to fix spelling
    {      
        public static void spin(Player player)
        {
            // Does this code work?
            // ¯\_(ツ)_/¯ haven't tested
            float speed = Plugin.Instance.Config.spinSpeed;
            Quaternion target = player.Transform.rotation;

            target *= Quaternion.Euler(0, speed * Time.deltaTime, 0);
            player.Transform.rotation = Quaternion.RotateTowards(player.Transform.rotation, target, speed * Time.deltaTime);        
        }
    }
}
