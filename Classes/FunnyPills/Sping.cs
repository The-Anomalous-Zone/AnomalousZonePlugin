using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using AnomalousZonePlugin.EventHandlers;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class Sping // Not my job to fix spelling
    {      
        public static void spin(Player player)
        {
            // What ever this was
            // It doesn't work
            bool spinning = true;
            for (int i = 0; i < 300000; i++)
            {
                if (spinning)
                {
                    player.Rotation = new Quaternion(i, i, i, i);
                    spinning = false;
                }
                Timing.CallDelayed(.5f, () =>
                {
                    spinning = true;
                });
            }
        
        }
    }
}
