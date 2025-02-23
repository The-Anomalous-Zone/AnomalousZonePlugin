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
    public class SpawnTransform
    {
        public Vector3 Position { get; set; } = new Vector3(0, 0, 0);
        public Vector3 Rotation { get; set; } = new Vector3(0, 0, 0);
        public Vector3 Scale { get; set; } = Vector3.one;
    }
}