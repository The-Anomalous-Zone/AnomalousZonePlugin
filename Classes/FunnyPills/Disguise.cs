using Exiled.API.Features;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using AnomalousZonePlugin.Classes;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class Disguise
    {
        public static void RandomRole(Player player)
        {
            RoleTypeId[] foundationRoles = new RoleTypeId[] 
            {
                RoleTypeId.Scientist,
                RoleTypeId.FacilityGuard,
                RoleTypeId.NtfSergeant,
                RoleTypeId.NtfCaptain
            };

            RoleTypeId[] chaosRoles = new RoleTypeId[]
            {
                RoleTypeId.ClassD,
                RoleTypeId.ChaosRifleman,
                RoleTypeId.ChaosRepressor,
                RoleTypeId.ChaosMarauder
            };

        RoleTypeId disguise;
            switch (player.Role.Type)
            {
                case RoleTypeId.Scientist:
                case RoleTypeId.FacilityGuard:
                case RoleTypeId.NtfPrivate:
                case RoleTypeId.NtfSergeant:
                case RoleTypeId.NtfSpecialist:
                case RoleTypeId.NtfCaptain:
                    disguise = chaosRoles.Random();
                    break;
                case RoleTypeId.ClassD:
                case RoleTypeId.ChaosConscript:
                case RoleTypeId.ChaosRifleman:
                case RoleTypeId.ChaosRepressor:
                case RoleTypeId.ChaosMarauder:
                    disguise = foundationRoles.Random();
                    break;
                default:
                    if (UnityEngine.Random.value < 0.7)
                    {
                        disguise = foundationRoles.Random();
                    }
                    else
                    {
                        disguise = chaosRoles.Random();
                    }
                break;
            }
        }
    }
}
