using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerRoles;
using Exiled.API.Enums;
using UnityEngine;
using Exiled.API.Features.Pickups;
using Exiled.API.Extensions;
using CustomRoles.API;
using MEC;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class SummonPlayer
    {
        public static void Summon(Player player)
        {
            List<Player> Dead = Player.List.Where(player => player.Role.Type == RoleTypeId.Spectator).ToList();
            if (Dead.Count > 0)
            {
                RoleTypeId spawnPlayerRole;
                switch (player.Role.Type)
                {
                    case RoleTypeId.ClassD:
                    case RoleTypeId.ChaosConscript:
                    case RoleTypeId.ChaosRifleman:
                    case RoleTypeId.ChaosRepressor:
                    case RoleTypeId.ChaosMarauder:
                        spawnPlayerRole = RoleTypeId.ChaosRifleman;
                        break;
                    case RoleTypeId.Scientist:
                    case RoleTypeId.FacilityGuard:
                    case RoleTypeId.NtfPrivate:
                    case RoleTypeId.NtfSergeant:
                    case RoleTypeId.NtfSpecialist:
                    case RoleTypeId.NtfCaptain:
                        spawnPlayerRole = RoleTypeId.NtfSergeant;
                        break;
                    default:
                        // Unless some weird stuff is going down
                        // This will never happen (hopefully)
                        spawnPlayerRole = RoleTypeId.Scp0492;
                        break;
                }
                Player spawnPlayer = Dead.Random();
                var roleString = $"<color={spawnPlayerRole.GetColor().ToHex()}>{spawnPlayerRole.GetFullName()}</color>";
                spawnPlayer.Position = player.Position;
                spawnPlayer.Role.Set(spawnPlayerRole, SpawnReason.Respawn, RoleSpawnFlags.All);
                spawnPlayer.Position = player.Position;
                
                player.Broadcast(5, $"Summoning a {roleString}");
                spawnPlayer.Broadcast(16, "<color=#0d98ba>You have been summoned by Funny pills</color>",
                    Broadcast.BroadcastFlags.Normal, true);
                Timing.CallDelayed(1, () =>
                {
                    Vector3 spawnLocation = player.Position;
                    if (player.IsAlive)
                    {
                        spawnPlayer.Teleport(spawnLocation);
                    }
                });
            }
        }
    }
}
