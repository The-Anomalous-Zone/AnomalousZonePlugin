using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Extensions;
using Exiled.CustomRoles.API;
using Exiled.Events.EventArgs.Player;
using AnomalousZonePlugin;

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
    }
}
