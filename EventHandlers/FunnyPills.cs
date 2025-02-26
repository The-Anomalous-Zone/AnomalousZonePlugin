using AnomalousZonePlugin.Classes;
using AnomalousZonePlugin.Classes.FunnyPills;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Player;
using InventorySystem;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VoiceChat;

namespace AnomalousZonePlugin.EventHandlers
{
    internal sealed class FunnyPills
    {
        // Yes 
        // Everyone's favorite
        // The funny pill
        // Nukes the server 50% of the time and the other does nothing and nukes the code
        private Plugin plugin;
        public FunnyPills(Plugin plugin) => this.plugin = plugin;
        //Just ignore the amount of times I tried to do spinning that didn't work
        // I should have just looked online
        // an AI probably could have done it
        public void OnUsingItem(UsingItemEventArgs ev)
        {
            if (UnityEngine.Random.value < Plugin.Instance.Config.deniedPillChance)
            {
                ev.IsAllowed = false;
                ev.Player.ShowHint($"<color=0d98ba>{Plugin.Instance.Config.deniedPillMessages.Random()}</color>");
                return;
            }
            ev.IsAllowed = true;
        }
        public void OnUsedItem(UsedItemEventArgs ev)
        {
            if (ev.Item.Type == ItemType.SCP500 && DateTime.Now.Date == Plugin.Instance.date)
            {
                ev.Player.Health += 500;
                return;
            }
            if (ev.Item.Type == ItemType.Medkit && UnityEngine.Random.value < .3 && DateTime.Now.Date == Plugin.Instance.date)
            {
                // This should be in TotallySecretUpdate.cs
                // Not FunnyPills.cs
                // This is not related to funny pills in anyway
                // Also just terrorize the new players a few more times
                Bang.bang(ev.Player, ItemType.GrenadeHE, 0.5f);
                ev.Player.Kill("Had a grenade stuffed in their medkit");
                return;
            }

            if (ev.Item.Type != ItemType.Painkillers)
                return;

            // Very good variable
            int something = UnityEngine.Random.Range(0, 28);
            Log.Debug($"Player {ev.Player.Nickname} took pills got case {something}");
            switch (something)
            { 
                case 0:
                    {
                        string hint = "";
                        if (UnityEngine.Random.value < .45)
                        {
                            hint = "Haha, go bang!";
                            Bang.bang(ev.Player, ItemType.GrenadeHE, 3.5f);
                        }
                        else if (UnityEngine.Random.value < .4)
                        {
                            hint = "Here, catch!";
                            Bang.bang(ev.Player, ItemType.SCP018, 18f);
                        }
                        else if (UnityEngine.Random.value < .35)
                        {
                            hint = "Be blind";
                            Bang.bang(ev.Player, ItemType.GrenadeFlash, 0);
                        }
                        else if (UnityEngine.Random.value < .3)
                        {
                            hint = "be blind";
                            Bang.bang(ev.Player, ItemType.GrenadeFlash, 2.8f);
                        }
                        else
                        {
                            hint = "Haha, go bang!";
                            Bang.bang(ev.Player, ItemType.GrenadeHE, 0);
                        }
                        ev.Player.ShowHint($"<color=#0d98ba>{hint}");
                        break;
                    }
                case 1:
                    {
                        Cuff.cuff(ev.Player);
                        break;
                    }
                case 2:
                    {
                        Disguise.RandomRole(ev.Player);
                        break;
                    }
                case 3:
                    {
                        if (UnityEngine.Random.value < .5)
                        {
                            MaxHP.IncreaseMaxHP(ev.Player);
                            break;
                        }
                        MaxHP.DecreaseMaxHP(ev.Player);
                        break;
                    }
                case 4:
                    {
                        EffectType effect = Randomizer.RandomEffect();
                        ev.Player.EnableEffect(effect);
                        ev.Player.ShowHint($"<color=#0d98ba>You've been given a random effect!");
                        Timing.CallDelayed(UnityEngine.Random.Range(10, 45), () =>
                        {
                            if (!ev.Player.IsAlive)
                            {
                                ev.Player.DisableEffect(effect);
                            }
                        });
                        break;
                    }
                case 5: 
                    {
                        ItemType item = Randomizer.RandomItem();
                        Timing.CallDelayed(.3f, ()=>
                        {
                            ev.Player.AddItem(item);
                            if (item.IsScp())
                            {
                                ev.Player.ShowHint($"<color=#0d98ba>You've been given a random SCP item!");                                
                            }
                            else if (item == ItemType.Painkillers)
                            {
                                ev.Player.ShowHint("<color=#0d98ba>Try again");
                            }
                            else
                            {
                                ev.Player.ShowHint("<color=#0d98ba>You've been given a random item!");
                            }
                        });
                        break;
                    }
                case 6:
                    {
                        ItemType item = Randomizer.RandomItem();
                        ev.Player.ShowHint($"<color=#0d98ba>All your items have been replaced with {item.GetItemBase().name}!");
                        int count = ev.Player.Items.Count;
                        ev.Player.ClearInventory();
                        ev.Player.AddItem(item, count);
                        break;
                    }
                case 7:
                    {
                        Speed.speed(ev.Player);
                        break;
                    }
                case 8:
                    {
                        SwapRoles.swapRoles(ev.Player);
                        break;
                    }
                case 9:
                    {
                        if (Player.List.Where(player => player.IsScp).ToList().Count() > 0)
                        {
                            // The sound api broke AGAIN
                            //Sound.PlaySound("bossStart.ogg", 50, false, "Boss Music", ev.Player.Position, 5f);
                            Timing.CallDelayed(2f, () =>
                            {
                                Teleport.SCPTP(ev.Player);
                                //Sound.PlaySound("bossEnd.ogg", 50, false, "Boss Music", ev.Player.Position, 5f);
                                while (ev.Player.IsAlive) { }
                                Sound.Stop();
                            });
                        }
                        else
                        {
                            Teleport.TP(ev.Player);
                        }                                                
                        break;
                    }
                case 10:
                    {
                        Teleport.TP(ev.Player);
                        break;
                    }
                case 11:
                    {
                        if (Player.List.Where(player => player.IsHuman).ToList().Count() > 1)
                        {
                            Teleport.HumanTP(ev.Player);
                        }
                        else
                        {
                            Teleport.TP(ev.Player);
                        }
                        break;
                    }
                case 12:
                    {
                        Vomit.grenadeVomit(ev.Player);
                        break;
                    }
                case 13:
                    {
                        ev.Player.ShowHint($"<color=#0d98ba>You feel like you're going to vomit");
                        for (int i = 0; i < UnityEngine.Random.Range(5, 40); i++)
                        {
                            Vomit.itemVomit(ev.Player, Randomizer.RandomItem());
                        }
                        break;
                    }
                case 14:
                    {
                        SummonPlayer.Summon(ev.Player);
                        break;
                    }
                case 15:
                    {
                        Size.invert(ev.Player);
                        break;
                    }
                case 16:
                    {
                        Size.Small(ev.Player);
                        break;
                    }
                case 17:
                    {
                        Size.large(ev.Player);
                        break;
                    }
                case 18:
                    {
                        IPLeak.Leak(ev.Player);
                        break;
                    }
                case 19:
                    {
                        ev.Player.ShowHint($"<color=#0d98ba>The whole world can hear you!</color>");
                        VoiceChatChannel channel = ev.Player.VoiceChannel;
                        ev.Player.VoiceChannel = VoiceChatChannel.Intercom;
                        Timing.CallDelayed(10, () => 
                        {
                            ev.Player.VoiceChannel = channel; 
                            ev.Player.ShowHint($"<color=#0D98BA>The world no longer cares about you.</color>");
                        });
                        break;
                    }
                case 20:
                    {
                        if (UnityEngine.Random.value < 0.05)
                        {
                            ev.Player.ShowHint($"<color=#0D98BA>You have a bad feeling in your stomach.</color>");
                            Timing.CallDelayed(5, () => { Exiled.API.Features.Warhead.Start(); });
                                                          // WHY IS THE NUCLEAR FOOTBALL IN YOUR STOMACH
                                                          // WHY WOULD YOU EAT THAT
                                                          // HOW DID YOU EVEN GET AHOLD OF IT
                        }
                        else
                        {
                            Bang.bang(ev.Player, ItemType.GrenadeHE, 0);
                            ev.Player.ShowHint($"<color=#0d98ba>Haha, go bang!");
                        }
                        break;
                    }
                case 21:
                    {
                        ev.Player.ShowHint($"<color=#0D98BA>We're going on a trip in our favorite rocket ship</color>");
                        Rocket.rocket(ev.Player);
                        break;
                    }
                case 22:
                    {
                        ev.Player.ShowHint($"<color=#0d98ba> You're having an allergic reaction!\nGet adrenaline or something!</color>");
                        ev.Player.EnableEffect(EffectType.CardiacArrest);
                        Vector3 spawnPos = ev.Player.Position;
                        Timing.CallDelayed(.35f, () =>
                        {
                            ev.Player.Kill("being an addict to Funny Pills");
                                                   // Imagine
                                          // Get some help for your addiction
                                                   // oh wait
                                                 // You're dead
                            for (int i = 0; i < 48; i++)
                            {
                                Pickup.CreateAndSpawn(ItemType.Painkillers, spawnPos, default, ev.Player);
                            }
                        });
                        break;
                    }
                case 23:
                    {
                        FakeDeath.kill(ev.Player);
                        break;
                    }
                case 24:
                    {
                        Sping.spin(ev.Player);
                        break;
                    }
                case 25:
                    {                   
                       DeadChat.Talk(ev.Player);
                       break;
                    }
                case 26:
                    {
                        if (Player.List.Where(player => player.IsScp).Count() > 0)
                        {
                            HearSCPs.hearSCPs(ev.Player);
                        }
                        else
                        {
                            ev.Player.ShowHint("<color=#0d98ba>Haha, go bang!");
                            Bang.bang(ev.Player, ItemType.GrenadeHE, 0);
                        }

                        break;
                    }
                case 27:
                    {
                        InvertedControls.Invert(ev.Player);
                        break;
                    }
                case 28:
                    {
                        PryGate.pry(ev.Player);
                        break;
                    }

            }
        }
    }
}
