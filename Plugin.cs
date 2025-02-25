using AnomalousZonePlugin.Configs;
using AnomalousZonePlugin.Configs.SCP294;
using AnomalousZonePlugin.EventHandlers;
using AnomalousZonePlugin.Classes.Roles;
using AnomalousZonePlugin.Classes.SCP294;
using System;
using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;
using SCP3114 = Exiled.Events.Handlers.Scp3114;
using SCP079 = Exiled.Events.Handlers.Scp079;
using SCP330 = Exiled.Events.Handlers.Scp330;
using Cassie = Exiled.Events.Handlers.Cassie;
using SCP096 = Exiled.Events.Handlers.Scp096;
using Warhead = Exiled.Events.Handlers.Warhead;
using SCP173 = Exiled.Events.Handlers.Scp173;
using SCP939 = Exiled.Events.Handlers.Scp939;
using HarmonyLib;
using MEC;
using System.Collections.Generic;
using MapEditorReborn.API.Features.Objects;
using Exiled.CustomRoles.API.Features;
using Exiled.CustomRoles.API;
using Map = Exiled.Events.Handlers.Map;
using Exiled.CustomItems.API;
using CustomItems.Configs;
using AnomalousZonePlugin.Classes;
using AnomalousZonePlugin.EventHandlers.SCP294;
using AnomalousZonePlugin.EventHandlers.SCP294Handlers;
using VoiceChat;
using AnomalousZonePlugin.Classes.FunnyPills;

// SCP-294 was orginally made by Creepycats here: https://github.com/creepycats/Ultimate294
// I've made some changes (sounds, drinks, text, ect)

namespace AnomalousZonePlugin
{
    public class Plugin : Plugin<MainConfig>
    {
        public override string Name => "Anomalous Zone Plugin";
        public override string Author => "Logan \"coollogan876\" Dresel";
                                                      //Many builds
        public override Version Version => new Version(6, 3, 8);
        public static Plugin Instance { get; private set; }

        public Dictionary<Exiled.API.Features.Player, VoiceChatChannel> channels { get; set; } = new Dictionary<Exiled.API.Features.Player, VoiceChatChannel>();
        public Dictionary<SchematicObject, bool> spawned { get; set; } = new Dictionary<SchematicObject, bool>();
        public Dictionary<SchematicObject, bool> SpawnedSCP294s { get; set; } = new Dictionary<SchematicObject, bool>();
        public Dictionary<SchematicObject, int> SCP294UsesLeft { get; set; } = new Dictionary<SchematicObject, int>();
        public Dictionary<SchematicObject, LightSourceObject> lightSource { get; set; } = new Dictionary<SchematicObject, LightSourceObject>();
        public List<string> PlayersNear294 { get; set; } = new List<string>();
        public Dictionary<ushort, DrinkInfo> CustomDrinkItems { get; set; } = new Dictionary<ushort, DrinkInfo>();
        public DrinkManager DrinkManager = new DrinkManager();
        public Dictionary<string, float> PlayerVoicePitch = new Dictionary<string, float>();
        public DateTime date = new DateTime(DateTime.Now.Year, 4, 1);
        public Dictionary<Exiled.API.Features.Player, bool> canPryGates = new();
        public Dictionary<ReferenceHub, OpusComponent> Encoders = new Dictionary<ReferenceHub, OpusComponent>();

        private TeslaBlackouts blackouts;
        private Subclasses subclasses;
        private RemoteKeycard keycard;
        private NNN NNN;
        private FunnyPills pills;
        private PeanutExplodes peanut;
        private serverHandler scp294Server;
        private playerHandler scp294Player;
        private Coin coin;
        private ReplacePlayer replace;
        private EndRoundEvents EndRoundEvents;
        private ModdedVoiceChat voiceChat;
        private Candy Candy;
        private Harmony Harmony;
        private SkeletonNerf Nerf;
        private KeepPlayerEffects keepEffects;
        private ImprovedCassie iCassie;
        private Radiation radiation;
        private Vaporize vaporize;
        private CoroutineHandle coroutineHandle;
        private TotallySecretUpdate secretUpdate;
        private MoreEscapes escape;
        private redirect redirect;
        private ReallyStupidStuff reallyStupid;
        private PryGate prygate;

