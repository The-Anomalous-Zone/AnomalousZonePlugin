using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Events.EventArgs.Player;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Scp079;
using Exiled.Events.EventArgs.Scp3114;
using Exiled.API.Enums;
using PlayerRoles;
using PluginAPI;
using MEC;
using UnityEngine;
using Exiled.API.Features.Roles;

namespace AnomalousZonePlugin.EventHandlers
{
    internal sealed class TeslaBlackouts
    {
        private Plugin plugin;
        public TeslaBlackouts(Plugin plugin) => this.plugin = plugin;
        private Dictionary<Room, bool> RoomName = new Dictionary<Room, bool>();
        private Dictionary<ZoneType, bool> zone = new Dictionary<ZoneType, bool>();
        private Team team = Team.SCPs;

        public void OnRoomBlackout(RoomBlackoutEventArgs ev)
        {
            RoomName.Add(ev.Room, true);
            Timing.CallDelayed(15, () => { RoomName.Remove(ev.Room); });
        }
        public void OnZoneBlackout(ZoneBlackoutEventArgs ev)
        {
            zone.Add(ev.Zone, true);
            Timing.CallDelayed(60, () => { zone.Remove(ev.Zone); });
        }
        public void OnDisguising(DisguisingEventArgs ev)
        {
            team = ev.Ragdoll.Role.GetTeam();
        }
        public void OnRevealing(RevealingEventArgs ev)
        {
            team = Team.SCPs;
        }
        public void OnInteractingTesla(InteractingTeslaEventArgs ev)
        {
            if (RoomName.ContainsKey(ev.Tesla.Room) || zone.ContainsKey(ZoneType.HeavyContainment) && !Plugin.Instance.Config.allow079)
            {
                ev.Tesla.HurtRange = Vector3.zero;
                ev.Tesla.TriggerRange = 0;
                ev.Tesla.IsIdling = false;
                ev.Tesla.UseInstantBurst = false;
            }
        }
        // Nobody knows how broken these are
        // Blackouts sometimes just trigger them forever
        // Blackouts sometimes allow them to trigger for MTF
        // Sometimes they don't trigger during blackouts (like I want it)
        // Too lazy to fix
        public void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if ((ev.Player.Role.Team == Team.FoundationForces || ev.Player.Role.Team == Team.Scientists) || RoomName.ContainsKey(ev.Player.CurrentRoom) || zone.ContainsKey(ZoneType.HeavyContainment) ||
                (ev.Player.Role.Type == RoleTypeId.Scp3114 && (team == Team.FoundationForces || team == Team.Scientists)))
            {
                ev.IsInHurtingRange = false;
                ev.IsInIdleRange = false;
                ev.DisableTesla = true;
                ev.IsTriggerable = false;
            }
        }
    }
}