using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class MaxHP
    {
        public static void IncreaseMaxHP(Player player)
        {
            player.MaxHealth += 20;
            player.Heal(player.MaxHealth);
            player.ShowHint($"<color=#0d98ba>Your maximum HP has increased to {player.MaxHealth}!</color>");
        }
        public static void DecreaseMaxHP(Player player)
        {
            player.MaxHealth -= 20;
            player.Heal(player.MaxHealth);
            player.ShowHint($"<color=#0d98ba>Your maximum HP has decreased to {player.MaxHealth}!</color>");
        }
    }
}
