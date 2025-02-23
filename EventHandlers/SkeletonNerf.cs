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

namespace AnomalousZonePlugin.EventHandlers
{
    internal sealed class SkeletonNerf
    {
        private Plugin plugin;
        public SkeletonNerf(Plugin plugin) => this.plugin = plugin;
        public void OnHurting(HurtingEventArgs ev)
        {
            if (true)
                return;
            // Fix
            //I forgor
            if (ev.DamageHandler.Type == DamageType.Strangled)
            {
                Timing.CallDelayed(Plugin.Instance.Config.StrangleLimit, () =>
                {
                    ev.IsAllowed = false;
                    Timing.CallDelayed(Plugin.Instance.Config.StrangleCooldown, () =>
                    {
                        ev.IsAllowed = true;
                    });
                });
            }
        }
    }
}