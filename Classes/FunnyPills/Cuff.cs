using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using MEC;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class Cuff
    {
        public static void cuff(Player player)
        {
            player.Handcuff();
            player.DropItems();
            player.ShowHint($"<color=#0d98ba>You should probably stop</color>");
                                           // You did it to yourself
                         // You shall deal with your shame as everyone steals your stuff
                                    // At least you didn't turn into pills
            Timing.CallDelayed(30, () => 
            {
                if (player.IsCuffed)
                    player.RemoveHandcuffs();
            });
        }
    }
}
