using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Extensions;
using Exiled.CustomRoles.API;
using Exiled.Events.EventArgs.Player;
using AnomalousZonePlugin;
using UnityEngine;
using Exiled.CustomRoles;
using CustomRoles.Configs;

namespace AnomalousZonePlugin.EventHandlers
{
    internal sealed class Subclasses
    {
        private Plugin plugin;
        public Subclasses(Plugin plugin) => this.plugin = plugin;

        public void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (Plugin.Instance.Config.appearance.GetTeam() == PlayerRoles.Team.FoundationForces)
            {
                ev.IsInHurtingRange = false;
                ev.IsInIdleRange = false;
                ev.IsTriggerable = false;
                ev.DisableTesla = true;
            }
        }
     
        public void OnSpawning(SpawningEventArgs ev)
        {
            // yes
            // What was I doing again?
            // I forgor
            if (ev.OldRole == PlayerRoles.RoleTypeId.None && ev.Player.Role.Type == PlayerRoles.RoleTypeId.ClassD)
            {
                if (UnityEngine.Random.value < .4)
                {
                    //ev.Player.UniqueRole = ;
                }
            }
        }
    }
}