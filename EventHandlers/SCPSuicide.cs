using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerRoles;
using Exiled.API.Features;
using MEC;
using Exiled.API.Enums;

namespace AnomalousZonePlugin.EventHandlers
{
    internal sealed class SCPSuicide
    {
        private Plugin plugin;
        private Dictionary<Player, bool> isPCInflicted = new Dictionary<Player, bool>();
        public SCPSuicide(Plugin plugin) => this.plugin = plugin;

        public void OnHurting(HurtingEventArgs ev)
        {
            if(ev.Player.Role.Team == Team.SCPs && ev.Attacker.Role.Type == RoleTypeId.Scp079)
            {
                isPCInflicted.Add(ev.Player, true);
                Timing.CallDelayed(10, () =>
                {
                    isPCInflicted.Remove(ev.Player);
                });                
            }
        }
        public void OnDying(DyingEventArgs ev)
        {
            if (ev.Player.Role.Team != Team.SCPs)
                return;
            if (isPCInflicted[ev.Player])
                return;
            if (ev.DamageHandler.Type == DamageType.Tesla || ev.DamageHandler.Type == DamageType.Crushed || (ev.DamageHandler.Type == DamageType.Unknown && ev.DamageHandler.Damage >= 50000) || (ev.DamageHandler.Type == DamageType.Explosion && ev.DamageHandler.IsSuicide))
            {

            }
        }
    }
}
