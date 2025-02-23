using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class Size
    {
        // I don't think anyone knows just how much this can be exploited
        // So I'm not fixing it
        public static void Small(Player player)
        {
            player.Scale = new Vector3(.25f, .25f, .25f);
            player.ShowHint($"<color=#0D98BA>You have turned extremely small.</color>");
            Timing.CallDelayed(60, () =>
            {
                player.Scale = Vector3.one;
            });
        }
        public static void invert(Player player)
        {
            player.ShowHint($"<color=#0D98BA>You have been inverted.</color>");
            player.Scale = new Vector3(-1 , -1, -1);
            player.CustomName = "Dinnerbone";
            Timing.CallDelayed(60, () =>
            {
                player.Scale = Vector3.one;
                player.CustomName = null;
            });
        }
        public static void large(Player player)
        {
            player.ShowHint($"<color=#0D98BA>You have turned extremely large.</color>");
            player.Scale = new Vector3(1.35f, 1.1f, 1.35f);
            Timing.CallDelayed(60, () =>
            {
                player.Scale = Vector3.one;
            });
        }
    }
}
