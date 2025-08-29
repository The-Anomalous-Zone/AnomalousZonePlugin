using Exiled.API.Features;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using AnomalousZonePlugin.Classes;
using Exiled.API.Extensions;
using MEC;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class Disguise
    {
        // This actually works well enough that a real dev
        // Asked how I did this
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
            RoleTypeId[] SCPs = new RoleTypeId[]
            {
                RoleTypeId.Scp079,
                RoleTypeId.Scp0492,
                RoleTypeId.Scp049,
                RoleTypeId.Scp096,
                RoleTypeId.Scp3114,
                RoleTypeId.Scp939,
                RoleTypeId.Scp106,
                RoleTypeId.Scp173
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
                    if (UnityEngine.Random.value < .2)
                    {
                        disguise = SCPs.Random();
                    }
                    else
                    {
                        disguise = chaosRoles.Random();
                    }
                    
                    break;
                case RoleTypeId.ClassD:
                case RoleTypeId.ChaosConscript:
                case RoleTypeId.ChaosRifleman:
                case RoleTypeId.ChaosRepressor:
                case RoleTypeId.ChaosMarauder:
                    if (UnityEngine.Random.value < .2)
                    {
                        disguise = SCPs.Random();
                    }
                    else
                    {
                        disguise = foundationRoles.Random();
                    }
                    break;
                default:
                    disguise = SCPs.Random();
                    break;
            }
            player.ShowHint($"<b><color=#0d98ba>You've been disguised as a </color><color={disguise.GetColor().ToHex()}>{disguise.GetFullName()}</color><color=#0d98ba> temporarily!");
            player.ChangeAppearance(disguise);
            Timing.CallDelayed(UnityEngine.Random.Range(Plugin.Instance.Config.PillsDisguiseMinTime, (Plugin.Instance.Config.PillsDisguiseMaxTime * 60)), () =>
            {
                player.ShowHint($"<color=#0d98ba>Your disguise has worn off!</color>");
                player.ChangeAppearance(player.Role.Type);
            });
        }
    }
}