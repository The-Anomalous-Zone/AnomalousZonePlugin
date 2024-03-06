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
            playerEffects.AddEffect(ev.Effect, ev.Intensity, ev.CurrentIntensity, ev.Duration);
            if (ev.Duration > 0f)
            {
                Timing.CallDelayed(ev.Duration, ()=>
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
            if (ev.Reason == SpawnReason.Escaped)
            {
                Effect[] effects = PlayerEffects.GetEffects(ev.Player);
                foreach (Effect effect : effects)
                {
                    playerEffects.RemoveEffect(effect);
                    OnGainingEffect(ev.Player, effect.GetEffect, effect.GetIntensity, effect.GetCurrentIntensity, effect.GetDuration);
                }
            }
        }
                            
    }
}
