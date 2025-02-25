using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerRoles;
using UnityEngine;
using MEC;
using System.Threading;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class Rocket
    {
        public static void rocket(Player player)
        {
            string message = "";
            switch (UnityEngine.Random.Range(0, 9))
            {
                case 0:
                    {
                        message = "Went in a trip in their favorite rocketship";
                        break;
                    }
                case 1:
                    {
                        message = "Was a rocket man";
                        // 🚀 "And I think it's gonna be a long, long time"
                        //    "I'm a rocket man!" 🎶
                        break;
                    }
                case 2:
                    {
                        message = "Tried to achieve escape velocity";
                        break;
                    }
                case 3:
                    {
                        message = "Became a human firework";
                        break;
                    }
                case 4:
                    {
                        message = "Spaghettified";
                        break;
                    }
                case 5:
                    {
                        message = "Tried to noclip";
                        break;
                    }
                case 6:
                    {
                        message = "Velocity and explosion = skill issue";
                        break;
                    }
                case 7:
                    {
                        message = "Let him cook, he overcooked";
                        break;
                    }
                case 8:
                    {
                        message = "Forgetting about gravity";
                        break;
                    }
                case 9:
                    {
                        message = "The sky's the limit, but the ground was destiny";
                        break;
                    }
            }                                                 
            
            int max = 50;
            int current = 0;
            while (player.IsAlive)
            {
                player.Position += Vector3.up;
                current++;
                if (current >= max)
                {
                    Bang.bang(player, ItemType.GrenadeHE, .25f);
                    player.Kill(message);
                }

                Thread.Sleep(15); // Don't kill the server
            }
        }
    }
}
