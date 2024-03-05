using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnomalousZonePlugin.Configs
{
    public class MainConfig : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;
        [Description("Is debug enabled?")]
        public bool Debug { get; set; } = false;
        [Description("Percent chance of getting Pink Candy from SCP-330")]
        public int PinkChance { get; set; } = 5;
        [Description("Does SCP-173 explode on dying?")]
        public bool ExplodeOnDeath { get; set; } = true;
        [Description("Are SCPs and Flamingos hostile?")]
        public bool Hostile { get; set; } = true;
        [Description("Enable FF when round ends?")]
        public bool RoundEndFriendlyFire { get; set; } = true;
        [Description("Percent chance of coin exploding on flip")]
        public int CoinExplosionChance { get; set; } = 5;
        [Description("Allow SCP-079 to use tesla gates during blackouts")]
        public bool allow079 { get; set; } = false;
    }
}
