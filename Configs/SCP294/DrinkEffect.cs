using Exiled.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// SCP-294 was orginally made by Creepycats here: https://github.com/creepycats/Ultimate294
// I've made some changes (sounds, drinks, text, ect)

namespace AnomalousZonePlugin.Configs.SCP294
{
    public sealed class DrinkEffect
    {
        public EffectType EffectType { get; set; }

        public bool ShouldAddIfPresent { get; set; }

        public float Time { get; set; }

        public byte EffectAmount { get; set; }
    }
}