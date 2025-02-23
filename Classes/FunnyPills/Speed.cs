using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using UnityEngine;
using MEC;
using System.Runtime.InteropServices;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class Speed
    {                  // speed (^▽^)👍  
        public static void speed(Player player)
        {
            float time = UnityEngine.Random.Range(Plugin.Instance.Config.PillsSpeedMinTime, Plugin.Instance.Config.PillsSpeedMaxTime);
            player.EnableEffect(EffectType.MovementBoost, time);
            player.ChangeEffectIntensity(EffectType.MovementBoost, 255);
            player.EnableEffect(EffectType.Invigorated, time);
            player.ShowHint($"<color=#0d98ba>You feel energized</color>");
            //player.IsGodModeEnabled = true;
            //for (int i = 0; i < 15;  i++)
            //{
            //    Bang.bang(player, ItemType.GrenadeHE, 0);
            //}
            Timing.CallDelayed(time, () =>
            {               
                //Timing.CallDelayed(1.5f, ()=> { player.IsGodModeEnabled=false; });
                player.DisableEffect(EffectType.MovementBoost);
                player.DisableEffect(EffectType.Invigorated);
                player.EnableEffect(EffectType.Disabled, 8);
                player.EnableEffect(EffectType.Concussed, 8);
                player.Health = 10;
                player.ShowHint($"<color=#0d98ba>You don't feel so good...</color>");
            });
        }
    }
}
