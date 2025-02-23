using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerRoles;
using Exiled.CustomRoles.API;
using AnomalousZonePlugin.Classes.MoreEscapes;
using MEC;
using UnityEngine;
using System.Linq.Expressions;
using Exiled.API.Features;
using CustomPlayerEffects;
using Interactables.Interobjects;

namespace AnomalousZonePlugin.EventHandlers
{
    public class MoreEscapes
    {
        private Plugin plugin;
        public MoreEscapes(Plugin plugin) => this.plugin = plugin;
        private RoleTypeId newRole;

        // Why are there this many spaces
        // ¯\_(ツ)_/¯

        public void UsingElevator(InteractingElevatorEventArgs ev)
        {




            








            //Timing.RunCoroutine((IEnumerator<float>)GuardCoroutine(ev.Player));
            
        }

        public void OnCuffed(HandcuffingEventArgs ev)
        {
            if (ev.Target.Role.Team != Team.FoundationForces && ev.Target.Role.Team != Team.ChaosInsurgency)
            {
                return;
            }

            while (ev.Target.IsCuffed)
            {           
                if (PositionChecker.Check(ev.Target))
                {
                    switch (ev.Target.Role.Type)
                    {
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
                        case RoleTypeId.ChaosRifleman:
                            {
                                newRole = RoleTypeId.NtfPrivate;
                                break;
                            }
                        case RoleTypeId.ChaosConscript:
                            {
                                newRole = RoleTypeId.NtfSpecialist;
                                break;
                            }
                        case RoleTypeId.FacilityGuard:
                            {
                                newRole = RoleTypeId.ChaosRifleman;
                                break;
                            }
                        default:
                            {
                                newRole = RoleTypeId.Scp0492;
                                break;
                            }
                    }

                    ev.Target.Role.Set(newRole, Exiled.API.Enums.SpawnReason.Escaped, RoleSpawnFlags.All);
                }
                else
                {
                    Timing.WaitForSeconds(.25f);
                }
            }
        }

        private IEnumerable<float> GuardCoroutine(Player player)
        {
            // We're server gambling today!!!!
            // Either you become MTF or the server fucking dies
            while (true)
            {
                if (PositionChecker.Check(player))
                {
                    player.Role.Set(RoleTypeId.NtfSpecialist, Exiled.API.Enums.SpawnReason.Escaped, RoleSpawnFlags.All);
                    yield break;
                }
                yield return Timing.WaitForSeconds(.25f);
            }
        }
    }
}
