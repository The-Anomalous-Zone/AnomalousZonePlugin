using AnomalousZonePlugin.Configs;
using Exiled.Events.Patches.Events.Scp330;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Events.EventArgs.Scp330;
using UnityEngine;

namespace AnomalousZonePlugin.EventHandlers
{
    internal sealed class Candy
    {
        // Candy
        // (👍^▽^)👍  
        private Plugin plugin;
        public Candy(Plugin plugin) => this.plugin = plugin;
        public void OnInteractingScp330(InteractingScp330EventArgs ev)
        {

            float chance = Plugin.Instance.Config.PinkChance / 100f;
            if (UnityEngine.Random.value < chance)
            {
                ev.Candy = InventorySystem.Items.Usables.Scp330.CandyKindID.Pink;
            
            }

        }
    }
}
