using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using CustomRoles.API;
using Exiled.Events.EventArgs.Player;
using MapGeneration;
using PlayerRoles;
using System.Collections.Generic;
using System.Linq;
using Player = Exiled.Events.Handlers.Player;
using Map = PluginAPI.Core.Map;
using Exiled.API.Features.Doors;
using Interactables.Interobjects.DoorUtils;

namespace AnomalousZonePlugin.Classes.Roles
{
    [CustomRole(RoleTypeId.FacilityGuard)]
    public class Captain : CustomRole, ICustomRole
    {
        // This is the one role that has 
        // no lore
        // no bugs
        // no issues
        // it's just normal
        public override uint Id { get; set; } = 2;
        public override RoleTypeId Role { get; set; } = RoleTypeId.FacilityGuard;
        public override int MaxHealth { get; set; } = 100;
        public override string Name { get; set; } = "Guard Captain";
        public override string Description { get; set; } = "You receive better items than your fellow guards due to your experience at the Foundation";
        public override string CustomInfo { get; set; } = "Guard Captain";
        public override float SpawnChance { get; set; } = 45f;
        public override List<string> Inventory { get; set; } = new List<string>
        {
            $"{ItemType.ArmorLight}",
            $"{ItemType.Radio}",
            $"{ItemType.GrenadeFlash}",
            $"{ItemType.GunCrossvec}",
            $"{ItemType.KeycardMTFPrivate}",
            $"{ItemType.Medkit}"
        };
        public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new Dictionary<AmmoType, ushort>()
        {
            { AmmoType.Nato9, 75},
            { AmmoType.Nato556, 45 }
        };
        public StartTeam StartTeam { get; set; } = StartTeam.Guard;
        public int Chance { get; set; } = 45;

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            Limit = 1,
        };

        protected override void SubscribeEvents()
        {
            Player.Spawning += OnSpawning;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Player.Spawning -= OnSpawning;

            base.UnsubscribeEvents();
        }

        private void OnSpawning(SpawningEventArgs ev)
        {
            if (!Check(ev.Player))
                return;
            var Spawnroom = Map.Rooms.Where(Room => Room.Name == RoomName.HczCheckpointA);
            Room doorRoom = Room.Get((RoomIdentifier)Spawnroom);
            foreach (Door door in doorRoom.Doors)
            {
                DoorEvents.TriggerAction(door.Base, DoorAction.Opened, ev.Player.ReferenceHub);
            }
            ev.Player.Teleport(Spawnroom);
        }
    }
}