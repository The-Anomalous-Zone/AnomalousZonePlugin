using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginAPI.Core;
using Exiled.API.Enums;
using MapGeneration;
using Exiled.API.Extensions;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    // Let's go doxxing!!
    public class IPLeak
    {
        public static void Leak(Exiled.API.Features.Player player)
        {
            var room = player.CurrentRoom;
            var zone = room.Zone;
            string ZoneName = " ";
            if (zone.GetZone() == FacilityZone.Other)
            {
                ZoneName = "at Checkpoint";
            }
            if (zone.GetZone() == FacilityZone.Entrance)
            {
                ZoneName = "in Entrance";
            }
            if (zone.GetZone() == FacilityZone.HeavyContainment)
            {
                ZoneName = "in Heavy";
            }
            if (zone.GetZone() == FacilityZone.LightContainment)
            {
                ZoneName = "in Light";
            }
            if (zone.GetZone() == FacilityZone.Surface)
            {
                ZoneName = "on Surface";
            }
            if (zone.GetZone() == FacilityZone.None)
            {
                ZoneName = "at I don't know where";
            }


            player.ShowHint($"<color=#0D98BA>Your IP address has been leaked!</color>");            

            foreach(Exiled.API.Features.Player players in Exiled.API.Features.Player.List.ToList())
            {
                players.Broadcast(10, $"<color=#0D98BA>{player.Nickname} is a </color><color={player.Role.Type.GetColor().ToHex()}>{player.Role.Type.GetFullName()} <color=#0D98BA>{ZoneName}!</color>");
            }
        }
    }
}
