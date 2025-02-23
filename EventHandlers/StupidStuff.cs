using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnomalousZonePlugin.Classes;
using Exiled.API.Features;
using PlayerRoles;
using UnityEngine;
using Exiled.Events.EventArgs.Player;
using Exiled.API.Features.Items;
using System.Reflection;
using System.Threading;

namespace AnomalousZonePlugin.EventHandlers
{
    // That name doesn't do justice for what I was attempting
    // What was I attempting
    // ¯\_(ツ)_/¯
    internal sealed class StupidStuff
    {
        private Plugin plugin;
        public StupidStuff(Plugin plugin) => this.plugin = plugin;
        public void OnSpawned(SpawnedEventArgs ev)
        {
            // Please don't question my programming after reading this
            // I was probably writing this after spending 11 hours
            // at a robotics competition with 4 hours of sleep
            if (ev.Player == null)
                return;
            Timing.CallDelayed(5f, () =>
            {
                while (ev.Player.CurrentRoom.AreLightsOff)
                {

                }
                Timing.CallDelayed(1.5f, () =>
                {
                    if (!ev.Player.IsNPC || ev.Player == null)
                        return;
                    if (ev.Player.Role.Type == RoleTypeId.ClassD)
                    {
                        Timing.CallDelayed(UnityEngine.Random.value, () =>
                        {
                            int counter = 0;
                            while (counter < 3)
                            {
                                ev.Player.Position += Vector3.forward * 2f;
                                counter++;
                            }
                            while (ev.Player.IsAlive)
                            {
                                ev.Player.CurrentItem = Item.Create(ItemType.Painkillers);
                                Timing.CallDelayed(1, () =>
                                {
                                    if (!ev.Player.UseItem(ItemType.Painkillers))
                                    {
                                        ev.Player.DropItems();
                                    }
                                });
                            }
                        });
                    }
                    if (ev.Player.Role.Type == RoleTypeId.FacilityGuard)
                    {
                        int counter = 0;
                        while (counter < 10)
                        {
                            ev.Player.CurrentItem = Item.Create(ItemType.GunFSP9);
                            Timing.CallDelayed(UnityEngine.Random.value, () =>
                            {
                                ev.Player.Position += Vector3.forward * 2f;
                                //foreach (Player player in Player.List.Where(player  => !player.IsNPC))
                                //{
                                //    player.PlayGunSound(ItemType.GunFSP9, (byte)counter, 0);
                                //}
                                ev.Player.UseItem(ItemType.GunFSP9);

                                counter++;
                            });
                        }
                    }
                    if (ev.Player.Role.Type == RoleTypeId.Scientist)
                    {
                        Thread thread = new Thread(() => { spin(ev.Player); });
                        Timing.CallDelayed(15, () =>
                        {
                            int counter = 0;
                            while (counter < 5)
                            {
                                ev.Player.CurrentItem = Item.Create(ItemType.MicroHID);
                                Timing.CallDelayed(UnityEngine.Random.value, () =>
                                {
                                    ev.Player.Position += Vector3.forward * 2f;
                                });
                            }
                            while (ev.Player.IsAlive)
                            {
                                thread.Start();
                                ev.Player.UseItem(ItemType.MicroHID);
                            }
                        });
                    }
                });
            });
        }

        public static void spin(Player npc)
        {
            // This doesn't even work and I didn't even want to try

            int counter = 0;
            Timing.CallDelayed(UnityEngine.Random.value, () =>
            {
                npc.Rotation = new Quaternion(counter, counter, counter, counter);
                counter++;
            });
        }
    }
}
