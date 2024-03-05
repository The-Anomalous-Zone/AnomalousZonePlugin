using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnomalousZonePlugin.EventHandlers
{
    internal sealed class PeanutExplodes
    {
        private Plugin plugin;
        public PeanutExplodes(Plugin plugin) => this.plugin = plugin;

        public void OnDying(DyingEventArgs ev)
        {
            if (ev.Player.Role.Type == RoleTypeId.Scp173 && Plugin.Instance.Config.ExplodeOnDeath)
            {
                ExplosiveGrenade grenade;
                grenade = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
                grenade.FuseTime = 0;
                grenade.SpawnActive(ev.Player.Position, ev.Player);
            }
        }
    }
}
