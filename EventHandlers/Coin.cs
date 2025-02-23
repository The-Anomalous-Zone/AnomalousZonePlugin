using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using AnomalousZonePlugin.Classes.FunnyPills;

namespace AnomalousZonePlugin.EventHandlers
{
    internal sealed class Coin
    {
        private Plugin plugin;
        public Coin(Plugin plugin) => this.plugin = plugin;

        // It's just a coin
        // What did you expect
        // no explosion
        // pffft
        // no
        // WAIt WHAT
        public void OnFlippingCoin(FlippingCoinEventArgs ev)
        {
            if (UnityEngine.Random.value < Plugin.Instance.Config.CoinExplosionChance / 100f)
            {
                Timing.CallDelayed(2.5f, () =>
                {
                    Bang.bang(ev.Player, ItemType.GrenadeHE, 0);
                });
            }
        }
    }
}