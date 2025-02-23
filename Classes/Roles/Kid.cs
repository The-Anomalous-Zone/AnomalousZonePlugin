using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using MapGeneration;
using PlayerRoles;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Player = Exiled.Events.Handlers.Player;
using Candy = Exiled.Events.Handlers.Scp330;
using Map = PluginAPI.Core.Map;
using Exiled.API.Features.Doors;
using Interactables.Interobjects.DoorUtils;
using Exiled.Events.EventArgs.Scp330;
using CustomRoles.API;
using InventorySystem.Items.Usables.Scp330;

namespace AnomalousZonePlugin.Classes.Roles
{
    [CustomRole(RoleTypeId.ClassD)]
    public class Kid : CustomRole, ICustomRole
    {
        public override uint Id { get; set; } = 1;
        public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
        public override int MaxHealth { get; set; } = 100;
        public override string Name { get; set; } = "THE CHILD";
        public override string Description { get; set; } = "<i>Your parents abandoned you</i>\nGrab 3 pieces of candy for your sadness";
                                                                 // I need the CHILD lore
                                                                 // The CHILD needs a backstory
        public override string CustomInfo { get; set; } = "THE CHILD";
        public override float SpawnChance { get; set; } = 0;
        public override Vector3 Scale { get; set; } = new Vector3(.75f, .75f, .75f);
        public override List<string> Inventory { get; set; } = new List<string>
        {
            $"{ItemType.Ammo9x19}",
            $"{ItemType.Lantern}"
        };
        public StartTeam StartTeam { get; set; } = StartTeam.ClassD;
        public int Chance { get; set; } = 0;
        public static bool spawned;
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            Limit = 0,
        };

        protected override void SubscribeEvents()
        {
            spawned = false;
            Player.Spawning += OnSpawning;
            Candy.InteractingScp330 += OnInteractingCandy;
            Player.Dying += OnDying;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            spawned = false;
            Player.Spawning -= OnSpawning;
            Candy.InteractingScp330 -= OnInteractingCandy;
            Player.Dying -= OnDying;

            base.UnsubscribeEvents();
        }

        private void OnSpawning(SpawningEventArgs ev)
        {
            if (!Check(ev.Player))
                return;
            spawned = true;
        }

        private void OnInteractingCandy(InteractingScp330EventArgs ev)
        {
            if (!Check(ev.Player))
                return;
            if (ev.UsageCount < 3 && UnityEngine.Random.value <= .05)
            {
                ev.ShouldSever = false;
                ev.IsAllowed = true;
                ev.Candy = CandyKindID.Pink;
            }
            else if (ev.UsageCount < 3)
            {
                ev.ShouldSever = false;
                ev.IsAllowed = true;
            }
            else
            {
                // Sever the CHILD's hands
                ev.ShouldSever = true;
                ev.IsAllowed = false;
            }
        }
        private void OnDying(DyingEventArgs ev)
        {
            if (!Check(ev.Player))
                return;
            ev.Player.Scale = Vector3.one;
        }
    }
}