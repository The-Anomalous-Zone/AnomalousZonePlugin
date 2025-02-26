using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using PlayerRoles;

namespace AnomalousZonePlugin.EventHandlers
{
    internal sealed class Vaporize
    {
        private Plugin plugin;
        public Vaporize(Plugin plugin) => this.plugin = plugin;
        public void OnHurting(HurtingEventArgs ev)
        {
            // It took so long to prevent these lines of code from crashing the server
            // Why
            // ¯\_(ツ)_/¯
            if (ev.DamageHandler.Type == DamageType.MicroHid 
                && ev.Player.Role.Team != Team.SCPs)
            {
                ev.Player.Vaporize();
            }
        }
    }
}
