using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnomalousZonePlugin.Classes;
using Exiled.Events.EventArgs.Player;

namespace AnomalousZonePlugin.EventHandlers
{
    internal sealed class KeepPlayerEffects
    {
        private Plugin plugin;
        public KeepPlayerEffects(Plugin plugin) => this.plugin = plugin;
        PlayerEffects playerEffects;
        public void OnJoining(JoinedEventArgs ev)
        {
            playerEffects = new PlayerEffects(ev.Player);
        }
        public void OnGainingEffect(ReceivingEffectEventArgs ev)
        {
            playerEffects.AddEffect(ev.Effect)
        }
    }
}
