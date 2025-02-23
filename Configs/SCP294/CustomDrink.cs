using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// SCP-294 was orginally made by Creepycats here: https://github.com/creepycats/Ultimate294
// I've made some changes (sounds, drinks, text, ect)

namespace AnomalousZonePlugin.Configs.SCP294
{
    public class CustomDrink
    {
        public List<string> DrinkNames { get; set; } = new List<string>(){
            ""
        };
        public bool AntiColaModel { get; set; } = false;
        public bool KillPlayer { get; set; } = false;
        public string KillPlayerString { get; set; } = "";
        public float HealAmount { get; set; } = 0;
        public bool HealStatusEffects { get; set; } = false;
        public bool Tantrum { get; set; } = false;
        public bool Explode { get; set; } = false;
        public bool ExplodeOnBackfire { get; set; } = false;
        public float BackfireChance { get; set; } = 0f;
        public string DrinkMessage { get; set; } = "You drank the placeholder drink. Very cool";
        public List<DrinkEffect> DrinkEffects { get; set; } = new List<DrinkEffect>()
        {

        };
        public Action<Player> DrinkCallback { get; set; } = null;
    }
}