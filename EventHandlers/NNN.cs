using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Enums;
using PlayerRoles;
using Exiled.Events.EventArgs.Player;

namespace AnomalousZonePlugin.EventHandlers
{
    internal sealed class NNN
    {
        private Plugin plugin;
        public NNN(Plugin plugin) => this.plugin = plugin;
        private List<RoleTypeId> ScpRoles = new List<RoleTypeId>
        {
            RoleTypeId.Scp049,
            RoleTypeId.Scp096,
            RoleTypeId.Scp106,
            RoleTypeId.Scp939,
            RoleTypeId.Scp3114,
        };
        private RoleTypeId GetAvailableScp()
        {
            RoleTypeId chosen = RoleTypeId.None;
            ScpRoles.ShuffleList();
            foreach (RoleTypeId role in ScpRoles)
            {
                if (Player.Get(role).Count() < 1)
                {
                    chosen = role;
                    break;
                }
            }
            if (chosen == RoleTypeId.None)
                chosen = ScpRoles.First();
            return chosen;
        }
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            // You're not failing no nut november in this server
            // Not even an admin has the power to allow the NUT
            if (ev.NewRole == RoleTypeId.Scp173 && DateTime.Now.Month == 11)
            {
                ev.NewRole = GetAvailableScp();
            }
        }
    }
}