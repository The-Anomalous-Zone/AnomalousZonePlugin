using AnomalousZonePlugin.Configs.SCP294;
using Exiled.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// SCP-294 was orginally made by Creepycats here: https://github.com/creepycats/Ultimate294
// I've made some changes (sounds, drinks, text, ect)

namespace AnomalousZonePlugin.Configs.SCP294
{
    public class SpawningConfig
    {
        public int SpawnAmount { get; set; } = 1;
        public Dictionary<RoomType, List<SpawnTransform>> SpawnRooms { get; set; } = new Dictionary<RoomType, List<SpawnTransform>>();
    }
}