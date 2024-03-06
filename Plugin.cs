using AnomalousZonePlugin.Configs;
using AnomalousZonePlugin.Configs.SCP294;
using AnomalousZonePlugin.EventHandlers;
using AnomalousZonePlugin.EventHandlers.SCP294Handlers;
using AnomalousZonePlugin.Classes.Roles;
using AnomalousZonePlugin.Classes.SCP294;
using System;
using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;
using SCP3114 = Exiled.Events.Handlers.Scp3114;
using SCP079 = Exiled.Events.Handlers.Scp079;
using SCP330 = Exiled.Events.Handlers.Scp330;
using HarmonyLib;
using MEC;
using System.Collections.Generic;
using MapEditorReborn.API.Features.Objects;
using Exiled.CustomRoles.API.Features;
using Exiled.CustomRoles.API;

namespace AnomalousZonePlugin
{
    public class Plugin : Plugin<MainConfig>
    {
        public override string Name => "Anomalous Zone Plugin";
        public override string Author => "Logan \"coollogan876\"";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion = new Version(8, 6, 0);
        public static Plugin Instance { get; private set; }

        public Dictionary<SchematicObject, bool> SpawnedSCP294s { get; set; } = new Dictionary<SchematicObject, bool>();
        public Dictionary<SchematicObject, int> SCP294UsesLeft { get; set; } = new Dictionary<SchematicObject, int>();
        public Dictionary<SchematicObject, LightSourceObject> lightSource { get; set; } = new Dictionary<SchematicObject, LightSourceObject>();
        public List<string> PlayersNear294 { get; set; } = new List<string>();
        public Dictionary<ushort, DrinkInfo> CustomDrinkItems { get; set; } = new Dictionary<ushort, DrinkInfo>();
        public DrinkManager DrinkManager = new DrinkManager();
        public Dictionary<string, float> PlayerVoicePitch = new Dictionary<string, float>();

        private TeslaBlackouts blackouts;
        private Subclasses subclasses;
        private PlayerHandler scp294Player;
        private ServerHandler scp294Server;
        private RemoteKeycard keycard;
        private NNN NNN;
        private FunnyPills pills;
        private PeanutExplodes peanut;
        private Coin coin;
        private ReplacePlayer replace;
        private EndRoundEvents EndRoundEvents;
        private Candy Candy;
        private Harmony Harmony;
        private SkeletonNerf Nerf;
        private CoroutineHandle coroutineHandle;
        public Dictionary<ReferenceHub, OpusComponent> Encoders = new Dictionary<ReferenceHub, OpusComponent>();

        public override void OnEnabled()
        {
            Instance = this;
            blackouts = new TeslaBlackouts(this);
            subclasses = new Subclasses(this);
            scp294Server = new ServerHandler(this);
            scp294Player = new PlayerHandler(this);
            keycard = new RemoteKeycard(this);
            NNN = new NNN(this);
            pills = new FunnyPills(this);
            peanut = new PeanutExplodes(this);
            coin = new Coin(this);
            replace = new ReplacePlayer(this);
            EndRoundEvents = new EndRoundEvents(this);
            Candy = new Candy(this);
            Nerf = new SkeletonNerf(this);
            keepEffects = new KeepPlayerEffects(this);

            // Register Tesla Blackouts events
            SCP3114.Disguising += blackouts.OnDisguising;
            SCP3114.Revealing += blackouts.OnRevealing;
            SCP079.InteractingTesla += blackouts.OnInteractingTesla;
            SCP079.RoomBlackout += blackouts.OnRoomBlackout;
            SCP079.ZoneBlackout += blackouts.OnZoneBlackout;
            Player.TriggeringTesla += blackouts.OnTriggeringTesla;

            // Register SubClasses roles
            Instance.Config.Kid.Register();
            Instance.Config.Supervisor.Register();
            Instance.Config.Captain.Register();
            Instance.Config.Dealer.Register();
            Instance.Config.Thief.Register();
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

            // Register RemoteKeycard events
            Player.InteractingDoor += keycard.OnDoorInteract;
            Player.UnlockingGenerator += keycard.OnGeneratorUnlock;
            Player.InteractingLocker += keycard.OnLockerInteract;
            Player.ActivatingWarheadPanel += keycard.OnWarheadUnlock;

            // Register NNN events
            Player.ChangingRole += NNN.OnChangingRole;

            // Register Funny Pills events
            Player.UsedItem += OnUsedItem;

            // Register Peanut Explode events
            Player.Dying += peanut.OnDying;

            // Register End Round events
            Server.RoundEnded += EndRoundEvents.OnRoundEnding;
            Server.RoundEnded += EndRoundEvents.OnRoundEnded;
            Server.WaitingForPlayers += EndRoundEvents.OnWaitingForPlayers;

            // Register Exploding coin flips events
            Player.FlippingCoin += coin.OnFlippingCoin;

            // Register player disconnection events
            Player.Left += replace.OnLeft;

            // Register candy events
            SCP330.InteractingScp330 += Candy.OnInteractingScp330;

            // Register Skeleton events
            Player.Hurting += Nerf.OnHurting;

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
            EndRoundEvents =  null;
            Candy = null;
            Nerf = null;

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
            Server.RoundStarted -= subclasses.OnRoundStarted;
            Player.TriggeringTesla -= subclasses.OnTriggeringTesla;

            // Unregister SCP294 events
            Server.RoundStarted -= scp294Server.WaitingForPlayers;
            Player.ChangingItem -= scp294Player.ChangingItem;
            Player.UsedItem -= scp294Player.UsedItem;
            Player.Joined -= scp294Player.Joined;
            DrinkManager.UnloadAllDrinks;
            Timing.KillCoroutines(coroutineHandle);            

            // Unregister RemoteKeycard events
            Player.InteractingDoor -= keycard.OnDoorInteract;
            Player.UnlockingGenerator -= keycard.OnGeneratorUnlock;
            Player.InteractingLocker -= keycard.OnLockerInteract;
            Player.ActivatingWarheadPanel -= keycard.OnWarheadUnlock;

            // Unregister NNN events
            Player.ChangingRole -= NNN.OnChangingRole;

            // Unregister Funny Pills events
            Player.UsedItem -= OnUsedItem;

            // Unregister Peanut Explode events
            Player.Dying -= peanut.OnDying;

            // Unregister Ending Round events
            Server.RoundEnded -= EndRoundEvents.OnRoundEnding;
            Server.RoundEnded -= EndRoundEvents.OnRoundEnded;
            Server.WaitingForPlayers -= EndRoundEvents.OnWaitingForPlayers;

            // Unregister Exploding coin flips events
            Player.FlippingCoin -= coin.OnFlippingCoin;

            // Unregister player disconnection events
            Player.Left -= replace.OnLeft;

            // Unregister candy events
            SCP330.InteractingScp330 -= Candy.OnInteractingScp330;

            // Unregister Skeleton events
            Player.Hurting -= Nerf.OnHurting;

            base.OnDisabled();
        }
    }
}
