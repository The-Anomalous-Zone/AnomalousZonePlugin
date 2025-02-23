using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerRoles;
using UnityEngine;
using MEC;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class Rocket
    {
        public static void rocket(Player player)
        {
            // 🚀 "And I think it's gonna be a long, long time"
            //    "I'm a rocket man!" 🎶
            ServerConsole.EnterCommand($"rocket {player.Id} 1");
            // I had actual code but lost it so I did this and was too lazy
            // Hopefully admintools doesn't randomly break (like it normally does)          
        }
    }
}
