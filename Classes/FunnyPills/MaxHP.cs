﻿using Exiled.API.Features;
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
            // I'm curious what the chance of the player managing
            // to get their health to negative would be
            // There are currently 27 cases for the funny pills
            // then from the health case is a 50% of increasing health
            // So there is about a 1.8% chance of increasing the player's health
            // If we assume that the player is starting at 100HP
            // then the player would have take at least 107,374,183 painkillers
            // to roll over so if we combine that with the chance of getting it
            // the player would need to take approximately 6 billion painkillers
            // on average to reach the int limit by the law of large
            // numbers(real thing) it's actually a 50% chance! (not considering dying)
            // Let's consider the chances of dying restarting everything
            // It should raise the average number of required pills to 7.32 billion
            // The chance now is 9.09% with dying factored in
            // This is actually wrong since health is a float not int
            // float has a max value of 340,282,300,000,000,000,000,000,000,000,000,000,000
            // and overflow doesn't go negative it goes to infinity, so the player would have to
            // take at least 17,014,115,000,000,000,000,000,000,000,000,000,000,000 painkillers
            // It would require on average 36.7 decillion painkillers
            // My calculator refuses to give an approximate decimal but the chance of doing it is
            // 1/(10^(5.5x10^39)) which is basically zero
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
