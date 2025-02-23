using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using MapGeneration;
using PlayerRoles;
using System.Collections.Generic;
using System.Linq;
using Player = Exiled.Events.Handlers.Player;
using Map = PluginAPI.Core.Map;
using Exiled.API.Features.Doors;
using Interactables.Interobjects.DoorUtils;
using CustomRoles.API;
using MEC;

namespace AnomalousZonePlugin.Classes.Roles
{
    [CustomRole(RoleTypeId.Scientist)]
    public class Supervisor : CustomRole, ICustomRole
    {
        public override uint Id { get; set; } = 3;
        public override RoleTypeId Role { get; set; } = RoleTypeId.Scientist;
        public override int MaxHealth { get; set; } = 100;
        public override string Name { get; set; } = "Researcher Supervisor";
        public override string Description { get; set; } = "You've worked your way up and are a supervisor to your fellow Scientists";
        public override string CustomInfo { get; set; } = "Researcher Supervisor";
        public override float SpawnChance { get; set; } = 45f;

        public override List<string> Inventory { get; set; } = new List<string>
        {
            // At least the Researcher SUPERVISOR gets a SUPERVISOR keycard unlike
            // some other servers *cough* dr brights *cough cough*
            $"{ItemType.Radio}",
            $"{ItemType.KeycardResearchCoordinator}",
            $"{ItemType.Medkit}"
        };
        public StartTeam StartTeam { get; set; } = StartTeam.Scientist;
        public int Chance { get; set; } = 45;
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            Limit = 1,
        };


        protected override void SubscribeEvents()
        {
            Player.Spawning += OnSpawning;
            Player.Escaping += OnEscaping;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Player.Spawning -= OnSpawning;
            Player.Escaping -= OnEscaping;

            base.UnsubscribeEvents();
        }

        private void OnSpawning(SpawningEventArgs ev)
        {
            if (!Check(ev.Player))
                return;
            var Spawnroom = Map.Rooms.Where(Room => Room.Name == RoomName.LczAirlock);
            Room doorRoom = Room.Get((RoomIdentifier)Spawnroom);
            foreach (Door door in doorRoom.Doors)
            {
                DoorEvents.TriggerAction(door.Base, DoorAction.Opened, ev.Player.ReferenceHub);
            }
            ev.Player.Teleport(Spawnroom);
        }

        private void OnEscaping(EscapingEventArgs ev)
        {
            if (!Check(ev.Player))
                return;                                                                   // SpawnFlags?
                                                                                          // Yes
            ev.Player.Role.Set(RoleTypeId.NtfCaptain, Exiled.API.Enums.SpawnReason.Escaped, RoleSpawnFlags.All);
        }
    }
}