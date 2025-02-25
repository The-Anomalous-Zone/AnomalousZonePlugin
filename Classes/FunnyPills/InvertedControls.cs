using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Windows;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    internal sealed class InvertedControls
    {
        public static void Invert(Player player)
        {
            // Apparently if you slow down a player enough, their controls just invert
            player.EnableEffect(Exiled.API.Enums.EffectType.Slowness, 200, 0, false);
            Timing.CallDelayed(15, () =>
            {
                player.DisableEffect(Exiled.API.Enums.EffectType.Slowness);
            });
        }        
    }
}
