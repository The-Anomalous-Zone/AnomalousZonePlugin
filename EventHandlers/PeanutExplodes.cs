using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnomalousZonePlugin.Classes.FunnyPills;

namespace AnomalousZonePlugin.EventHandlers
{
    internal sealed class PeanutExplodes
    {
        private Plugin plugin;
        public PeanutExplodes(Plugin plugin) => this.plugin = plugin;

        public void OnDying(DyingEventArgs ev)
        {
            // Concrete go boom
            if (ev.Player.Role.Type == RoleTypeId.Scp173 && Plugin.Instance.Config.ExplodeOnDeath)
            {
                Bang.bang(ev.Player, ItemType.GrenadeHE, 0);
            }
        }
    }
}