using System.Collections.Generic;
using System.ComponentModel;
using AnomalousZonePlugin.Classes.FunnyPills;
using CameraShaking;
using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Permissions.Extensions;
using PlayerRoles;
using PlayerStatsSystem;
using UnityEngine;

namespace AnomalousZonePlugin.Classes.Items
{
    [CustomItem(ItemType.GunCom45)]
    public class AdminGun : CustomWeapon
    {
        // I can't make an admin gun
        // Everything else works
        // other gun tests work
        // Just specifically this doesn't
        // why
        // ¯\_(ツ)_/¯
        public override uint Id { get; set; } = 34;
        public override string Name { get; set; } = "Admin Gun";
        public override string Description { get; set; } = "Funny admin <s>abuse</s> helper tool";
        public override float Weight { get; set; } = 4.35f;
        public override bool ShouldMessageOnGban => true;
        public override SpawnProperties? SpawnProperties { get; set; } = new()
        {
            Limit = 0,
        };
        public override float Damage { get; set; } = float.MaxValue;
        public override byte ClipSize { get; set; } = byte.MaxValue;

        protected override void OnShooting(ShootingEventArgs ev)
        {
            if (ev.Player.CurrentItem is Firearm firearm)
            {
                if (!ev.Player.CheckPermission("AZ.gun"))
                {
                    ev.IsAllowed = false;
                    Bang.bang(ev.Player, ItemType.GrenadeHE, 0.5f);
                    ev.Player.Vaporize(ev.Player, $"{ev.Player.DisplayNickname} thought they could use the admin gun");
                    return;
                }
                // Give the gun infinite ammo
                firearm.MaxAmmo = byte.MaxValue;
                firearm.Ammo = byte.MaxValue; 
                // Give the gun no recoil
                firearm.Recoil = new RecoilSettings(0, 0, 0, 0, 0); 
            }
        }
    }
}
