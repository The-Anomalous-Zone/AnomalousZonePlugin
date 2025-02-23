using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;
using Exiled.API.Features;
using Server = Exiled.API.Features.Server;
using Round = Exiled.API.Features.Round;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nuke = Exiled.Events.EventArgs.Warhead;

namespace AnomalousZonePlugin.EventHandlers
{
    //Didn't even try
    internal sealed class Radiation
    {        
        private Plugin plugin;
        public Radiation(Plugin plugin) => this.plugin = plugin;
        private bool radiation = false;
        public void OnNuke(Nuke.DetonatingEventArgs ev)
        {
            if (ev.IsAllowed)
            {
                if (Round.IsLocked)
                    return;
                Timing.CallDelayed(Plugin.Instance.Config.TimeUntilRadiation * 60, () =>
                {
                    //radiation = true;
                });
            }
            if (radiation)
            {
                foreach (Player player in Player.List.ToList())
                {
                    player.EnableEffect(EffectType.Asphyxiated);
                    player.EnableEffect(EffectType.Concussed);
                }
            }
        }
        public void OnSpawning(SpawningEventArgs ev)
        {
            if (radiation)
            {
                foreach (Player player in Player.List.ToList())
                {
                    player.EnableEffect(EffectType.Asphyxiated);
                    player.EnableEffect(EffectType.Concussed);
                }
            }
        }
    }
}
