using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using System.Collections.Generic;
using Player = Exiled.Events.Handlers.Player;
using CustomRoles.API;
using MEC;
using Exiled.API.Extensions;

namespace AnomalousZonePlugin.Classes.Roles
{
    [CustomRole(RoleTypeId.ClassD)]
    public class IdentityThief : CustomRole, ICustomRole
    {
        public override uint Id { get; set; } = 5;
        public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
        public override int MaxHealth { get; set; } = 100;
        public override string Name { get; set; } = "Identity Thief";
        public override string Description { get; set; } = "Drop a card from your inventory to disguise yourself";
        public override string CustomInfo { get; set; } = "Class D";
        public override float SpawnChance { get; set; } = 25f;
        public override List<string> Inventory { get; set; } = new List<string>
        {
            $"{ItemType.Ammo9x19}",
            $"{ItemType.Lantern}",
            $"{ItemType.KeycardJanitor}",
            $"{ItemType.KeycardScientist}"
        };
        public StartTeam StartTeam { get; set; } = StartTeam.ClassD;
        public int Chance { get; set; } = 25;
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            Limit = 1,
        };

        protected override void SubscribeEvents()
        {
            Player.DroppingItem += OnDroppingItem;
            Player.Escaping += OnEscaping;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Player.DroppingItem -= OnDroppingItem;
            Player.Escaping -= OnEscaping;

            base.UnsubscribeEvents();
        }

        private bool disguise = false;
        private void OnDroppingItem(DroppingItemEventArgs ev)
        {
            if (!Check(ev.Player))
                return;

            if (ev.Item.IsKeycard && !ev.IsThrown)
            {
                if (ev.Item.Type == ItemType.KeycardJanitor)
                {
                    disguise = false;
                    ev.Player.ShowHint($"You've removed your disguise");
                    ev.Player.ChangeAppearance(Plugin.Instance.Config.appearance);
                    return;
                }
                else if ((ev.Item.Type == ItemType.KeycardScientist || ev.Item.Type == ItemType.KeycardResearchCoordinator) && !disguise)
                {
                    disguise = true;
                    Plugin.Instance.Config.appearance = RoleTypeId.Scientist;
                    CustomInfo = "Scientist";
                }
                else if (ev.Item.Type == ItemType.KeycardGuard && !disguise)
                {
                    disguise = true;
                    Plugin.Instance.Config.appearance = RoleTypeId.FacilityGuard;
                    CustomInfo = "Guard";
                }
                else if (ev.Item.Type == ItemType.KeycardMTFCaptain && !disguise)
                {
                    disguise = true;
                    Plugin.Instance.Config.appearance = RoleTypeId.NtfCaptain;
                    CustomInfo = "MTF Captain";
                }
                else if (ev.Item.Type == ItemType.KeycardO5 && !disguise)
                {
                    System.Random random = new System.Random();
                    int num = random.Next(0, 7);
                    switch (num)
                    {
                        case 0:
                            Plugin.Instance.Config.appearance = RoleTypeId.Scp049;
                            break;
                        case 1:
                            Plugin.Instance.Config.appearance = RoleTypeId.Scp0492;
                            break;
                        case 2:
                            Plugin.Instance.Config.appearance = RoleTypeId.Scp096;
                            break;
                        case 3:
                            Plugin.Instance.Config.appearance = RoleTypeId.Scp106;
                            break;
                        case 4:
                            Plugin.Instance.Config.appearance = RoleTypeId.Scp173;
                            break;
                        case 5:
                            Plugin.Instance.Config.appearance = RoleTypeId.Scp3114;
                            break;
                        case 6:
                            Plugin.Instance.Config.appearance = RoleTypeId.Scp939;
                            break;
                    }
                }
                else if ((ev.Item.Type == ItemType.KeycardMTFOperative || ev.Item.Type == ItemType.KeycardMTFPrivate) && !disguise)
                {
                    disguise = true;
                    Plugin.Instance.Config.appearance = RoleTypeId.NtfSergeant;
                }
                else if (ev.Item.Type == ItemType.KeycardChaosInsurgency && !disguise)
                {
                    disguise = true;
                    Plugin.Instance.Config.appearance = RoleTypeId.ChaosConscript;
                }
                ev.Player.ChangeAppearance(Plugin.Instance.Config.appearance);
                ev.Player.ShowHint($"You've disguised yourself as a <color={Plugin.Instance.Config.appearance.GetColor().ToHex()}>{Plugin.Instance.Config.appearance.GetFullName()}</color>");

                Timing.CallDelayed(120, () => { disguise = false; });
            }
        }
        private void OnEscaping(EscapingEventArgs ev)
        {
            if (!Check(ev.Player))
                return;
        }

    }
}