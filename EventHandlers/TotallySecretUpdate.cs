using Exiled.API.Features.Components;
using Exiled.Events.EventArgs.Scp096;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CentralAuth;
using Exiled.API.Features;
using Exiled.API.Enums;
using MEC;
using Mirror;
using PlayerRoles;
using PlayerRoles.RoleAssign;
using AnomalousZonePlugin.Classes.FunnyPills;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp330;
using InventorySystem.Items.Usables.Scp330;
using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Scp173;
using Exiled.Events.EventArgs.Scp939;


namespace AnomalousZonePlugin.EventHandlers
{
                        // Ah yes
    // The perfect name to prevent people from figuring out the april fools update
    internal sealed class TotallySecretUpdate
    {
        private bool charge = false;
        private Dictionary<Player, bool> attack = new();
        private Plugin plugin;
        private bool hurt;
        public TotallySecretUpdate(Plugin plugin) => this.plugin = plugin;
        public void OnCharging(ChargingEventArgs ev)
        {
            // Let's terrorize the new players who have no clue what is happening
            // Make the angry skinny white guy just turn into a missile
            ev.Player.IsGodModeEnabled = true;
            charge = true;
            Timing.CallDelayed(2.5f, () => { charge = false; });
            SpawnGrenade(ev.Player);
        }

        // Had to make this because using a while loop would crash the server
        // I was too lazy to figure out that the Sleep function is under Thread
        // So I made this instead
        private void SpawnGrenade(Player player)
        {
            Bang.bang(player, ItemType.GrenadeHE, 0);
            if (!charge)
            {
                Timing.CallDelayed(.5f, () => { player.IsGodModeEnabled = false; });
                return;
            }
            Timing.CallDelayed(.25f, () => { SpawnGrenade(player); });
        }

        // The insanity of being peanut and having every kill blow up drove me crazy
        public void OnDying(DyingEventArgs ev)
        {
            ev.Player.DisableAllEffects();
            if (ev.Player.Role.Type == RoleTypeId.Scp173)
            {
                for (int i = 0; i < 15; i++)
                {
                    Bang.bang(ev.Player, ItemType.GrenadeHE, 0);
                }
            }
            if (UnityEngine.Random.value < .4)
            {
                Bang.bang(ev.Player, ItemType.GrenadeHE, 0);
            }
        }
        public void OnSpawning(SpawnedEventArgs ev)
        {
            // Yes
            // Jumpscare zombie (Can't hit due to speed)
            if (ev.Player.Role.Type == RoleTypeId.Scp0492)
            {
                ev.Player.Health = 2000;
                ev.Player.EnableEffect(EffectType.SilentWalk);
                ev.Player.EnableEffect(EffectType.MovementBoost);
                ev.Player.ChangeEffectIntensity(EffectType.MovementBoost, 255);
            }
            if (ev.Player.Role.Team == Team.SCPs)
            {
                // Confuse the players even more
                Timing.CallDelayed(1.5f, () =>
                {
                    switch (UnityEngine.Random.Range(0, 6))
                    {
                        case 0:
                            {
                                ev.Player.ChangeAppearance(RoleTypeId.Scp049);
                                break;
                            }
                        case 1:
                            {
                                ev.Player.ChangeAppearance(RoleTypeId.Scp0492);
                                break;
                            }
                        case 2:
                            {
                                ev.Player.ChangeAppearance(RoleTypeId.Scp096);
                                break;
                            }
                        case 3:
                            {
                                ev.Player.ChangeAppearance(RoleTypeId.Scp106);
                                break;
                            }
                        case 4:
                            {
                                ev.Player.ChangeAppearance(RoleTypeId.Scp173);
                                break;
                            }
                        case 5:
                            {
                                ev.Player.ChangeAppearance(RoleTypeId.Scp939);
                                break;
                            }
                        case 6:
                            {
                                ev.Player.ChangeAppearance(RoleTypeId.Scp3114);
                                break;
                            }
                    }
                });
            }
        }
        public void OnEatingCandy(EatingScp330EventArgs ev)
        {
            // Turn pink candy into gambling mechanic
            if (ev.Candy.Kind != CandyKindID.Pink)
                return;
            ev.Player.IsGodModeEnabled = true;
            attack.Add(ev.Player, true);
            Timing.CallDelayed(3, () =>
            {
                attack.Remove(ev.Player);
                ev.Player.IsGodModeEnabled = false;
            });
            if (UnityEngine.Random.value < .5)
            {
                hurt = true;
            }
            else
            {
                hurt = false;
            }
        }

        public void OnHurting(HurtingEventArgs ev)
        {
            if (attack.ContainsKey(ev.Attacker))
            {
                if (!hurt)
                {
                    ev.IsAllowed = false;
                    ev.Player.Health += ev.Amount;
                }
            }
        }
        public void OnusedItem(UsedItemEventArgs ev)
        {
            // Make 500 lore accurate
            if(ev.Item.Type != ItemType.SCP500)
                return;
            ev.Player.Health += 500;
        }
        public void OnBlinking(BlinkingEventArgs ev)
        {
            // Peanut's really going vroooom
            ev.BlinkCooldown = 0.25f;
        }
        public void OnPounce(LungingEventArgs ev)
        {
            // Just terrorize the new players even more
            // Hmm why is dog thing behind the door
            // oh god OHH
            // IT EXPLOED
            // WHAT IS GAME????!?!?!?!
            ev.Player.IsGodModeEnabled = true;
            Bang.bang(ev.Player, ItemType.GrenadeHE, 0);
            Timing.CallDelayed(1.5f, () => { ev.Player.IsGodModeEnabled = false; });
        }
    }
}
