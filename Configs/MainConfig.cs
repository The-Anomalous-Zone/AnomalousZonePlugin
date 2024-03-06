using Exiled.API.Interfaces;
using PlayerRoles;
using AnomalousZonePlugin.Classes.Roles;
using System.ComponentModel;

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
        [Description("Time limit for SCP-3114 Strangle ability")]
        public float StrangleLimit { get; set; } = 5f;
        [Description("Cooldown for SCP-3114 Strangle ability")]
        public float StrangleCooldown { get; set; } = 3.5f;
        [Description("Minimum time for Funny Pills disguise(secs)")]
        public float PillsDisguiseMinTime { get; set; } = 20f;
        [Description("Maximum time for Funny Pills disguise(mins)")]
        public float PillsDisguiseMaxTime { get; set; } = 5f;
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
    }
}
