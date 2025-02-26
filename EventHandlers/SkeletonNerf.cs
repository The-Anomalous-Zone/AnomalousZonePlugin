using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Enums;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;
using MEC;
using System.Threading;

namespace AnomalousZonePlugin.EventHandlers
{
    internal sealed class SkeletonNerf
    {
        private Plugin plugin;
        public SkeletonNerf(Plugin plugin) => this.plugin = plugin;
        public void OnHurting(HurtingEventArgs ev)
        {
            if (ev.DamageHandler.Type == DamageType.Strangled)
            {
                Thread.Sleep(((int)Plugin.Instance.Config.StrangleLimit) * 1000);
                ev.IsAllowed = false;
                Thread.Sleep((int)Plugin.Instance.Config.StrangleCooldown * 1000);
                ev.IsAllowed = true;
            }
        }
    }
}