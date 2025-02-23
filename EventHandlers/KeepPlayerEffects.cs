using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnomalousZonePlugin.Classes;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;

namespace AnomalousZonePlugin.EventHandlers
{
    //Spent so long
    // Doesn't work like half the code here
    // Do I care enough to fix
    // no its calling me
    // the robot
    // I must do
    // github.com/9204A-B/OverUnder
    // This was so long ago
    // not even this
    // github.com/9204A-B/HighStakes 
    // is current anymore
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
            playerEffects.AddEffect(ev.Effect, ev.Intensity, ev.CurrentIntensity, ev.Duration);
            if (ev.Duration > 0f)
            {
                Timing.CallDelayed(ev.Duration, () =>
                {
                    if (ev.Player.IsAlive)
                    {
                        playerEffects.RemoveEffect(ev.Effect);
                    }
                });
            }
        }
        public void OnDying(DiedEventArgs ev)
        {
            playerEffects.ClearEffects();
        }
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Reason == SpawnReason.Escaped || ev.Reason == SpawnReason.ForceClass)
            {
                Effect[] effects = PlayerEffects.GetEffects(playerEffects);
                foreach (Effect effect in effects)
                {
                    playerEffects.RemoveEffect(effect.Type);
                }
            }
        }

    }
}