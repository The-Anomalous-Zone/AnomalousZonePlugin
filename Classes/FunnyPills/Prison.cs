using AdminToys;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class Prison
    {
        public static void Jail(Player player)
        {
            // Turns out really only one of the towers is good at holding players
            // Also it's the admin tower
            // Do I want to use unity and MER to jail a player
            // I was going to but forgot to
            Vector3 position = player.Position;
            float time = UnityEngine.Random.Range(Plugin.Instance.Config.JailMinTime, Plugin.Instance.Config.JailMaxTime);
            //switch (UnityEngine.Random.Range(0, 5))
            //{
            //    case 0:
            //        {
            //            player.Position = new Vector3(0, 1013, -40);
            //           // Timing.CallDelayed()
            //            break;
            //        }
            //}
           
        }
    }
}
