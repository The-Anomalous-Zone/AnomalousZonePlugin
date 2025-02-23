using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using UnityEngine;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class Randomizer
    {
        private static readonly ItemType[] items = (ItemType[])Enum.GetValues(typeof(ItemType));
        private static readonly EffectType[] effects = (EffectType[])Enum.GetValues(typeof(EffectType));

        public static ItemType RandomItem()
        {
            return items[UnityEngine.Random.Range(0, items.Length - 1)];
        }
        public static EffectType RandomEffect()
        {
            return effects[UnityEngine.Random.Range(0, effects.Length - 1)];
        }
    }
}