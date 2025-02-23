using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerRoles;
using AnomalousZonePlugin.Classes;

namespace AnomalousZonePlugin.Classes
{
    public class RandomPlayer
    {
        public static Player GetRandomPlayer(Team team)
        {
            switch (team)
            {
                case Team.SCPs:
                    {
                        return Player.List.Where(player => player.Role.Team == Team.SCPs).Random();
                    }
                case Team.Dead:
                    {
                        return Player.List.Where(player => player.Role.Team == Team.Dead).Random();
                    }
                default:
                    {
                        return Player.List.Where(player => player.Role.Team == Team.Dead).Random();
                    }
            }
        }
        public static Player GetRandomPlayer()
        {
            return Player.List.Where(player => player.IsAlive && !player.IsScp && player.Role.Type == RoleTypeId.Tutorial).Random();
        }
    }
}
