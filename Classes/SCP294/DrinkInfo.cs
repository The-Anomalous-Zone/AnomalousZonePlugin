using Exiled.API.Features;
using Exiled.API.Features.Items;
using InventorySystem.Items;
using AnomalousZonePlugin.Configs.SCP294;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
// SCP-294 was orginally made by Creepycats here: https://github.com/creepycats/Ultimate294
// I've made some changes (sounds, drinks, text, ect)

namespace AnomalousZonePlugin.Classes.SCP294
{
    public class DrinkInfo
    {
        public ushort ItemSerial { get; set; } = 0;
        public Item ItemObject { get; set; } = null;
        public string DrinkName { get; set; } = "";
        public string DrinkMessage { get; set; } = "";
        public bool KillPlayer { get; set; } = false;
        public string KillPlayerString { get; set; } = "";
        public float HealAmount { get; set; } = 0;
        public bool HealStatusEffects { get; set; } = false;
        public bool Tantrum { get; set; } = false;
        public List<DrinkEffect> DrinkEffects { get; set; } = new List<DrinkEffect>()
        {

        };
        public Action<Player> DrinkCallback = null;

        public static bool IsCustomDrink(ItemBase itembase)
        {
            return SCP294.Instance.CustomDrinkItems.Keys.Contains(itembase.ItemSerial);
        }
    }
}