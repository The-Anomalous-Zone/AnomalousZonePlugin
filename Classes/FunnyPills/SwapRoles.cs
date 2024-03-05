using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Enums;
using UnityEngine;
using Exiled.API.Features.Pickups;
using PlayerRoles;
using UnityEngine;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class SwapRoles
    {
        public static void swapRoles(Player player)
        {
            RoleTypeId newRole;
            switch (player.Role.Type)
            {
                case RoleTypeId.Scientist:
                    {
                        newRole = RoleTypeId.ClassD;
                        break;
                    }
                case RoleTypeId.ClassD:
                    {
                        newRole = RoleTypeId.Scientist;
                        break;
                    }
                case RoleTypeId.NtfCaptain:
                    {
                        newRole = RoleTypeId.ChaosRepressor;
                        break;
                    }
                case RoleTypeId.NtfSergeant:
                    {
                        newRole = RoleTypeId.ChaosMarauder;
                        break;
                    }
                case RoleTypeId.NtfPrivate:
                    {
                        newRole = RoleTypeId.ChaosRifleman;
                        break;
                    }
                case RoleTypeId.NtfSpecialist:
                    {
                        newRole = RoleTypeId.ChaosConscript;
                        break;
                    }
                case RoleTypeId.ChaosRepressor:
                    {
                        newRole = RoleTypeId.NtfCaptain;
                        break;
                    }
                case RoleTypeId.ChaosMarauder:
                    {
                        newRole = RoleTypeId.NtfSergeant;
                        break;
                    }
                case RoleTypeId.ChaosConscript:
                    {
                        newRole = RoleTypeId.NtfSpecialist;
                        break;
                    }
                case RoleTypeId.ChaosRifleman:
                    {
                        newRole = RoleTypeId.NtfPrivate;
                        break;
                    }
                case RoleTypeId.FacilityGuard:
                    {
                        newRole = UnityEngine.Random.value switch
                        {
                            < 0.1f => RoleTypeId.Scp0492,
                            < 0.55f => RoleTypeId.ChaosConscript,
                            _ => RoleTypeId.ClassD
                        };
                        break;
                    }

            }
        }
    }
}
