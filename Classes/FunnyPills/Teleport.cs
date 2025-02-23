using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Map = PluginAPI.Core.Map;
using MapGeneration;
using AnomalousZonePlugin.Classes;
using Exiled.API.Enums;
using UnityEngine;
using MEC;
using PlayerRoles;
using Exiled.API.Extensions;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public static class Teleport
    {
        public static void TP(Player player)
        {
            var chosenRoom = Map.Rooms.Where(room =>
            (room.Zone != FacilityZone.LightContainment 
            || (!Exiled.API.Features.Map.IsLczDecontaminated 
            && PluginAPI.Core.Warhead.IsDetonated))
            && (room.Zone != FacilityZone.Surface 
            ^ PluginAPI.Core.Warhead.IsDetonated)
            && room.isActiveAndEnabled
            && room.gameObject != null
            && room.Name != RoomName.Lcz173
            && room.Name != RoomName.EzCollapsedTunnel
            && room.Name != RoomName.EzEvacShelter
            ).Random();
            if (chosenRoom.Name == RoomName.LczGreenhouse)
            {
                                                // Grass (￣︶￣)👍  
                player.ShowHint($"<b><color=green>Go touch grass</color></b>");
            }
            else if (chosenRoom.Name == RoomName.Pocket)
            {
                player.ShowHint($"<color=#0d98ba>You've been teleported!");
                player.EnableEffect(EffectType.PocketCorroding);
                return;
            }

            float chance = UnityEngine.Random.value;
            player.ShowHint($"<color=#0d98ba>You've been teleported!");
            if (chance < .4)
            {                
                Vector3 first = player.Position;
                player.Teleport(chosenRoom);
                Timing.CallDelayed(UnityEngine.Random.Range(5, 15), () =>
                {
                    player.Position = first;
                    player.ShowHint($"<color=#0d98ba>Fake teleport");
                });
            }
            else if (chance < .2)
            {
                Vector3 first = player.Position;
                player.Teleport(chosenRoom);
                Timing.CallDelayed(UnityEngine.Random.Range(5, 15), () =>
                {
                    player.Position = first;
                    player.ShowHint($"<color=#0d98ba>Fake teleport");
                    Timing.CallDelayed(UnityEngine.Random.Range(5, 15), () =>
                    {
                        player.Teleport(chosenRoom); // Let's pull this from the gta chaos mod
                        player.ShowHint($"<color=#0d98ba>Fake fake teleport"); 
                    });
                });
            }

        }
        public static void SCPTP(Player player)
        {
            Player SCP = RandomPlayer.GetRandomPlayer(Team.SCPs);
            float chance = UnityEngine.Random.value;
            player.ShowHint($"<color=#0d98ba>You've been teleported to an SCP!"); // YAY!
                                           // We're really terrorizing new players with this one 
            if (chance < .4)
            {
                Vector3 first = player.Position;
                player.Position = SCP.Position;
                Timing.CallDelayed(UnityEngine.Random.Range(5, 15), () =>
                {
                    if (player.IsAlive && player.CurrentRoom.Type != RoomType.Pocket)
                    {
                        player.Position = first;
                        player.ShowHint($"<color=#0d98ba>Fake teleport");
                    }
                });
            }
            else if (chance < .2)
            {
                // Unopmitized code GO!
                Vector3 first = player.Position;
                player.Position = SCP.Position;
                Timing.CallDelayed(UnityEngine.Random.Range(5, 15), () =>
                {
                    if (player.IsAlive && player.CurrentRoom.Type != RoomType.Pocket)
                    {
                        player.Position = first;
                        player.ShowHint($"<color=#0d98ba>Fake teleport");
                        Timing.CallDelayed(UnityEngine.Random.Range(5, 15), () =>
                        {
                            if (player.IsAlive && player.CurrentRoom.Type != RoomType.Pocket)
                            {
                                player.Position = SCP.Position;
                                player.ShowHint($"<color=#0d98ba>Fake fake teleport!");
                            }
                        });
                    }
                });
            }
        }
        public static void HumanTP(Player player)
        {
            Player Human = RandomPlayer.GetRandomPlayer();
            while (Human == player)
            {
                Human = RandomPlayer.GetRandomPlayer();
            }
            Vector3 first = player.Position;
            player.Position = Human.Position;
            Human.Position = first;
            player.ShowHint($"<color=#0d98ba>You've swapped positions with </color><color={Human.Role.Type.GetColor().ToHex()}>{Human.Nickname}</color><color=#0d98ba>!");
            Human.ShowHint($"<color=#0d98ba>You've swapped positions with </color><color={player.Role.Type.GetColor().ToHex()}>{player.Nickname}</color><color=#0d98ba> by funny pills!");
        }
    }
}
