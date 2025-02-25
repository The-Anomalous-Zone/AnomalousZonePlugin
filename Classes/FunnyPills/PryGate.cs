using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Interfaces;
using Exiled.Events.EventArgs.Player;
using Interactables.Interobjects;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
                   // I disabled this so long ago thinking it caused a bug
                   // But it didn't and I had nuked the code
                   // Time to rewrite it
                   // Yay!
    internal sealed class PryGate
    {
        private Plugin plugin;
        public PryGate(Plugin plugin) => this.plugin = plugin;        

        public static void pry(Player player)
        {
            Plugin.Instance.canPryGates.Add(player, true);
            player.Scale = new Vector3(1.25f, 1.1f, 1.25f);
            player.ShowHint($"<color=#0D98BA>You've become buff enough to pry doors open!</color>");
            Timing.CallDelayed(25, () =>
            {
                player.Scale = Vector3.one;
                //All of my bool dictionaries could be lists instead
                Plugin.Instance.canPryGates.Remove(player);
                if (!player.IsAlive)
                {
                    return;
                }
                player.ShowHint($"<color=#0d98ba>Your muscles are too weak</color>");
            });
        }

        public void openDoors(InteractingDoorEventArgs ev)
        {                          
            if (!Plugin.Instance.canPryGates.ContainsKey(ev.Player))
            {
                return;
            }
            if (!ev.IsAllowed)
            {
                if (ev.Door is Gate gate && !ev.Door.IsOpen)
                {
                    gate.TryPry(ev.Player);
                }
                if (ev.Door.IsDamageable && ev.Door is IDamageableDoor door)
                {
                    door.Break(Interactables.Interobjects.DoorUtils.DoorDamageType.Scp096);
                }
            }
        }
    }
}
