using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using System.Collections.Generic;
using Player = Exiled.Events.Handlers.Player;
using CustomRoles.API;
using InventorySystem;

namespace AnomalousZonePlugin.Classes.Roles
{
    [CustomRole(RoleTypeId.ClassD)]
    public class Dealer : CustomRole, ICustomRole
    {
        public override uint Id { get; set; } = 4;
        public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
        public override int MaxHealth { get; set; } = 100;
        public override string Name { get; set; } = "Drug Dealer";
        public override string Description { get; set; } = "You spawn with a special coin";
        public override string CustomInfo { get; set; } = "Drug Dealer";
        public override float SpawnChance { get; set; } = 25f;
        public override List<string> Inventory { get; set; } = new List<string>
        {
            $"{ItemType.Ammo9x19}",
            $"{ItemType.Lantern}",
            $"{ItemType.Coin}"
        };
        public StartTeam StartTeam { get; set; } = StartTeam.ClassD;
        public int Chance { get; set; } = 25;
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            Limit = 1,
        };

        protected override void SubscribeEvents()
        {
            Player.Spawning += OnSpawning;
            Player.DroppingItem += OnDroppingItem;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Player.Spawning -= OnSpawning;
            Player.DroppingItem += OnDroppingItem;

            base.UnsubscribeEvents();
        }

        private void OnDroppingItem(DroppingItemEventArgs ev)
        {
            if (!Check(ev.Player))
                return;
            if (ev.Item.Type == ItemType.Coin)
            {
                ev.IsThrown = false;
                ev.IsAllowed = false;
                for (int x = 0; x < 7; x++)
                {
                    ev.Player.Inventory.ServerAddItem(ItemType.Painkillers);
                }
            }
            else if (ev.Item.Type == ItemType.Painkillers)
            {
                ev.IsAllowed = false;
                ev.IsThrown = false;
                ev.Player.ShowHint("<b><color=#0d98ba>The Drug Dealer cannot drop pills");
            }
        }

        private void OnSpawning(SpawningEventArgs ev)
        {
            if (!Check(ev.Player))
                return;
            // Why does the Identity Thief sometimes call this when it's a PRIVATE METHOD
            // OF A DIFFERNENT OBJECT I DON'T FUCKING KNOWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
            ev.Player.Broadcast(10, "<b><color=#0d98ba>You are the Drug Dealer drop your coin for pills!</color></b>");
        }
    }
}