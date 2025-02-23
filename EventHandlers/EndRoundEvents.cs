using Player = Exiled.API.Features.Player;
using Server = Exiled.API.Features.Server;
using Warhead = Exiled.API.Features.Warhead;
using UnityEngine;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEC;
using PluginAPI.Core;
using InventorySystem;
using Exiled.API.Features.Doors;

namespace AnomalousZonePlugin.EventHandlers
{
    internal sealed class EndRoundEvents
    {
        private Plugin plugin;
        public EndRoundEvents(Plugin plugin) => this.plugin = plugin;
        private bool roundLock = false;

        public void OnDying(DyingEventArgs ev)
        {
            //Uncomment during christmas update

            //List<Player> SCPlist = Player.List.Where(p => p.Role.Team == Team.SCPs);
            //Player Flamlist = Player.List.Where(p => p.Role.Team == Team.Flamingos);
            //if (SCPlist.Count() > 0 &&  Flamlist.Count() > 0 && Plugin.Instance.Config.Hostile)
            //{
            //    roundLock = true;
            //}
            //else { roundLock = false; }

        }
        //public void OnRoundEnding(EndingRoundEventArgs ev)
        //{
        //    if (roundLock)
        //    {
        //        ev.IsRoundEnded = false;
        //    }
        //}
        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            //Add to the server chaos that has the potential to kill the server
            if (Plugin.Instance.Config.RoundEndFriendlyFire) { Server.FriendlyFire = true; }

            Timing.CallDelayed(5, () => 
            {
                Warhead.Detonate();
                int random = UnityEngine.Random.Range(0, 3);
                List<Player> players = Player.List.ToList();
                switch (random)
                {
                    case 0:
                        {
                            foreach(Player player in players)
                            {
                                player.Role.Set(RoleTypeId.Scp096);
                            }
                            Timing.CallDelayed(1, () => { Warhead.Detonate(); });
                            break;
                        }
                    case 1:
                        {
                            Respawn.Spawn(Respawning.SpawnableTeamType.ChaosInsurgency, true);
                            break;
                        }
                    case 2:
                        {
                            Respawn.Spawn(Respawning.SpawnableTeamType.NineTailedFox, true);
                            break;
                        }
                    case 3:
                        {
                            foreach (Player player in players)
                            {
                                player.Role.Set(RoleTypeId.ClassD);
                                for (int i = 0; i < 8; i++)
                                {
                                    player.Inventory.ServerAddItem(ItemType.Painkillers);
                                }
                            }
                            break;
                        }
                }
            });
        }
        public void OnWaitingForPlayers()
        {
            //Try to undo some of the chaos
            Server.FriendlyFire = false;
        }
        public void OnRoundStart()
        {
            // Hmm... yes
            // Let's check
            // File name: EndRoundEvents.cs             ✓
            // Method name: OnRoundStart()              ✓
            // When does it active? Beginning of round  ✓
            // Does it make sense with the file name    ✓

            var spawn = Exiled.API.Features.Room.Get(Exiled.API.Enums.RoomType.LczClassDSpawn);
            foreach (Door door in spawn.Doors)
            {
                door.IsOpen = true;
            }
        }
    }
}
