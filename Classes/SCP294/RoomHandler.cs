using Exiled.API.Enums;
using Exiled.API.Features;
using Mirror;
using Mono.Cecil;
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
    public class RoomHandler
    {
        public static Room GetRandomRoom(RoomType _roomType)
        {
            IEnumerable<Room> _roomList = Room.Get((Room room) => room.Type == _roomType);
            return _roomList.ElementAtOrDefault(UnityEngine.Random.Range(0, _roomList.Count()));
        }
        public static Room GetRandomRoom(List<RoomType> _roomType)
        {
            IEnumerable<Room> _roomList = Room.Get((Room room) => _roomType.Contains(room.Type));
            return _roomList.ElementAtOrDefault(UnityEngine.Random.Range(0, _roomList.Count()));
        }
    }
}