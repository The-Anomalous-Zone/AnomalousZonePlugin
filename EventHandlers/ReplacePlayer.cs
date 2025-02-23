using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using PlayerRoles;
using UnityEngine;
using Exiled.API.Extensions;
using InventorySystem;
using AnomalousZonePlugin.Classes;
using Exiled.API.Features.Items;

namespace AnomalousZonePlugin.EventHandlers
{
    internal sealed class ReplacePlayer
    {
        private Plugin plugin;
        public ReplacePlayer(Plugin plugin) => this.plugin = plugin;
        PlayerInventory playerInventory;

        public void OnJoining(JoinedEventArgs ev)
        {
            playerInventory = new PlayerInventory(ev.Player);
        }

        public void OnGainingItem(ItemAddedEventArgs ev)
        {
            playerInventory.addItem(ev.Item);
        }
        public void OnLosingItem(ItemRemovedEventArgs ev)
        {
            playerInventory.removeItem(ev.Item);
        }

        public void OnLeft(LeftEventArgs ev)
        {
            // It took me so long to finally put this one bit of code to prevent
            // spectators from replacing players leaving before they even joined
            if (ev.Player.Role.Type == RoleTypeId.None || 
                ev.Player.Role.Type == RoleTypeId.Spectator)
            {
                return;
            }
            List<Player> Spectators = Player.List.Where(p => p.Role.Type == RoleTypeId.Spectator).ToList();
            Player NewPlayer = Spectators[UnityEngine.Random.Range(0, Spectators.Count)];
            if (Spectators.Count() > 0)
            {
                NewPlayer.Role.Set(ev.Player.Role.Type, RoleSpawnFlags.None);
                NewPlayer.Position = ev.Player.Position;
                NewPlayer.Health = ev.Player.Health;
                NewPlayer.Broadcast(5, $"<b>You've replaced {ev.Player.Nickname} as a <color={ev.Player.Role.Type.GetColor().ToHex()}>{ev.Player.Role.Type.GetFullName()}</color></b>");
                if (ev.Player.Role.Team == Team.SCPs)
                {
                    foreach (Player player in Player.List)
                    {
                        player.Broadcast(5, $"<b><color={ev.Player.Role.Type.GetColor().ToHex()}>{ev.Player.Role.Type.GetFullName()}</color><color=#0d98ba> has been replaced</color></b>");
                    }
                }
                else if (ev.Player.IsAlive)
                {
                    foreach (Item item in PlayerInventory.getItems(playerInventory))
                    {
                        NewPlayer.AddItem(item);
                    }
                }
            }
        }
    }
}