        public override void OnEnabled()
        {
            Instance = this;
            blackouts = new TeslaBlackouts(this);
            subclasses = new Subclasses(this);
            keycard = new RemoteKeycard(this);
            NNN = new NNN(this);
            pills = new FunnyPills(this);
            peanut = new PeanutExplodes(this);
            scp294Server = new serverHandler();
            scp294Player = new playerHandler();
            coin = new Coin(this);
            replace = new ReplacePlayer(this);
            EndRoundEvents = new EndRoundEvents(this);
            voiceChat = new ModdedVoiceChat(this);
            Candy = new Candy(this);
            Nerf = new SkeletonNerf(this);
            keepEffects = new KeepPlayerEffects(this);
            iCassie = new ImprovedCassie(this);
            radiation = new Radiation(this);
            vaporize = new Vaporize(this);
            secretUpdate = new TotallySecretUpdate(this);
            escape = new MoreEscapes(this);
            redirect = new redirect(this);        
            reallyStupid = new ReallyStupidStuff(this);
            prygate = new PryGate(this);

            Player.Hurting += reallyStupid.OnHurting;
            Player.Dying += reallyStupid.OnDying;

            //Player.Spawned += stupid.OnSpawned;
            if (DateTime.Now.Date == date)
            {
                SCP096.Charging += secretUpdate.OnCharging;
                Player.Dying += secretUpdate.OnDying;
                Player.Spawned += secretUpdate.OnSpawning;
                SCP330.EatingScp330 += secretUpdate.OnEatingCandy;
                Player.Hurting += secretUpdate.OnHurting;
                SCP173.Blinking += secretUpdate.OnBlinking;
                SCP939.Lunging += secretUpdate.OnPounce;
            }
            else
            {
                Player.Dying += peanut.OnDying;
            }

            //Player.Spawned += escape.OnSpawned;
            Player.Handcuffing += escape.OnCuffed;

            //Player.UsedItem += secretUpdate.OnusedItem;
            // Register Tesla Blackouts events
            SCP3114.Disguising += blackouts.OnDisguising;
            SCP3114.Revealing += blackouts.OnRevealing;
            SCP079.InteractingTesla += blackouts.OnInteractingTesla;
            SCP079.RoomBlackout += blackouts.OnRoomBlackout;
            SCP079.ZoneBlackout += blackouts.OnZoneBlackout;
            Player.TriggeringTesla += blackouts.OnTriggeringTesla;

            // Register SubClasses roles
            Timing.CallDelayed(.5f, () =>
            {
                while (Instance == null)
                {

                }
                Instance.Config.Kid.Register();
                Instance.Config.Supervisor.Register();
                Instance.Config.Captain.Register();
                Instance.Config.Dealer.Register();
                Instance.Config.Thief.Register();
                Instance.Config.AdminGuns.Register();
            });
            // SubClasses events
            Player.TriggeringTesla += subclasses.OnTriggeringTesla;

            // Register SCP294 events
            Server.RoundStarted += scp294Server.WaitingForPlayers;
            Player.ChangingItem += scp294Player.ChangingItem;
            Player.UsedItem += scp294Player.UsedItem;
            Player.Joined += scp294Player.Joined;
            DrinkManager.LoadBaseDrinks();
            coroutineHandle = Timing.RunCoroutine(SCP294Object.Handle294Hint());
            Harmony = new Harmony("SCP294");
            Harmony.PatchAll();

            // Register prygate events
            Player.InteractingDoor += prygate.openDoors;

            // Register RemoteKeycard events
            Player.InteractingDoor += keycard.OnDoorInteract;
            Player.UnlockingGenerator += keycard.OnGeneratorUnlock;
            Player.InteractingLocker += keycard.OnLockerInteract;
            Player.ActivatingWarheadPanel += keycard.OnWarheadUnlock;

            // Register NNN events
            Player.ChangingRole += NNN.OnChangingRole;

            // Register Funny Pills events
            Player.UsedItem += pills.OnUsedItem;

            // Register Peanut Explode events
            

            // Register End Round events
            Server.RoundEnded += EndRoundEvents.OnRoundEnded;
            Server.WaitingForPlayers += EndRoundEvents.OnWaitingForPlayers;
            Server.RoundStarted += EndRoundEvents.OnRoundStart;

            // Register voice chat events
            Player.VoiceChatting += voiceChat.OnPlayerUsingVoiceChat;

            // Register Exploding coin flips events
            Player.FlippingCoin += coin.OnFlippingCoin;

            // Register player disconnection events
            Player.Left += replace.OnLeft;

            // Register candy events
            SCP330.InteractingScp330 += Candy.OnInteractingScp330;

            // Register Skeleton events
            Player.Hurting += Nerf.OnHurting;

            // Register Cassie events
            Map.AnnouncingDecontamination += iCassie.OnAnnoucingDecontaim;
            Map.AnnouncingNtfEntrance += iCassie.OnAnnouncingMTF;
            Map.AnnouncingScpTermination += iCassie.OnAnnouncingSCP;

            // Register Radiation events
            //Warhead.Detonating += radiation.OnNuke;
            //Player.Spawning += radiation.OnSpawning;

            //Player.ChangingRole += keepEffects.OnChangingRole;
            //Player.Joined += redirect.onJoin;
            //Player.ReceivingEffect += keepEffects.OnGainingEffect;
            //Player.Died += keepEffects.OnDying;

            Player.Hurting += vaporize.OnHurting;
          

          
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            blackouts = null;
            subclasses = null;
            scp294Server = null;
            scp294Player = null;
            keycard = null;
            NNN = null;
            pills = null;
            peanut = null;
            coin = null;
            replace = null;
            EndRoundEvents = null;
            voiceChat = null;
            Candy = null;
            Nerf = null;
            vaporize = null;
            secretUpdate = null;    
            escape = null;
            redirect = null;
            reallyStupid = null;
            prygate = null;

            Player.Hurting -= reallyStupid.OnHurting;
            Player.Dying -= reallyStupid.OnDying;

            //Player.Joined -= redirect.onJoin;

            //Player.Spawned -= escape.OnSpawned;
            Player.Handcuffing -= escape.OnCuffed;

            if (DateTime.UtcNow.Date == date)
            {
                SCP096.Charging -= secretUpdate.OnCharging;
                Player.Dying -= secretUpdate.OnDying;
                Player.Spawned -= secretUpdate.OnSpawning;
                SCP330.EatingScp330 -= secretUpdate.OnEatingCandy;
                Player.Hurting -= secretUpdate.OnHurting;
                SCP173.Blinking -= secretUpdate.OnBlinking;
                //Player.UsedItem -= secretUpdate.OnusedItem;
                //Player.Spawned -= stupid.OnSpawned;
                SCP939.Lunging -= secretUpdate.OnPounce;
            }           

            // Unregister Tesla Blackouts events
            SCP3114.Disguising -= blackouts.OnDisguising;
            SCP3114.Revealing -= blackouts.OnRevealing;
            SCP079.InteractingTesla -= blackouts.OnInteractingTesla;
            SCP079.RoomBlackout -= blackouts.OnRoomBlackout;
            SCP079.ZoneBlackout -= blackouts.OnZoneBlackout;
            Player.TriggeringTesla -= blackouts.OnTriggeringTesla;

            // Unregister SubClasses roles
            CustomRole.UnregisterRoles();

            // Unregister SubClasses events
            Player.TriggeringTesla -= subclasses.OnTriggeringTesla;

            // Unregister SCP294 events
            Server.RoundStarted -= scp294Server.WaitingForPlayers;
            Player.ChangingItem -= scp294Player.ChangingItem;
            Player.UsedItem -= scp294Player.UsedItem;
            Player.Joined -= scp294Player.Joined;
            DrinkManager.UnloadAllDrinks();
            Timing.KillCoroutines(coroutineHandle);

            // Unregister prygate events
            Player.InteractingDoor += prygate.openDoors;

            // Unregister RemoteKeycard events
            Player.InteractingDoor -= keycard.OnDoorInteract;
            Player.UnlockingGenerator -= keycard.OnGeneratorUnlock;
            Player.InteractingLocker -= keycard.OnLockerInteract;
            Player.ActivatingWarheadPanel -= keycard.OnWarheadUnlock;

            // Unregister NNN events
            Player.ChangingRole -= NNN.OnChangingRole;

            // Unregister Funny Pills events
            Player.UsedItem -= pills.OnUsedItem;

            // Unregister Peanut Explode events
            Player.Dying -= peanut.OnDying;

            // Unregister Ending Round events
            Server.RoundEnded -= EndRoundEvents.OnRoundEnded;
            Server.WaitingForPlayers -= EndRoundEvents.OnWaitingForPlayers;
            Server.RoundStarted -= EndRoundEvents.OnRoundStart;

            // Unregister voice chat 
            Player.VoiceChatting -= voiceChat.OnPlayerUsingVoiceChat;

            // Unregister Exploding coin flips events
            Player.FlippingCoin -= coin.OnFlippingCoin;

            // Unregister player disconnection events
            Player.Left -= replace.OnLeft;

            // Unregister candy events
            SCP330.InteractingScp330 -= Candy.OnInteractingScp330;

            // Unregister Skeleton events
            Player.Hurting -= Nerf.OnHurting;

            // Unregister Cassie events
            Map.AnnouncingDecontamination -= iCassie.OnAnnoucingDecontaim;
            Map.AnnouncingNtfEntrance -= iCassie.OnAnnouncingMTF;
            Map.AnnouncingScpTermination -= iCassie.OnAnnouncingSCP;

            // Unregister Radiation events
            Warhead.Detonating -= radiation.OnNuke;
            Player.Spawning -= radiation.OnSpawning;

           // Player.ChangingRole -= keepEffects.OnChangingRole;
            //Player.Joined -= keepEffects.OnJoining;
           // Player.ReceivingEffect -= keepEffects.OnGainingEffect;
           // Player.Died -= keepEffects.OnDying;

            Player.Hurting -= vaporize.OnHurting;

            base.OnDisabled();
        }
    }
}