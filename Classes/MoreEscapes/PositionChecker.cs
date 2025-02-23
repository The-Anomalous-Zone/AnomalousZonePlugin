using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using PlayerRoles;
using UnityEngine;

namespace AnomalousZonePlugin.Classes.MoreEscapes
{
    // Let's go gambling!
    // Will the server die or will the guard become MTF
    // Who knows!
    public class PositionChecker
    {
        private static Vector3 exitcorner1 = new Vector3(125, 988.8f, 19.2f);
        private static Vector3 exitcorner2 = new Vector3(122.4f, 988.8f, 19.2f);

        public static bool Check(Player player)
        {
            if (player.CurrentRoom.Type != Exiled.API.Enums.RoomType.Surface)
            {
                return false;
            }
            
            float diffZ = Mathf.Abs(player.Position.z - exitcorner1.z);
            if (diffZ > Plugin.Instance.Config.ErrorRange)
            {
                return false;
            }

            if (player.Position.x <= exitcorner1.x && player.Position.x >= exitcorner2.x) 
            {
                return true;
            }
            return false;
        }
    }
}
