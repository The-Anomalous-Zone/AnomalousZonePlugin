using CommandSystem;
using CustomPlayerEffects;
using Exiled.API.Features;
using Exiled.CreditTags.Features;
using Exiled.Permissions.Extensions;
using MEC;
using RemoteAdmin;
using AnomalousZonePlugin.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Exiled.API.Enums;
using System.IO;
using PlayerRoles;
using System.Threading;
using Exiled.API.Features.Items;

namespace AnomalousZonePlugin.Classes
{
    // This was another level of broken 
    // Also I was trying to something in a testing server
    // Please don't run this in a listed server
    // It will violate VSR 8.1

    //[CommandHandler(typeof(RemoteAdminCommandHandler))]
    //public class StupidCommand : ICommand, IUsageProvider
    //{
    //    public string Command { get; } = "npc";
    //    public string[] Aliases { get; } = { };
    //    public string Description { get; } = "Stupid stuff";
    //    public string[] Usage { get; } = new string[1] { "Name, Role" };
    //    public static int id = 1000;
    //    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string reponse)
    //    {
    //        var player = Player.Get(((PlayerCommandSender)sender).ReferenceHub);
    //        if (!sender.CheckPermission("AnomalousZonePlugin.admin"))
    //        {
    //            reponse = "No access";
    //            return false;
    //        }
    //        Log.Debug("before create npc");
    //        Npc.Spawn(arguments.At(0), Role(arguments.At(1)), id, "", player.Position);
            
    //        Log.Debug("after create npc\nbefore method");
    //        Start(id);
    //        reponse = "new npc";
    //        id++;

    //        return true;

    //    }
    //    public static RoleTypeId Role(string role)
    //    {
    //        return role switch
    //        {
    //            "scientist" => RoleTypeId.Scientist,
    //            "classd" => RoleTypeId.ClassD,
    //            "captain" => RoleTypeId.NtfCaptain,
    //            "private" => RoleTypeId.NtfPrivate,
    //            "sergeant" => RoleTypeId.NtfSergeant,
    //            "guard" => RoleTypeId.FacilityGuard,
    //            "scp939" => RoleTypeId.Scp939,
    //            "scp173" => RoleTypeId.Scp173,
    //            "scp3114" => RoleTypeId.Scp3114,
    //            "scp049" => RoleTypeId.Scp049,
    //            "scp106" => RoleTypeId.Scp106,
    //            "scp096" => RoleTypeId.Scp096,
    //            "chaos" => RoleTypeId.ChaosRifleman,
    //            "repressor" => RoleTypeId.ChaosRepressor,
    //            _ => RoleTypeId.None,
    //        };
    //    }
    //    public static void Start(int id)
    //    {
    //        Timing.CallDelayed(1.5f, ()=>
    //            {
    //            if (Npc.Get(id) != null)
    //                return;
    //            Log.Debug("Start of movement");
    //            while (Npc.Get(id).CurrentRoom.AreLightsOff)
    //            {

    //            }
    //            Timing.CallDelayed(1.5f, () =>
    //            {
    //                if (!Npc.Get(id).IsNPC || Npc.Get(id) == null)
    //                    return;
    //                if (Npc.Get(id).Role.Type == RoleTypeId.ClassD)
    //                {
    //                    Timing.CallDelayed(UnityEngine.Random.value, () =>
    //                    {
    //                        int counter = 0;
    //                        while (counter < 3)
    //                        {
    //                            Npc.Get(id).Position += Vector3.forward * 2f;
    //                            counter++;
    //                        }
    //                        while (Npc.Get(id).IsAlive)
    //                        {
    //                            Npc.Get(id).CurrentItem = Item.Create(ItemType.Painkillers);
    //                            Timing.CallDelayed(1, () =>
    //                            {
    //                                if (!Npc.Get(id).UseItem(ItemType.Painkillers))
    //                                {
    //                                    Npc.Get(id).DropItems();
    //                                }
    //                            });
    //                        }
    //                    });
    //                }
    //                if (Npc.Get(id).Role.Type == RoleTypeId.FacilityGuard)
    //                {
    //                    int counter = 0;
    //                    while (counter < 10)
    //                    {
    //                        Npc.Get(id).CurrentItem = Item.Create(ItemType.GunFSP9);
    //                        Timing.CallDelayed(UnityEngine.Random.value, () =>
    //                        {
    //                            Npc.Get(id).Position += Vector3.forward * 2f;
    //                            //foreach (Player player in Player.List.Where(player  => !player.IsNPC))
    //                            //{
    //                            //    player.PlayGunSound(ItemType.GunFSP9, (byte)counter, 0);
    //                            //}
    //                            Npc.Get(id).UseItem(ItemType.GunFSP9);

    //                            counter++;
    //                        });
    //                    }
    //                }
    //                if (Npc.Get(id).Role.Type == RoleTypeId.Scientist)
    //                {
    //                    //Thread thread = new Thread(() => { Spin(Npc.Get(id)); });
    //                    Timing.CallDelayed(15, () =>
    //                    {
    //                        int counter = 0;
    //                        while (counter < 5)
    //                        {
    //                            Npc.Get(id).CurrentItem = Item.Create(ItemType.MicroHID);
    //                            Timing.CallDelayed(UnityEngine.Random.value, () =>
    //                            {
    //                                Npc.Get(id).Position += Vector3.forward * 2f;
    //                            });
    //                        }
    //                        while (Npc.Get(id).IsAlive)
    //                        {
    //                           // thread.Start();
    //                            Npc.Get(id).UseItem(ItemType.MicroHID);
    //                        }
    //                    });
    //                }
    //            });
    //        });
    //    }
    //    public static void Spin(Player npc)
    //    {
    //        int counter = 0;
    //        Timing.CallDelayed(UnityEngine.Random.value, () =>
    //        {
    //            npc.Rotation = new Quaternion(counter, counter, counter, counter);
    //            counter++;
    //        });
    //    }
    //}
}
