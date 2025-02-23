using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// SCP-294 was orginally made by Creepycats here: https://github.com/creepycats/Ultimate294
// I've made some changes (sounds, drinks, text, ect)

namespace AnomalousZonePlugin.Classes.SCP294
{
    public enum DrinkSound
    {
        Normal = 0,
        Unstable = 1
    }
    public class DrinkSoundFiles
    {
        public List<string> List = new List<string>() {
            "slurp.ogg",
            "spit.ogg",
            "Vomit.ogg",
            "ahh.ogg",
            "burn.ogg",
            "coin_drop.ogg",
            "cough.ogg",
            "dispense0.ogg",
            "dispense1.ogg",
            "dispense2.ogg",
            "dispense3.ogg",
            "ew1.ogg",
            "ew2.ogg",
            "outofrange.ogg",
            "retch1.ogg",
            "retch2.ogg"
        };
    }
}