using Exiled.API.Interfaces;
using PlayerRoles;
using AnomalousZonePlugin.Classes.Roles;
using System.ComponentModel;
using AnomalousZonePlugin.Configs.SCP294;
using System.Collections.Generic;
using UnityEngine;
using Exiled.API.Enums;
using AnomalousZonePlugin.Classes.Items;
using System.IO;
using System;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using Exiled.Loader;
using YamlDotNet.Serialization;

namespace AnomalousZonePlugin.Configs
{
    public class MainConfig : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;
        [Description("Is debug enabled?")]
        public bool Debug { get; set; } = false;
        [Description("Percent chance of getting Pink Candy from SCP-330")]
        public int PinkChance { get; set; } = 5;
        [Description("Does SCP-173 explode on dying?")]
        public bool ExplodeOnDeath { get; set; } = true;
        [Description("Are SCPs and Flamingos hostile?")]
        public bool Hostile { get; set; } = true;
        [Description("Enable FF when round ends?")]
        public bool RoundEndFriendlyFire { get; set; } = true;
        [Description("Percent chance of coin exploding on flip")]
        public int CoinExplosionChance { get; set; } = 5;
        [Description("Allow SCP-079 to use tesla gates during blackouts")]
        public bool allow079 { get; set; } = false;
        public Kid Kid { get; set; } = new Kid();
        public Captain Captain { get; set; } = new Captain();
        public Supervisor Supervisor { get; set; } = new Supervisor();
        public Dealer Dealer { get; set; } = new Dealer();
        public IdentityThief Thief { get; set; } = new IdentityThief();
        [Description("Identity Thief starter role")]
        public RoleTypeId appearance { get; set; } = RoleTypeId.ClassD;
        public List<AdminGun> AdminGuns { get; private set; } = new()
        {
            new AdminGun(),
        };
        [Description("Time limit for SCP-3114 Strangle ability")]
        public float StrangleLimit { get; set; } = 5f;
        [Description("Cooldown for SCP-3114 Strangle ability")]
        public float StrangleCooldown { get; set; } = 3.5f;
        [Description("Minimum time for Funny Pills disguise(secs)")]
        public float PillsDisguiseMinTime { get; set; } = 20f;
        [Description("Maximum time for Funny Pills disguise(mins)")]
        public float PillsDisguiseMaxTime { get; set; } = 5f;
        [Description("Minimum time for Funny Pills speed effect(secs)")]
        public float PillsSpeedMinTime { get; set; } = 10f;
        [Description("Maximum time for Funny Pills speed effect(secs)")]       
        public float PillsSpeedMaxTime { get; set; } = 60f;
        [Description("Time for nuke radiation to start(mins)")]
        public float TimeUntilRadiation { get; set; } = 5f;
        [Description("Configure the Spawning Locations of SCP-294")]
        public SpawningConfig SpawningLocations { get; set; } = new SpawningConfig()
        {
            SpawnAmount = 1,
            SpawnRooms = new Dictionary<RoomType, List<SpawnTransform>>()
            {
                [RoomType.EzUpstairsPcs] = new List<SpawnTransform>(){
                    new SpawnTransform() {
                        Position = new Vector3(-5.15f, 0f, 2f),
                        Rotation = new Vector3(0f, -90f, 0f),
                        Scale = Vector3.one
                    }
                },
                [RoomType.EzPcs] = new List<SpawnTransform>(){
                    new SpawnTransform() {
                        Position = new Vector3(-7f, 0f, -1.75f),
                        Rotation = new Vector3(0f, -90f, 0f),
                        Scale = Vector3.one
                    },
                    new SpawnTransform() {
                        Position = new Vector3(2.5f, 0f, 6.8f),
                        Rotation = new Vector3(0f, 0f, 0f),
                        Scale = Vector3.one
                    }
                },
                [RoomType.EzDownstairsPcs] = new List<SpawnTransform>(){
                    new SpawnTransform() {
                        Position = new Vector3(7f, -1.5f, -5.8f),
                        Rotation = new Vector3(0f, 90f, 0f),
                        Scale = Vector3.one
                    },
                    new SpawnTransform() {
                        Position = new Vector3(7f, -1.5f, 5.8f),
                        Rotation = new Vector3(0f, 90f, 0f),
                        Scale = Vector3.one
                    }
                }
            }
        };
        [Description("Enable Voice Effects?")]
        public bool EnableVoiceEffects { get; set; } = true;
        [Description("Should players be forced to get a random drink?")]
        public bool ForceRandom { get; set; } = false;
        [Description("How close to the machine does the player have to be?")]
        public float UseDistance { get; set; } = 2.5f;
        [Description("Should the Cola be dispensed into the machine's output?")]
        public bool SpawnInOutput { get; set; } = true;
        [Description("How long should it take from command execution to dispense the drink?")]
        public float DispenseDelay { get; set; } = 5.5f;
        [Description("Cooldown after a player uses the machine.")]
        public float CooldownTime { get; set; } = 10f;
        [Description("Enable to use the Community Made Drinks")]
        public bool EnableCommunityDrinks { get; set; } = true;
        [Description("The maximum uses of a SCP-294 machine before it deactivates. Set to -1 for infinite uses")]
        public int MaxUsesPerMachine { get; set; } = 3;
        [Description("The maximum size a player can grow to from a drink.")]
        public Vector3 MaxSizeFromDrink { get; set; } = new Vector3(1.3f, 1.3f, 1.3f);
        [Description("The minimum size a player can shrink to from a drink.")]
        public Vector3 MinSizeFromDrink { get; set; } = new Vector3(0.7f, 0.7f, 0.7f);
        [Description("The error range for the escapes to trigger")]
        public float ErrorRange { get; set; } = 0.5f;
        [Description("Minimum time for Funny Pills dead chat effect(secs)")]
        public float DeadChatMinTime { get; set; } = 10f;
        [Description("Maximum time for Funny Pills dead chat effect(secs)")]
        public float DeadChatMaxTime { get; set; } = 60f;
        [Description("Minimum time for Funny Pills SCP chat effect(secs)")]
        public float SCPChatMinTime { get; set; } = 10f;
        [Description("Maximum time for Funny Pills SCP chat effect(secs)")]
        public float SCPChatMaxTime { get; set; } = 60f;
        [Description("List of SCP nicknames")]
        public Dictionary<RoleTypeId, List<string>> SCPNicknames { get; set; } = new Dictionary<RoleTypeId, List<string>>
        {
            {
                RoleTypeId.Scp3114, new List<string>
                { 
                    "Skeleton",
                    "Skeletron",
                    "Bones",
                    "Skin Stealer",
                    "Rib-Tickling Comedian"
                } 
            },
            { 
                RoleTypeId.Scp173, new List<string> 
                {
                    "Nut", 
                    "Peanut",
                    "Matthew"
                }
            },
            {
                RoleTypeId.Scp049, new List<string>
                {
                    "Doctor",
                    "Plague Doctor",
                    "Fr*nchman", // Should be censored
                    "AIDs Giver",
                    "Bird Face"
                }            
            },
            {
                RoleTypeId.Scp939, new List<string>
                {
                    "Dog",
                    "Predator",
                    "Good Girl",
                    "Good Dog",
                    "Air Bud",
                    "Caked Up Dog" // This was a suggestion from an admin
                                   // I think his name started at N1bble and ends in head
                }
            },
            {
                RoleTypeId.Scp096, new List<string>
                {
                    "Shy Guy",
                    "White Man",
                    "Skinny White Man",
                    "The Quiet Kid",
                    "Ugly Man"
                }
            },
            {
                RoleTypeId.Scp106, new List<string>
                {
                    "Larry",
                    "Uncle Larry",
                    "Black Man",
                    "Tall Black Man", // Just call him an enderman
                    "Old Man"
                }
            },
            {
                RoleTypeId.Scp0492, new List<string>
                {
                    "Zombie",
                    "AIDs Patient" // 👍
                }
            }
        };
        [Description("Minimum time for Funny Pills jail time effect(secs)")]
        public float JailMinTime { get; set; } = 5f;
        [Description("Minimum time for Funny Pills jail time effect(secs)")]
        public float JailMaxTime { get; set; } = 10f;
        [Description("How quickly for Funny Pills to spin the player in deg/s")]
        public float spinSpeed { get; set; } = 50f;
        [Description("Message to show the player when denined pills")]
        public List<string> deniedPillMessages = new List<string>
        {
            "You don't feel like taking pills right now",
            "It's not a good time to take pills",
            "You feel like you're getting addicted to pills",
            "The pills are expired",
            "You have a bad feeling about taking these"
        };
        [Description("Chance of being denied pills")]
        public float deniedPillChance { get; set; } = .15f;
    }
}